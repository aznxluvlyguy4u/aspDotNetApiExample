using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.Repositories.TeachingAgeGroups
{
    public class TeachingAgeGroupsRepository : ITeachingAgeGroupsRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public TeachingAgeGroupsRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ITeachingAgeGroup>> GetAllTeachingAgeGroupsAysnc()
        {
            return await _dbContext.TeachingAgeGroups.Select(x => _mapper.Map<ITeachingAgeGroup>(x)).ToListAsync();
        }
    }
}