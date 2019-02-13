using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung.api.Enumerations;
using samsung_api.DataSource.Models;
using samsung_api.Models.Interfaces;
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

        public BuddiesRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
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
            _databaseContext.Buddies.Add(new Buddy()
            {
                ReceivingGeneralUser = receivingGeneralUser,
                RequestingGeneralUser = requestingGeneralUser,
                RequestState = BuddyRequestState.Pending
            });

            await _databaseContext.SaveChangesAsync();
        }

        private Buddy CheckExistingBuddyRequestsAysnc(int generalUserId1, int generalUserId2)
        {
            // TODO: See if rejected requests can be requested again
            Buddy existingBuddyRequest = _databaseContext.Buddies
            .Where(buddy =>
                (buddy.ReceivingGeneralUserId == generalUserId1 && buddy.RequestingGeneralUserId == generalUserId2)
                || (buddy.ReceivingGeneralUserId == generalUserId2 && buddy.RequestingGeneralUserId == generalUserId1)
                && (buddy.RequestState != BuddyRequestState.Rejected)
            )
            .FirstOrDefault();

            return existingBuddyRequest;
        }

        public async Task<IEnumerable<IBuddy>> GetBuddyRequestsByStateAysnc(int userId, BuddyRequestState state)
        {
            IEnumerable<IBuddy> buddies = _databaseContext.Buddies
            .Where(buddy =>
                (buddy.ReceivingGeneralUserId == userId || buddy.RequestingGeneralUserId == userId)
                && buddy.RequestState == state
            )
            .Select(x => _mapper.Map<IBuddy>(x))
            .ToList();

            return await Task.FromResult(buddies);
        }

        public async Task<IEnumerable<IGeneralUser>> GetMatchedBuddiesAysnc(int userId)
        {
            IEnumerable<GeneralUser> buddies = _databaseContext.Buddies
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
                .Select(g => _mapper.Map<IGeneralUser>(g))
                .ToList();

            return await Task.FromResult(generalUsers);
        }

        public async Task<IEnumerable<IGeneralUser>> GetPendingBuddyRequestsAsync(int userId)
        {
            IEnumerable<GeneralUser> buddies = _databaseContext.Buddies
                .Where(buddy =>
                    (buddy.ReceivingGeneralUserId == userId)
                    && buddy.RequestState == BuddyRequestState.Pending
                )
                .Select(x => x.RequestingGeneralUser)
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
                .Select(g => _mapper.Map<IGeneralUser>(g))
                .ToList();

            return await Task.FromResult(generalUsers);
        }

        public async Task EditBuddyRequestAsync(int receivingGeneralUserId, int requestingGeneralUserId, BuddyRequestState state)
        {
            Buddy buddyRequest = _databaseContext.Buddies
                .Where(buddy =>
                    buddy.ReceivingGeneralUserId == receivingGeneralUserId
                    && buddy.RequestingGeneralUserId == requestingGeneralUserId)
                .FirstOrDefault() ?? throw new ArgumentException($"No buddy request from user {requestingGeneralUserId}.");

            buddyRequest.RequestState = state;
            await _databaseContext.SaveChangesAsync();
        }
    }
}