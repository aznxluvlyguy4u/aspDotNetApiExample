using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung.api.Enumerations;
using samsung.api.Extensions;
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

            // Save to db
            _databaseContext.Buddies.Add(new samsung_api.DataSource.Models.Buddy()
            {
                ReceivingGeneralUser = receivingGeneralUser,
                RequestingGeneralUser = requestingGeneralUser,
                RequestState = BuddyRequestState.Pending
            });

            await _databaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IGeneralUser>> GetBuddiesByStateAysnc(int userId, BuddyRequestState state)
        {
            state = state == BuddyRequestState.None ? BuddyRequestState.Matched : state;

            IEnumerable<GeneralUser> buddies = _databaseContext.Buddies
            .Where(buddy =>
                (buddy.ReceivingGeneralUserId == userId || buddy.RequestingGeneralUserId == userId)
                && buddy.RequestState == state
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

        public async Task<IEnumerable<IBuddy>> GetPendingBuddyRequestsAsync(int userId)
        {
            IEnumerable<GeneralUser> buddies = _databaseContext.Buddies
            .Where(buddy =>
                (buddy.ReceivingGeneralUserId == userId)
                && buddy.RequestState == BuddyRequestState.Pending
            )
            .Select(x => (userId == x.RequestingGeneralUserId) ? x.ReceivingGeneralUser : x.RequestingGeneralUser)
            // Include all related data of ReceivingGeneralUser
            .ToList();


            return _databaseContext.Buddies.Where(buddy =>
                (buddy.ReceivingGeneralUserId == userId)
                && buddy.RequestState == BuddyRequestState.Pending)
            .Select(x => _mapper.Map<IBuddy>(x))
            .AsEnumerable();
        }

        //public async Task RegisterBuddyResponseAsync(int receivingUserId, int requestingBuddy, bool hasAccepted)
        //{
        //    _databaseContext.Buddies.Where(buddy =>
        //        buddy.ReceivingGeneralUserId == receivingUserId
        //        && buddy.RequestingGeneralUserId == requestingBuddy
        //    )
        //    .ForEach(x =>
        //        x.RequestState = hasAccepted ? BuddyRequestState.Matched : BuddyRequestState.Rejected
        //    );
        //    await _databaseContext.SaveChangesAsync();
        //}
    }
}