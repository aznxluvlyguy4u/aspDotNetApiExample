using samsung_api.Models.Interfaces;

namespace samsung.api.Repositories.GeneralUsers
{
    public interface IGeneralUsersRepository
    {
        IGeneralUser CreateGeneralUser(IGeneralUser generalUser);
    }
}