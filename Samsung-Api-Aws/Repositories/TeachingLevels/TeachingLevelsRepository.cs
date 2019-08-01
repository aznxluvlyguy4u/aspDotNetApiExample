using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.Repositories.TeachingLevels
{
    public class TeachingLevelsRepository : ITeachingLevelsRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public TeachingLevelsRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ITeachingLevel>> GetAllTeachingLevelsAysnc()
        {
            return await _dbContext.TeachingLevels.Select(x => _mapper.Map<ITeachingLevel>(x)).ToListAsync();
        }
    }
}