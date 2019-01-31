using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Repositories.Buddies
{
    public interface IBuddiesRepository
    {
        //Task<IEnumerable<IBuddy>> GetPendingBuddyRequestsAsync(int userId);

        Task CreateBuddyRequestAsync(int requestingUserId, int receivingUserId);

        //    Task RegisterBuddyResponseAsync(int receivingUserId, int requestingBuddy, bool hasAccepted);

        Task<IEnumerable<IBuddy>> GetBuddiesAysnc(int userId);
    }
}