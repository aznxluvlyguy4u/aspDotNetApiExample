using samsung.api.Repositories.Links;
using samsung.api.Services.GeneralUsers;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Links
{
    public class LinksService : ILinksService
    {
        private readonly IGeneralUsersService _generalUsersService;
        private readonly ILinksRepository _linkRepository;

        public LinksService(IGeneralUsersService generalUsersService, ILinksRepository linkRepository)
        {
            _generalUsersService = generalUsersService;
            _linkRepository = linkRepository;
        }

        public async Task<ILink> CreateLinkAsync(ILink link, ClaimsPrincipal user)
        {
            IGeneralUser generalUser = await _generalUsersService.FindByIdentityAsync(user);
            return await _linkRepository.CreateLinkAsync(link, generalUser);
        }

        public async Task<IEnumerable<ILink>> GetMyLinksAsync(ClaimsPrincipal user)
        {
            IGeneralUser generalUser = await _generalUsersService.FindByIdentityAsync(user);
            return await _linkRepository.GetLinksByUserAysnc(generalUser.Id);
        }
    }
}
