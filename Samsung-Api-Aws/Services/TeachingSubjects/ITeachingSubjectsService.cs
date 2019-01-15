using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Services.TeachingSubjects
{
    public interface ITeachingSubjectsService
    {
        Task<IEnumerable<ITeachingSubject>> GetAllTeachingSubjects();
    }
}