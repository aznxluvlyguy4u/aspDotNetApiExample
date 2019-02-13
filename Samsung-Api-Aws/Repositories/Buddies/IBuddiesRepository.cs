using samsung.api.Enumerations;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Repositories.Buddies
{
    public interface IBuddiesRepository
    {
        Task CreateBuddyRequestAsync(int requestingUserId, int receivingUserId);

        Task<IEnumerable<IGeneralUser>> GetPendingBuddyRequestsAsync(int userId);

        Task<IEnumerable<IBuddy>> GetBuddyRequestsByStateAysnc(int userId, BuddyRequestState state);

        Task<IEnumerable<IGeneralUser>> GetMatchedBuddiesAysnc(int userId);

        Task RegisterBuddyResponseAsync(int receivingUserId, int requestingBuddy, bool hasAccepted);
    }
}