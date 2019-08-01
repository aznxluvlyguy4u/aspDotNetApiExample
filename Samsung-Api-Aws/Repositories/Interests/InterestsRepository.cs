using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.Repositories.Interests
{
    public class InterestsRepository : IInterestsRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public InterestsRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IInterest>> GetAllInterestsAysnc()
        {
            return await _dbContext.Interests
                .OrderBy(x => x.Name)
                .Select(x => _mapper.Map<IInterest>(x)).ToListAsync();
        }
    }
}