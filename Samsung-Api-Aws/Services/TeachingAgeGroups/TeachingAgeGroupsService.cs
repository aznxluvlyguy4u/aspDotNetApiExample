using samsung.api.Repositories.TeachingAgeGroups;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Services.TeachingAgeGroups
{
    public class TeachingAgeGroupsService : ITeachingAgeGroupsService
    {
        private readonly ITeachingAgeGroupsRepository _teachingAgeGroupsRepository;

        public TeachingAgeGroupsService(ITeachingAgeGroupsRepository teachingAgeGroupsRepository)
        {
            _teachingAgeGroupsRepository = teachingAgeGroupsRepository;
        }

        public async Task<IEnumerable<ITeachingAgeGroup>> GetAllTeachingAgeGroups()
        {
            return await _teachingAgeGroupsRepository.GetAllTeachingAgeGroupsAysnc();
        }
    }
}