using samsung.api.Controllers;
using samsung.api.Enumerations;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Buddies
{
    public interface IBuddiesService
    {
        Task<IEnumerable<IBuddy>> GetBuddiesAsync(ClaimsPrincipal user, BuddyRequestState state);

        Task SendBuddyRequestAsync(ClaimsPrincipal user, int receivingUserId);

        Task RegisterBuddyResponseAsync(ClaimsPrincipal user, int requestingBuddy, bool hasAccepted);
    }
}