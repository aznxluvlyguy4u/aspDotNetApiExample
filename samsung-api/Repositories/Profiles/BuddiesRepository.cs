using samsung.api.Controllers;
using samsung.api.DataSource;
using samsung.api.Enumerations;
using samsung_api.DataSource.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using samsung.api.Extensions;
using System.Collections.Generic;
using AutoMapper;

namespace samsung.api.Repositories.Profiles
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

        public async Task CreateBuddyRequestAsync(int requestingUserId, int receivingUserId)
        {
            _databaseContext.Buddies.Add(new Buddies()
            {
                ReceivingGeneralUserId = receivingUserId,
                RequestingGeneralUserId = requestingUserId,
                RequestState = BuddyRequestState.Pending
            });
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IBuddy>> GetPendingBuddyRequestsAsync(int userId)
        {
            return _databaseContext.Buddies.Where(buddy =>
                (buddy.ReceivingGeneralUserId == userId || buddy.RequestingGeneralUserId == userId)
                && buddy.RequestState == BuddyRequestState.Pending)
            .Select(x => _mapper.Map<IBuddy>(x))
            .AsEnumerable();
        }

        public async Task RegisterBuddyResponseAsync(int receivingUserId, int requestingBuddy, bool hasAccepted)
        {
            _databaseContext.Buddies.Where(buddy =>
                buddy.ReceivingGeneralUserId == receivingUserId
                && buddy.RequestingGeneralUserId == requestingBuddy
            )
            .ForEach(x =>
                x.RequestState = hasAccepted ? BuddyRequestState.Matched : BuddyRequestState.Rejected
            );
            await _databaseContext.SaveChangesAsync();
        }
    }
}