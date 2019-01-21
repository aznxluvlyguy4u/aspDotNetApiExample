using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Services.TeachingLevels
{
    public interface ITeachingLevelsService
    {
        Task<IEnumerable<ITeachingLevel>> GetAllTeachingLevels();
    }
}