using AutoMapper;
using samsung.api.Repositories.Buddies;
using samsung.api.Repositories.GeneralUsers;
using samsung.api.Services.Buddies;
using samsung_api.Models.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.GeneralUsers
{
    public class GeneralUsersService : IGeneralUsersService
    {
        private readonly IGeneralUsersRepository _generalUsersRepository;
        private readonly IBuddiesRepository _buddiesRepository;
        private readonly IMapper _mapper;

        public GeneralUsersService(IGeneralUsersRepository generalUsersRepository, IBuddiesRepository buddiesRepository, IMapper mapper)
        {
            _generalUsersRepository = generalUsersRepository;
            _buddiesRepository = buddiesRepository;
            _mapper = mapper;
        }

        public Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser generalUser)
        {
            return _generalUsersRepository.CreateGeneralUserAsync(generalUser);
        }

        public Task<IGeneralUser> FindByIdentityAsync(ClaimsPrincipal user)
        {
            return _generalUsersRepository.FindByIdentityAsync(user);
        }

        public async Task<dynamic> FindByIdAsync(int generalUserId, ClaimsPrincipal user)
        {
            IGeneralUser loggedInUser = await FindByIdentityAsync(user);
            bool isBuddy = await _buddiesRepository.IsMatchedBuddyAsync(loggedInUser.Id, generalUserId);
            IGeneralUser IGeneralUser = await _generalUsersRepository.FindByIdAsync(generalUserId, user);

            if (isBuddy)
            {
                return IGeneralUser;
            }
            else
            {
                return _mapper.Map<IGeneralUser, ILimitedGeneralUser>(IGeneralUser);
            }
        }
    }
}