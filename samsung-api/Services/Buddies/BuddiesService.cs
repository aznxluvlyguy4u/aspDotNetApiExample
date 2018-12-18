using Microsoft.AspNetCore.Identity;
using samsung.api.Controllers;
using samsung.api.DataSource.Models;
using samsung.api.Enumerations;
using samsung.api.Repositories.Buddies;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Buddies
{
    public class BuddiesService : IBuddiesService
    {
        private readonly IBuddiesRepository _buddiesRepository;
        private readonly UserManager<GeneralUser> _userManager;

        public BuddiesService(IBuddiesRepository buddiesRepository, UserManager<GeneralUser> userManager)
        {
            _buddiesRepository = buddiesRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<IBuddy>> GetBuddiesAsync(ClaimsPrincipal user, BuddyRequestState state)
        {
            var userIdString = _userManager.GetUserId(user);
            var userId = int.Parse(userIdString);
            var buddies = await _buddiesRepository.GetBuddiesAysnc(userId);
            if (state == BuddyRequestState.None)
                return buddies;
            return buddies.Where(x => x.ContactRequestState == state);
        }

        public async Task RegisterBuddyResponseAsync(ClaimsPrincipal user, int requestingBuddy, bool hasAccepted)
        {
            var receivingUserIdString = _userManager.GetUserId(user);
            var receivingUserId = int.Parse(receivingUserIdString);
            await _buddiesRepository.RegisterBuddyResponseAsync(receivingUserId, requestingBuddy, hasAccepted);
        }

        public async Task SendBuddyRequestAsync(ClaimsPrincipal user, int receivingUserId)
        {
            var requestingUserIdString = _userManager.GetUserId(user);
            var requestingUserId = int.Parse(requestingUserIdString);
            await _buddiesRepository.CreateBuddyRequestAsync(requestingUserId, receivingUserId);
        }
    }
}