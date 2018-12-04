using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using samsung.api.Repositories.Profiles;
using samsung_api.Models.Interfaces;

namespace samsung.api.Services.Profiles
{
    public class ProfilesService : IProfilesService
    {
        private readonly IProfilesRepository _profilesRepository;
        public ProfilesService(IProfilesRepository profilesRepository)
        {
            _profilesRepository = profilesRepository;
        }

        public IProfile CreateProfile(IProfile profile)
        {
            return _profilesRepository.CreateProfile(profile);
        }
    }
}
