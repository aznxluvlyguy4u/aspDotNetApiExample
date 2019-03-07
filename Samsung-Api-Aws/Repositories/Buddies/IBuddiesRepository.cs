using samsung.api.Enumerations;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Repositories.Buddies
{
    public interface IBuddiesRepository
    {
        Task CreateBuddyRequestAsync(int requestingUserId, int receivingUserId);

        Task<IEnumerable<ILimitedGeneralUser>> GetPendingBuddyRequestsAsync(int userId);

        Task<IEnumerable<IGeneralUser>> GetBuddyRequestedUsersByStateAsync(int userId, BuddyRequestState state = default);

        Task<IEnumerable<IGeneralUser>> GetMatchedBuddiesAsync(int userId);

        Task EditBuddyRequestAsync(int receivingGeneralUserId, int requestingGeneralUserId, BuddyRequestState state);

        Task<bool> IsMatchedBuddyAsync(int generalUserId1, int generalUserId2);

        bool IsExistingBuddyRequest(int generalUserId1, int generalUserId2);
    }
}