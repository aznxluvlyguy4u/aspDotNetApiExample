using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Services.Interests
{
    public interface IInterestsService
    {
        Task<IEnumerable<IInterest>> GetAllInterests();
    }
}