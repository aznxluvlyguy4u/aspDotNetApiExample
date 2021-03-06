using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Buddies
{
    public interface IBuddiesService
    {
        Task<IEnumerable<IGeneralUser>> GetMyBuddiesAsync(ClaimsPrincipal user);

        Task<IEnumerable<ILimitedGeneralUser>> GetMyBuddyRequestsAsync(ClaimsPrincipal user);

        Task SendBuddyRequestAsync(ClaimsPrincipal user, int receivingGeneralUserId);

        Task EditBuddyRequestAsync(ClaimsPrincipal user, int requestingGeneralUserId, bool acceptBuddyRequest);

        Task<bool> IsMatchedBuddyAsync(ClaimsPrincipal user, int generalUserId);
    }
}