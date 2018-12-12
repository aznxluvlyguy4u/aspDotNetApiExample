using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung_api.Models.Interfaces;
using samsung.api.DataSource.Models;
using Microsoft.AspNetCore.Identity;

namespace samsung.api.Repositories.GeneralUsers
{
    public class GeneralUsersRepository : IGeneralUsersRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;


        public GeneralUsersRepository(DatabaseContext dbContext, IMapper mapper, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser generalUser)
        {
            using (_userManager)
            {
                var userIdentity = _mapper.Map<IGeneralUser, AppUser>(generalUser);
                var result = await _userManager.CreateAsync(userIdentity, generalUser.Password);

                if (result.Succeeded)
                {
                    var newGeneralUser = new GeneralUser { IdentityId = userIdentity.Id, Location = generalUser.Location };

                    await _dbContext.GeneralUsers.AddAsync(newGeneralUser);
                    await _dbContext.SaveChangesAsync();
                    return await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(newGeneralUser));
                }

                // TODO Alter exception message in production to prevent email data leak
                throw new ArgumentException(result.Errors.FirstOrDefault()?.Description);
            }
        }
    }
}
