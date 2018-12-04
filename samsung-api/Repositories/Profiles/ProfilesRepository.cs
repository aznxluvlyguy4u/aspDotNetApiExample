using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using Profile = samsung.api.DataSource.Models.Profile;
using samsung_api.Models.Interfaces;

namespace samsung.api.Repositories.Profiles
{
    public class ProfilesRepository : IProfilesRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public ProfilesRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IProfile CreateProfile(IProfile profile)
        {
            using (_dbContext)
            {
                var entity = _mapper.Map<IProfile, Profile>(profile);
                _dbContext.Profiles.Add(entity);
                _dbContext.SaveChanges();

                return entity;
            }
        }
    }
}
