using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung.api.Enumerations;
using samsung.api.Services.AwsS3;
using samsung_api.Models.Interfaces;
using SamsungApiAws.DataSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace samsung.api.Repositories.Links
{
    public class LinksRepository : ILinksRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAwsS3Service _awsS3Service;

        public LinksRepository(DatabaseContext databaseContext, IMapper mapper, IAwsS3Service awsS3Service)
        {
            _dbContext = databaseContext;
            _mapper = mapper;
            _awsS3Service = awsS3Service;
        }

        public async Task<ILink> CreateLinkForUserAsync(ILink toBeCreatedLink, IGeneralUser user)
        {
            using (_dbContext)
            {
                var strategy = _dbContext.Database.CreateExecutionStrategy();
                ILink ILink = default;
                await strategy.Execute(async () =>
                {
                    using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        Link dbLink = _mapper.Map<ILink, Link>(toBeCreatedLink);
                        if (dbLink == default)
                            throw new ArgumentNullException(nameof(dbLink));

                        // Validate and save Interests
                        if (toBeCreatedLink.Interests != null)
                        {
                            foreach (IInterest iInterest in toBeCreatedLink.Interests)
                            {
                                Interest interest = _dbContext.Interests.SingleOrDefault(t => t.Id == iInterest.Id);
                                if (interest == default)
                                    throw new ArgumentException($"Interest ID: {iInterest.Id} could not be found.");

                                LinkInterest newLinkInterest = new LinkInterest
                                {
                                    InterestId = iInterest.Id
                                };
                                dbLink.LinkInterests.Add(newLinkInterest);
                            }
                        }

                        // Empty accidentally entered ImageWebUrl if uploadImageType is base64
                        if (dbLink.ImageType == UploadImageType.Base64 && dbLink.ImageWebUrl != null)
                            dbLink.ImageWebUrl = null;

                        // Set User
                        dbLink.GeneralUserId = user.Id;

                        // Save
                        _dbContext.Links.Add(dbLink);
                        await _dbContext.SaveChangesAsync();
                        ILink = _mapper.Map<Link, ILink>(dbLink);

                        // save Image to AWS S3
                        if (toBeCreatedLink.ImageType == UploadImageType.Base64 && toBeCreatedLink.Image != null)
                        {
                            IImage linkImage = await _awsS3Service.UploadLinkImageAsync(toBeCreatedLink.Image, ILink);
                            ILink.Image = linkImage;
                        }

                        // Commit transaction if all commands succeed, transaction will auto-rollback
                        // when disposed if either commands fails
                        transaction.Complete();
                    }
                });

                return ILink;
            }
        }

        public async Task CreateFavoriteLinkForUserAsync(ILink link, IGeneralUser user)
        {
            // Validate Link
            Link toBeCreatedFavoriteLink = _dbContext.Links.SingleOrDefault(l =>
                l.Id == link.Id
                && l.IsDeleted == false
            ) ?? throw new ArgumentException($"Link ID: {link.Id} could not be found.");
            GeneralUser generalUser = _dbContext.GeneralUsers.SingleOrDefault(g => g.Id == user.Id) ?? throw new ArgumentException($"GeneralUser ID: {user.Id} could not be found.");
            if (toBeCreatedFavoriteLink.GeneralUserId == generalUser.Id)
            {
                throw new ArgumentException("Link belong to the logged in user.");
            }
            if (CheckExistingFavoriteLinkAysnc(toBeCreatedFavoriteLink.Id, generalUser.Id) != default) throw new ArgumentException($"Favorite link already exists.");

            // Save to db
            FavoriteLink newFavoriteLink = new FavoriteLink
            {
                Link = toBeCreatedFavoriteLink,
                GeneralUser = generalUser
            };
            generalUser.FavoriteLinks.Add(newFavoriteLink);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ILink>> GetLinksByUserAsync(IGeneralUser generalUser)
        {
            IEnumerable<ILink> links = _dbContext.Links
            .Where(link =>
                link.GeneralUserId == generalUser.Id
                && link.IsDeleted == false
            )
            .Include(link => link.GeneralUser)
            .Include(l => l.LinkInterests)
                    .ThenInclude(l => l.Interest)
            .Select(link => _mapper.Map<ILink>(link))
            .ToList();

            // Make call to AWS S3 to see if any profile image is linked to this GeneralUser
            foreach (ILink link in links)
            {
                link.Image = await LoadLinkImage(link);
            }

            return await Task.FromResult(links);
        }

        public async Task<IEnumerable<ILink>> GetFavoriteLinksByUserAsync(IGeneralUser generalUser)
        {
            IEnumerable<Link> favoriteLinks = _dbContext.FavoriteLinks
                .Where(fl =>
                    fl.GeneralUserId == generalUser.Id
                )
                .Select(fl => fl.Link)
                // Include all related data of ReceivingGeneralUser
                .ToList();

            IEnumerable<ILink> links = _dbContext.Links
                .Where(
                    l => favoriteLinks.Contains(l)
                    && l.IsDeleted == false
                )
                // Include all related data of RequestingGeneralUser
                .Include(l => l.GeneralUser)
                    .ThenInclude(g => g.Identity)
                .Include(l => l.GeneralUser.City)
                .Include(l => l.GeneralUser.GeneralUserTeachingAgeGroups)
                    .ThenInclude(g => g.TeachingAgeGroup)
                .Include(l => l.GeneralUser.GeneralUserTeachingSubjects)
                    .ThenInclude(g => g.TeachingSubject)
                .Include(l => l.GeneralUser.GeneralUserTeachingLevels)
                    .ThenInclude(g => g.TeachingLevel)
                .Include(l => l.GeneralUser.GeneralUserInterests)
                    .ThenInclude(g => g.Interest)
                .Include(l => l.LinkInterests)
                    .ThenInclude(l => l.Interest)
                .Select(l => _mapper.Map<ILink>(l))
                .ToList();

            // Make call to AWS S3 to see if any profile image is linked to this GeneralUser
            foreach (ILink link in links)
            {
                link.Image = await LoadLinkImage(link);

                // Make call to AWS S3 to see if any profile image is linked to the GeneralUser of this link
                IImage profileImage = await _awsS3Service.GetProfileImageByUserAsync(link.GeneralUser.IdentityId);
                if (profileImage != null)
                {
                    link.GeneralUser.ProfileImage = profileImage;
                }
            }

            return await Task.FromResult(links);
        }

        private FavoriteLink CheckExistingFavoriteLinkAysnc(int linkId, int generalUserId)
        {
            FavoriteLink existingFavoriteLink = _dbContext.FavoriteLinks
            .Where(gl =>
                gl.LinkId == linkId && gl.GeneralUserId == generalUserId
            )
            .FirstOrDefault();

            return existingFavoriteLink;
        }

        private async Task<IImage> LoadLinkImage(ILink link)
        {
            IImage image = default;
            if (link.ImageType == UploadImageType.Base64)
            {
                image = await _awsS3Service.GetLinkImageByIdAsync(link);
            }
            else
            {
                if (link.ImageWebUrl != null)
                {
                    image = new Image();
                    image.Url = link.ImageWebUrl;
                }
            }

            return image;
        }

        public async Task DeleteLinkForUserByIdAsync(int linkId, IGeneralUser generalUser)
        {
            Link link = _dbContext.Links
            .SingleOrDefault(l =>
                l.GeneralUserId == generalUser.Id
                && l.IsDeleted == false
                && l.Id == linkId
            );

            if (link == default)
                throw new ArgumentException($"Link ID: {linkId} could not be found.");

            link.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFavoriteLinkForUserByIdAsync(int linkId, IGeneralUser generalUser)
        {
            FavoriteLink favoriteLink = _dbContext.FavoriteLinks
            .SingleOrDefault(l =>
                l.GeneralUserId == generalUser.Id
                && l.LinkId == linkId
            );

            if (favoriteLink == default)
                throw new ArgumentException($"Favorite Link ID: {linkId} could not be found.");

            _dbContext.FavoriteLinks.Remove(favoriteLink);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ILink>> FindWithSimilarPreferenceAsync(IGeneralUser loggedInUser, int limit)
        {
            IEnumerable<ILink> links = _dbContext.Links
                .Where(x =>
                    // Not links posted by user self
                    x.GeneralUserId != loggedInUser.Id
                    // Not deleted
                    && x.IsDeleted == false
                    // Not already saved as favorite link
                    && !x.FavoriteLinks.Select(f => f.GeneralUserId).Contains(loggedInUser.Id)
                    // Not those already seen
                    && !x.GeneralUserSeenLinks.Select(g => g.GeneralUserId).Contains(loggedInUser.Id)
                )
                .Include(l => l.LinkInterests)
                    .ThenInclude(l => l.Interest)
                .Include(l => l.GeneralUser)
                .AsEnumerable()
                // ordering
                .OrderByDescending(d => d.LinkInterests.Select(l => l.Interest).Where(i => loggedInUser.Interests.Select(x => x.Id).Contains(i.Id)).Count())
                .Take(limit)
                .Select(x => _mapper.Map<ILink>(x))
                .ToList();

            // Make call to AWS S3 to see if any profile image is linked to this GeneralUser
            foreach (ILink ILink in links)
            {
                ILink.Image = await LoadLinkImage(ILink);

                // Make call to AWS S3 to see if any profile image is linked to the GeneralUser of this link
                IImage profileImage = await _awsS3Service.GetProfileImageByUserAsync(ILink.GeneralUser.IdentityId);
                if (profileImage != null)
                {
                    ILink.GeneralUser.ProfileImage = profileImage;
                }

                // flag as seen
                //_dbContext.GeneralUserSeenLink.Add(new GeneralUserSeenLink { GeneralUserId = loggedInUser.Id, LinkId = ILink.Id });
                //_dbContext.SaveChanges();
            }

            return await Task.FromResult(links);
        }
    }
}