using samsung.api.Controllers;
using samsung.api.DataSource;
using samsung_api.DataSource.Models;
using System;
using System.Threading.Tasks;

namespace samsung.api.Repositories.Profiles
{
    public class BuddiesRepository : IBuddiesRepository
    {
        private readonly DatabaseContext _databaseContext;

        public BuddiesRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
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

        public Task<IBuddy> GetPendingBuddyRequests()
        {
            throw new NotImplementedException();
        }
    }
}