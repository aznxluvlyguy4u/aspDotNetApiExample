using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.Repositories.TeachingSubjects
{
    public class TeachingSubjectsRepository : ITeachingSubjectsRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public TeachingSubjectsRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ITeachingSubject>> GetAllTeachingSubjectsAysnc()
        {
            return await _dbContext.TeachingSubjects.Select(x => _mapper.Map<ITeachingSubject>(x)).ToListAsync();
        }
    }
}