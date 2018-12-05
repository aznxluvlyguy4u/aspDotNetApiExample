using samsung_api.Models.Interfaces;

namespace samsung.api.Services.GeneralUsers
{
    public interface IGeneralUsersService
    {
        IGeneralUser CreateGeneralUser(IGeneralUser generalUser);
    }
}