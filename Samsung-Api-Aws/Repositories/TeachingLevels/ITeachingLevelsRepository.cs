using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Repositories.TeachingLevels
{
    public interface ITeachingLevelsRepository
    {
        Task<IEnumerable<ITeachingLevel>> GetAllTeachingLevelsAysnc();
    }
}