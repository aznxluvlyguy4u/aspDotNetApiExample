using samsung.api.DataSource.Models;
using samsung_api.Models.Interfaces;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.GeneralUsers
{
    public interface IGeneralUsersService
    {
        Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser generalUser);
        Task<IGeneralUser> FindByIdentityAsync(ClaimsPrincipal user);
    }
}