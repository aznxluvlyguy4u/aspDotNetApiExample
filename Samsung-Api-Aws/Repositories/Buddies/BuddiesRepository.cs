using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung.api.Enumerations;
using samsung.api.Services.AwsS3;
using samsung_api.DataSource.Models;
using samsung_api.Models.Interfaces;
using SamsungApiAws.DataSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.Repositories.Buddies
{
    public class BuddiesRepository : IBuddiesRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IAwsS3Service _awsS3Service;

        public BuddiesRepository(DatabaseContext databaseContext, IMapper mapper, IAwsS3Service awsS3Service)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _awsS3Service = awsS3Service;
        }

        public async Task CreateBuddyRequestAsync(int requestingGeneralUserId, int receivingGeneralUserId)
        {
            // Validate GeneralUsers
            if (requestingGeneralUserId == receivingGeneralUserId)
            {
                throw new ArgumentException("Requesting and Receiving GeneralUserId cannot be the same.");
            }

            GeneralUser requestingGeneralUser = _databaseContext.GeneralUsers.SingleOrDefault(g => g.Id == requestingGeneralUserId) ?? throw new ArgumentException($"GeneralUser ID: {requestingGeneralUserId} could not be found.");
            GeneralUser receivingGeneralUser = _databaseContext.GeneralUsers.SingleOrDefault(g => g.Id == receivingGeneralUserId) ?? throw new ArgumentException($"GeneralUser ID: {receivingGeneralUserId} could not be found.");
            if (CheckExistingBuddyRequestsAysnc(requestingGeneralUserId, receivingGeneralUserId) != default) throw new ArgumentException($"A buddy request already exists for GeneralUser {requestingGeneralUserId} and {receivingGeneralUserId}.");

            // Save to db
            _databaseContext.BuddyRequests.Add(new BuddyRequest()
            {
                ReceivingGeneralUser = receivingGeneralUser,
                RequestingGeneralUser = requestingGeneralUser,
                RequestState = BuddyRequestState.Pending
            });

            await _databaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IBuddyRequest>> GetBuddyRequestsByStateAysnc(int userId, BuddyRequestState state)
        {
            IEnumerable<IBuddyRequest> buddies = _databaseContext.BuddyRequests
            .Where(buddy =>
                (buddy.ReceivingGeneralUserId == userId || buddy.RequestingGeneralUserId == userId)
                && buddy.RequestState == state
            )
            .Select(x => _mapper.Map<IBuddyRequest>(x))
            .ToList();

            return await Task.FromResult(buddies);
        }

        public async Task<IEnumerable<IGeneralUser>> GetMatchedBuddiesAysnc(int userId)
        {
            IEnumerable<GeneralUser> buddies = _databaseContext.BuddyRequests
                .Where(buddy =>
                    (buddy.ReceivingGeneralUserId == userId || buddy.RequestingGeneralUserId == userId)
                    && buddy.RequestState == BuddyRequestState.Matched
                )
                .Select(x => (userId == x.RequestingGeneralUserId) ? x.ReceivingGeneralUser : x.RequestingGeneralUser)
                // Include all related data of ReceivingGeneralUser
                .ToList();

            IEnumerable<IGeneralUser> generalUsers = _databaseContext.GeneralUsers
                .Where(g => buddies.Contains(g))
                // Include all related data of RequestingGeneralUser
                .Include(g => g.Identity)
                .Include(g => g.City)
                .Include(g => g.GeneralUserTeachingAgeGroups)
                    .ThenInclude(t => t.TeachingAgeGroup)
                .Include(g => g.GeneralUserTeachingSubjects)
                    .ThenInclude(t => t.TeachingSubject)
                .Include(g => g.GeneralUserTeachingLevels)
                    .ThenInclude(t => t.TeachingLevel)
                .Include(g => g.GeneralUserInterests)
                    .ThenInclude(t => t.Interest)
                .Include(g => g.Links)
                .Select(g => _mapper.Map<IGeneralUser>(g))
                .ToList();

            // Make call to AWS S3 to see if any profile image is linked to this GeneralUser
            foreach (IGeneralUser generalUser in generalUsers)
            {
                IImage profileImage = await _awsS3Service.GetProfileImageByUserAsync(generalUser.IdentityId);
                if (profileImage != null)
                {
                    generalUser.ProfileImage = profileImage;
                }

                foreach (ILink link in generalUser.Links)
                {
                    link.Image = await LoadLinkImage(link);
                }
            }

            return await Task.FromResult(generalUsers);
        }

        public async Task<IEnumerable<ILimitedGeneralUser>> GetPendingBuddyRequestsAsync(int userId)
        {
            IEnumerable<GeneralUser> buddies = _databaseContext.BuddyRequests
                .Where(buddy =>
                    (buddy.ReceivingGeneralUserId == userId)
                    && buddy.RequestState == BuddyRequestState.Pending
                )
                .Select(x => x.RequestingGeneralUser)
                // Include all related data of ReceivingGeneralUser
                .ToList();

            IEnumerable<ILimitedGeneralUser> generalUsers = _databaseContext.GeneralUsers
                .Where(g => buddies.Contains(g))
                // Include all related data of RequestingGeneralUser
                .Include(g => g.Identity)
                .Include(g => g.City)
                .Include(g => g.GeneralUserTeachingAgeGroups)
                    .ThenInclude(t => t.TeachingAgeGroup)
                .Include(g => g.GeneralUserTeachingSubjects)
                    .ThenInclude(t => t.TeachingSubject)
                .Include(g => g.GeneralUserTeachingLevels)
                    .ThenInclude(t => t.TeachingLevel)
                .Include(g => g.GeneralUserInterests)
                    .ThenInclude(t => t.Interest)
                .Include(g => g.Links)
                .Select(g => _mapper.Map<ILimitedGeneralUser>(g))
                .ToList();

            // Make call to AWS S3 to see if any profile image is linked to this GeneralUser
            foreach (ILimitedGeneralUser generalUser in generalUsers)
            {
                IImage profileImage = await _awsS3Service.GetProfileImageByUserAsync(generalUser.IdentityId);
                if (profileImage != null)
                {
                    generalUser.ProfileImage = profileImage;
                }

                foreach (ILink link in generalUser.Links)
                {
                    link.Image = await LoadLinkImage(link);
                }
            }

            return await Task.FromResult(generalUsers);
        }

        public async Task EditBuddyRequestAsync(int receivingGeneralUserId, int requestingGeneralUserId, BuddyRequestState state)
        {
            BuddyRequest buddyRequest = _databaseContext.BuddyRequests
                .Where(buddy =>
                    buddy.ReceivingGeneralUserId == receivingGeneralUserId
                    && buddy.RequestingGeneralUserId == requestingGeneralUserId)
                .FirstOrDefault() ?? throw new ArgumentException($"No buddy request from user {requestingGeneralUserId}.");

            buddyRequest.RequestState = state;
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<bool> IsMatchedBuddyAsync(int generalUserId1, int generalUserId2)
        {
            IEnumerable<IGeneralUser> buddies = await GetMatchedBuddiesAysnc(generalUserId1);
            return buddies.Select(b => b.Id).Contains(generalUserId2);
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

        private BuddyRequest CheckExistingBuddyRequestsAysnc(int generalUserId1, int generalUserId2)
        {
            // TODO: See if rejected requests can be requested again
            BuddyRequest existingBuddyRequest = _databaseContext.BuddyRequests
            .Where(buddy =>
                (buddy.ReceivingGeneralUserId == generalUserId1 && buddy.RequestingGeneralUserId == generalUserId2)
                || (buddy.ReceivingGeneralUserId == generalUserId2 && buddy.RequestingGeneralUserId == generalUserId1)
                && (buddy.RequestState != BuddyRequestState.Rejected)
            )
            .FirstOrDefault();

            return existingBuddyRequest;
        }
    }
}