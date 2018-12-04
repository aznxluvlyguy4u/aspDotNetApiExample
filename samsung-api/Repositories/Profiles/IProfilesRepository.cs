using samsung_api.Models.Interfaces;

namespace samsung.api.Repositories.Profiles
{
    public interface IProfilesRepository
    {
        IProfile CreateProfile(IProfile profile);
    }
}