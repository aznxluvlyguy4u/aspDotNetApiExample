using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Repositories.Interests
{
    public interface IInterestsRepository
    {
        Task<IEnumerable<IInterest>> GetAllInterestsAysnc();
    }
}