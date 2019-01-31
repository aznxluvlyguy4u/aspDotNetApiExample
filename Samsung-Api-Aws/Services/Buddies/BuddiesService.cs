using Microsoft.AspNetCore.Identity;
using samsung.api.DataSource.Models;
using samsung.api.Enumerations;
using samsung.api.Repositories.Buddies;
using samsung.api.Services.GeneralUsers;
using samsung_api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Buddies
{
    public class BuddiesService : IBuddiesService
    {
        private readonly IBuddiesRepository _buddiesRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IGeneralUsersService _generalUsersService;

        public BuddiesService(IBuddiesRepository buddiesRepository, UserManager<AppUser> userManager, IGeneralUsersService generalUsersService)
        {
            _buddiesRepository = buddiesRepository;
            _userManager = userManager;
            _generalUsersService = generalUsersService;
        }

        public async Task<IEnumerable<IBuddy>> GetBuddiesAsync(ClaimsPrincipal user, BuddyRequestState state)
        {
            var userIdString = _userManager.GetUserId(user);
            var userId = int.Parse(userIdString);
            var buddies = await _buddiesRepository.GetBuddiesAysnc(userId);
            if (state == BuddyRequestState.None)
                return buddies;
            return buddies.Where(x => x.contactRequestState == state);
        }

        //public async Task RegisterBuddyResponseAsync(ClaimsPrincipal user, int requestingBuddy, bool hasAccepted)
        //{
        //    var receivingUserIdString = _userManager.GetUserId(user);
        //    var receivingUserId = int.Parse(receivingUserIdString);
        //    await _buddiesRepository.RegisterBuddyResponseAsync(receivingUserId, requestingBuddy, hasAccepted);
        //}

        public async Task SendBuddyRequestAsync(ClaimsPrincipal user, int receivingGeneralUserId)
        {
            IGeneralUser requestingGeneralUser = await _generalUsersService.FindByIdentityAsync(user);
            await _buddiesRepository.CreateBuddyRequestAsync(requestingGeneralUser.Id, receivingGeneralUserId);
        }
    }
}