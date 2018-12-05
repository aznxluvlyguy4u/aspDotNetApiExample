using samsung_api.Models.Interfaces;
using System.Threading.Tasks;

namespace samsung.api.Services.GeneralUsers
{
    public interface IGeneralUsersService
    {
        Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser generalUser);
    }
}