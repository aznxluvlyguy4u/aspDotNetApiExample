using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung.api.Services.AwsS3;
using samsung_api.Models.Interfaces;

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

        public async Task<ILink> CreateLinkAsync(ILink toBeCreatedLink, IGeneralUser user)
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
                    dbLink.GeneralUserId = user.Id;
                    _dbContext.Links.Add(dbLink);
                    await _dbContext.SaveChangesAsync();
                    ILink = _mapper.Map<Link, ILink>(dbLink);
                    // save Image to AWS S3
                    if (toBeCreatedLink.Image != null)
                    {
                        IImage linkImage = await _awsS3Service.UploadLinkImageAsync(toBeCreatedLink.Image, dbLink.Id);
                        ILink.Image = linkImage;
                    }

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Complete();
                }
            });

            return ILink;
        }

        public async Task<IEnumerable<ILink>> GetLinksByUserAysnc(int generalUserId)
        {
            IEnumerable<ILink> links = _dbContext.Links
            .Where(link =>
                link.GeneralUserId == generalUserId
                && link.IsDeleted == false
            )
            .Include(link => link.GeneralUser)
            .Select(link => _mapper.Map<ILink>(link))
            .ToList();

            // Make call to AWS S3 to see if any profile image is linked to this GeneralUser
            foreach (ILink link in links)
            {
                IImage image = await _awsS3Service.GetLinkImageByIdAsync(link.Id);
                if (image != null)
                {
                    link.Image = image;
                }
            }

            return await Task.FromResult(links);
        }
    }
}