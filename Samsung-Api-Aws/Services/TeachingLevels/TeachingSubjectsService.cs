using samsung.api.Repositories.TeachingLevels;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Services.TeachingLevels
{
    public class TeachingLevelsService : ITeachingLevelsService
    {
        private readonly ITeachingLevelsRepository _teachingLevelsRepository;

        public TeachingLevelsService(ITeachingLevelsRepository teachingLevelsRepository)
        {
            _teachingLevelsRepository = teachingLevelsRepository;
        }

        public async Task<IEnumerable<ITeachingLevel>> GetAllTeachingLevels()
        {
            return await _teachingLevelsRepository.GetAllTeachingLevelsAysnc();
        }
    }
}