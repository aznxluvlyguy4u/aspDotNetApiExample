using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Repositories.TeachingAgeGroups
{
    public interface ITeachingAgeGroupsRepository
    {
        Task<IEnumerable<ITeachingAgeGroup>> GetAllTeachingAgeGroupsAysnc();
    }
}