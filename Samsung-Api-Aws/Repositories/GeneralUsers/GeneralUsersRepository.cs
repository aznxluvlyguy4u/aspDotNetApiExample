using AutoMapper;
using Microsoft.AspNetCore.Identity;
using samsung.api.Constants;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung_api.Models.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
            var userIdentity = _mapper.Map<IGeneralUser, AppUser>(generalUser);
            var result = await _userManager.CreateAsync(userIdentity, generalUser.password);

            if (result.Succeeded)
            {
                var newGeneralUser = new GeneralUser { IdentityId = userIdentity.Id, Location = generalUser.location };

                await _dbContext.GeneralUsers.AddAsync(newGeneralUser);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(newGeneralUser));
            }

            // TODO Alter exception message in production to prevent email data leak
            throw new ArgumentException(result.Errors.FirstOrDefault()?.Description);
        }

        public async Task<IGeneralUser> FindByIdentityAsync(ClaimsPrincipal user)
        {
            var appUser = await _userManager.GetUserAsync(user);

            using (_dbContext)
            {
                return await Task.FromResult(
                    _mapper.Map<GeneralUser, IGeneralUser>(
                        _dbContext.GeneralUsers
                            .Single(generalUser => generalUser.Identity == appUser)
                    )
                );
            }
        }
    }
}