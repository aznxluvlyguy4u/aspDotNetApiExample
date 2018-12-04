using samsung_api.Models.Interfaces;

namespace samsung.api.Services.Profiles
{
    public interface IProfilesService
    {
        IProfile CreateProfile(IProfile profile);
    }
}