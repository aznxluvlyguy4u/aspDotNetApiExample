using AutoMapper;
using Microsoft.AspNetCore.Identity;
using samsung.api.DataSource.Models;
using samsung.api.Enumerations;
using samsung.api.Models.Response;
using samsung.api.Repositories.GeneralUsers;
using samsung.api.Repositories.Links;
using samsung.api.Services.GeneralUsers;
using samsung.api.Services.Links;
using samsung_api.Models.Interfaces;
using SamsungApiAws.DataSource.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Feeds
{
    public class FeedsService : IFeedsService
    {
        private readonly IMapper _mapper;
        private readonly IGeneralUsersService _generalUsersService;
        private readonly ILinksService _linksService;
        private readonly IGeneralUsersRepository _generalUsersRepository;
        private readonly ILinksRepository _linksRepository;

        public FeedsService(IMapper mapper, IGeneralUsersService generalUsersService, ILinksService linksService, IGeneralUsersRepository generalUsersRepository, ILinksRepository linksRepository)
        {
            _mapper = mapper;
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
            IEnumerable<IGeneralUser> feedUsers = await _generalUsersRepository.FindWithSimilarPreferenceAsync(loggedInGeneralUser, 1);
            IEnumerable<ILink> feedLinks = await _linksRepository.FindWithSimilarPreferenceAsync(loggedInGeneralUser, 4);

            List<IFeed> feeds = new List<IFeed>();
            var MatchedGeneralUser = feedUsers.FirstOrDefault();
            if (MatchedGeneralUser != default)
            {
                ILimitedGeneralUser MatchedGeneralUserResponse = _mapper.Map<ILimitedGeneralUser>(MatchedGeneralUser);
                feeds.Add(new Feed { Type = FeedType.GeneralUser, Body = MatchedGeneralUserResponse });
            }
                
            foreach (ILink link in feedLinks)
            {
                var linkResponse = _mapper.Map<GetLinkResponse>(link);
                feeds.Add(new Feed { Type = FeedType.Link, Body = linkResponse });
            }

            return feeds;
        }
    }
}