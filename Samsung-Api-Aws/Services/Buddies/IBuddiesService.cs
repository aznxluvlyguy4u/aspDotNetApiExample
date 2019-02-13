using samsung.api.Enumerations;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Buddies
{
    public interface IBuddiesService
    {
        Task<IEnumerable<IGeneralUser>> GetMyBuddiesAsync(ClaimsPrincipal user);

        Task<IEnumerable<IGeneralUser>> GetMyBuddyRequestsAsync(ClaimsPrincipal user);

        Task SendBuddyRequestAsync(ClaimsPrincipal user, int receivingUserId);

        Task RegisterBuddyResponseAsync(ClaimsPrincipal user, int requestingBuddy, bool hasAccepted);
    }
}