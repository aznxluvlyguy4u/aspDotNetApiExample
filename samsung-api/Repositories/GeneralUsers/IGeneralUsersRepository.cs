using samsung_api.Models.Interfaces;
using System.Threading.Tasks;

namespace samsung.api.Repositories.GeneralUsers
{
    public interface IGeneralUsersRepository
    {
        Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser generalUser);
    }
}