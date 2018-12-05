using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung_api.Models.Interfaces;
using samsung.api.DataSource.Models;

namespace samsung.api.Repositories.GeneralUsers
{
    public class GeneralUsersRepository : IGeneralUsersRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public GeneralUsersRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IGeneralUser CreateGeneralUser(IGeneralUser generalUser)
        {
            using (_dbContext)
            {
                var entity = _mapper.Map<IGeneralUser, GeneralUser>(generalUser);
                _dbContext.GeneralUsers.Add(entity);
                _dbContext.SaveChanges();
                return _mapper.Map<GeneralUser, IGeneralUser>(entity);
            }
        }
    }
}
