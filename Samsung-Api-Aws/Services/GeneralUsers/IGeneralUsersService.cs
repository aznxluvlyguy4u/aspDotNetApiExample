using samsung_api.Models.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.GeneralUsers
{
    public interface IGeneralUsersService
    {
        Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser generalUser);

        Task<dynamic> FindByIdAsync(int generalUserId, ClaimsPrincipal user);

        Task<IGeneralUser> FindByIdentityAsync(ClaimsPrincipal user);
    }
}