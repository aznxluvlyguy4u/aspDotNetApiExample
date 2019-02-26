using AutoMapper;
using samsung.api.Models.Requests;
using samsung.api.Models.Response;
using samsung.api.Repositories.Links;
using samsung.api.Services.GeneralUsers;
using samsung_api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace samsung.api.Services.Links
{
    public class LinksService : ILinksService
    {
        private readonly IGeneralUsersService _generalUsersService;
        private readonly ILinksRepository _linkRepository;
        private readonly IMapper _mapper;

        public LinksService(IGeneralUsersService generalUsersService, ILinksRepository linkRepository, IMapper mapper)
        {
            _generalUsersService = generalUsersService;
            _mapper = mapper;
            _linkRepository = linkRepository;
        }

        public async Task<ILink> CreateLinkAsync(ILink link, ClaimsPrincipal user)
        {
            IGeneralUser generalUser = await _generalUsersService.FindByIdentityAsync(user);
            return await _linkRepository.CreateLinkForUserAsync(link, generalUser);
        }

        public async Task CreateFavoriteLinkAsync(ILink link, ClaimsPrincipal user)
        {
            IGeneralUser generalUser = await _generalUsersService.FindByIdentityAsync(user);
            await _linkRepository.CreateFavoriteLinkForUserAsync(link, generalUser);
        }

        public async Task<IEnumerable<ILink>> GetMyLinksAsync(ClaimsPrincipal user)
        {
            IGeneralUser generalUser = await _generalUsersService.FindByIdentityAsync(user);
            return await _linkRepository.GetLinksByUserAsync(generalUser);
        }

        public async Task<IEnumerable<ILink>> GetMyFavoriteLinksAsync(ClaimsPrincipal user)
        {
            IGeneralUser generalUser = await _generalUsersService.FindByIdentityAsync(user);
            return await _linkRepository.GetFavoriteLinksByUserAsync(generalUser);
        }

        public async Task<IEnumerable<GetLinkImageSearchResponse>> FindImagesByUrl(FindImageRequest findImageRequest)
        {
            using (var client = new HttpClient())
            {
                string[] httpPrefixes = { "http://", "https://" };
                bool prefixMatch = httpPrefixes.Any(prefix => findImageRequest.Url.StartsWith(prefix));
                if (!prefixMatch)
                {
                    findImageRequest.Url = "https://" + findImageRequest.Url;
                }

                var result = await client.GetStringAsync(findImageRequest.Url);
                if (string.IsNullOrWhiteSpace(result))
                    throw new ArgumentNullException(nameof(findImageRequest.Url));

                MatchCollection m1 = Regex.Matches(result, "(?:src|href)=\"(http[s]{0,1}:\\/\\/[^\\s]{1,100}\\.(?:jpg|png|jpeg|gif))\"");

                IEnumerable<GetLinkImageSearchResponse> images = m1
                    .Select(m => m.Groups?[1]?.Value ?? null)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(link => _mapper.Map<GetLinkImageSearchResponse>(link));

                return images;
            }
        }
    }
}
