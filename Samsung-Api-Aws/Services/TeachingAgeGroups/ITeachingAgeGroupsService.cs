using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Services.TeachingAgeGroups
{
    public interface ITeachingAgeGroupsService
    {
        Task<IEnumerable<ITeachingAgeGroup>> GetAllTeachingAgeGroups();
    }
}