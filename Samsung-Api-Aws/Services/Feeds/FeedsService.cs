using Microsoft.AspNetCore.Identity;
using samsung.api.DataSource.Models;
using samsung.api.Repositories.GeneralUsers;
using samsung.api.Repositories.Links;
using samsung.api.Services.GeneralUsers;
using samsung.api.Services.Links;
using samsung_api.Models.Interfaces;
using SamsungApiAws.DataSource.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Feeds
{
    public class FeedsService : IFeedsService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IGeneralUsersService _generalUsersService;
        private readonly ILinksService _linksService;
        private readonly IGeneralUsersRepository _generalUsersRepository;
        private readonly ILinksRepository _linksRepository;

        public FeedsService(IGeneralUsersService generalUsersService, ILinksService linksService, IGeneralUsersRepository generalUsersRepository, ILinksRepository linksRepository)
        {
            _generalUsersService = generalUsersService;
            _linksService = linksService;
            _generalUsersRepository = generalUsersRepository;
            _linksRepository = linksRepository;
        }

        /// <summary>
        /// Lampen 5, interesse 3 per, ageGroup 2. schoollevel 2, location 2 filters toepassen
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<IFeed>> GetFeedsAsync(ClaimsPrincipal user)
        {
            IGeneralUser loggedInGeneralUser = await _generalUsersService.FindByIdentityAsync(user);
            await _generalUsersRepository.FindWithSimilarPreferenceAsync(user);

            return new List<IFeed>();
            throw new System.NotImplementedException();
        }
    }
}