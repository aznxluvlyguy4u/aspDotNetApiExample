using samsung.api.Repositories.Interests;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace samsung.api.Services.Interests
{
    public class InterestsService : IInterestsService
    {
        private readonly IInterestsRepository _interestsRepository;

        public InterestsService(IInterestsRepository interestsRepository)
        {
            _interestsRepository = interestsRepository;
        }

        public async Task<IEnumerable<IInterest>> GetAllInterests()
        {
            return await _interestsRepository.GetAllInterestsAysnc();
        }
    }
}