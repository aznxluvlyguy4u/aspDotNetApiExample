using samsung.api.Controllers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung_api.Models.Interfaces
{
    public interface IBuddyService
    {
        Task<List<IBuddy>> GetContactsAsync(ClaimsPrincipal user, BuddyRequestState state);

        Task SendBuddyRequestAsync(ClaimsPrincipal user, int receivingUserId);

        Task RegisterBuddyResponseAsync(ClaimsPrincipal user, int requestingBuddy, bool hasAccepted);
    }
}