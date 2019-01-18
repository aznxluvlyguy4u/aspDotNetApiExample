using samsung.api.Repositories.GeneralUsers;
using samsung_api.Models.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.GeneralUsers
{
    public class GeneralUsersService : IGeneralUsersService
    {
        private readonly IGeneralUsersRepository _generalUsersRepository;

        public GeneralUsersService(IGeneralUsersRepository generalUsersRepository)
        {
            _generalUsersRepository = generalUsersRepository;
        }

        public Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser generalUser)
        {
            return _generalUsersRepository.CreateGeneralUserAsync(generalUser);
        }

        public Task<IGeneralUser> FindByIdentityAsync(ClaimsPrincipal user)
        {
            return _generalUsersRepository.FindByIdentityAsync(user);
        }
    }
}