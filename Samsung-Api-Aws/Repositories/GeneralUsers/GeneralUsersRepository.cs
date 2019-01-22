using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser toBeCreatedgeneralUser)
        {
            var userIdentity = _mapper.Map<IGeneralUser, AppUser>(toBeCreatedgeneralUser);
            var result = await _userManager.CreateAsync(userIdentity, toBeCreatedgeneralUser.Password);

            if (result.Succeeded)
            {
                var newGeneralUser = new GeneralUser
                {
                    IdentityId = userIdentity.Id,
                    Location = toBeCreatedgeneralUser.Location,
                    Locale = toBeCreatedgeneralUser.Locale,
                    Gender = toBeCreatedgeneralUser.Gender
                };

                // TODO: simplify mapping mechanism so that these relational objects don't need to be saved separately
                // Save TeachingSubjects
                if (toBeCreatedgeneralUser.TeachingSubjects != null)
                {
                    foreach (ITeachingSubject teachingSubject in toBeCreatedgeneralUser.TeachingSubjects)
                    {
                        GeneralUserTeachingSubject newGeneralUserTeachingSubject = new GeneralUserTeachingSubject
                        {
                            TeachingSubjectId = teachingSubject.Id
                        };
                        newGeneralUser.GeneralUserTeachingSubjects.Add(newGeneralUserTeachingSubject);
                    }
                }

                // Save TeachingLevels
                if (toBeCreatedgeneralUser.TeachingLevels != null)
                {
                    foreach (ITeachingLevel teachingLevel in toBeCreatedgeneralUser.TeachingLevels)
                    {
                        GeneralUserTeachingLevel newGeneralUserTeachingLevel = new GeneralUserTeachingLevel
                        {
                            TeachingLevelId = teachingLevel.Id
                        };
                        newGeneralUser.GeneralUserTeachingLevels.Add(newGeneralUserTeachingLevel);
                    }
                }

                // Save Interests
                if (toBeCreatedgeneralUser.Interests != null)
                {
                    foreach (IInterest interest in toBeCreatedgeneralUser.Interests)
                    {
                        GeneralUserInterest newGeneralUserInterest = new GeneralUserInterest
                        {
                            InterestId = interest.Id
                        };
                        newGeneralUser.GeneralUserInterests.Add(newGeneralUserInterest);
                    }
                }

                await _dbContext.GeneralUsers.AddAsync(newGeneralUser);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(newGeneralUser));
            }

            // TODO Alter exception message in production to prevent email data leak
            throw new ArgumentException(result.Errors.FirstOrDefault()?.Description);
        }

        public async Task<IGeneralUser> FindByIdentityAsync(ClaimsPrincipal user)
        {
            var appUserId = _userManager.GetUserId(user);
            var generalUser = _dbContext.GeneralUsers
                .Include(g => g.Identity)
                .Include(g => g.GeneralUserTeachingSubjects)
                    .ThenInclude(t => t.TeachingSubject)
                .Include(g => g.GeneralUserTeachingLevels)
                    .ThenInclude(t => t.TeachingLevel)
                .Include(g => g.GeneralUserInterests)
                    .ThenInclude(t => t.Interest)
                .FirstOrDefault(g => g.IdentityId == new Guid(appUserId));

            IGeneralUser IGeneralUser = await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(generalUser));
            return IGeneralUser;
        }
    }
}