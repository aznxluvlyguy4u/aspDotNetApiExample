using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Repositories.TeachingSubjects
{
    public interface ITeachingSubjectsRepository
    {
        Task<IEnumerable<ITeachingSubject>> GetAllTeachingSubjectsAysnc();
    }
}