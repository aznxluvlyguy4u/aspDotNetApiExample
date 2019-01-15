using samsung.api.Repositories.TeachingSubjects;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Services.TeachingSubjects
{
    public class TeachingSubjectsService : ITeachingSubjectsService
    {
        private readonly ITeachingSubjectsRepository _teachingSubjectsRepository;

        public TeachingSubjectsService(ITeachingSubjectsRepository teachingSubjectsRepository)
        {
            _teachingSubjectsRepository = teachingSubjectsRepository;
        }

        public async Task<IEnumerable<ITeachingSubject>> GetAllTeachingSubjects()
        {
            return await _teachingSubjectsRepository.GetAllTeachingSubjectsAysnc();
        }
    }
}