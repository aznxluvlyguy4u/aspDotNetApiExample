using Microsoft.AspNetCore.Identity;
using samsung.api.Controllers;
using samsung.api.DataSource.Models;
using samsung.api.Repositories.Profiles;
using samsung_api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Contacts
{
    public class BuddyService : IBuddyService
    {
        private readonly IBuddiesRepository _buddiesRepository;
        private readonly UserManager<GeneralUser> _userManager;

        public BuddyService(IBuddiesRepository buddiesRepository, UserManager<GeneralUser> userManager)
        {
            _buddiesRepository = buddiesRepository;
            _userManager = userManager;
        }

        public Task<List<IBuddy>> GetContactsAsync(ClaimsPrincipal user, BuddyRequestState state)
        {
            throw new NotImplementedException();
        }

        public Task RegisterBuddyResponseAsync(ClaimsPrincipal user, int requestingBuddy, bool hasAccepted)
        {
            throw new NotImplementedException();
        }

        public async Task SendBuddyRequestAsync(ClaimsPrincipal user, int receivingUserId)
        {
            var requestingUserIdString = _userManager.GetUserId(user);
            var requestingUserId = int.Parse(requestingUserIdString);
            await _buddiesRepository.CreateBuddyRequestAsync(requestingUserId, receivingUserId);
        }
    }
}