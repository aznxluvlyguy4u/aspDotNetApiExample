using AutoMapper;
using Microsoft.AspNetCore.Identity;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung_api.Models.Interfaces;
using System;
using System.Collections.Generic;
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
                    foreach (int teachingSubjectId in toBeCreatedgeneralUser.TeachingSubjects)
                    {
                        GeneralUserTeachingSubject newGeneralUserTeachingSubject = new GeneralUserTeachingSubject
                        {
                            TeachingSubjectId = teachingSubjectId
                        };
                        newGeneralUser.GeneralUserTeachingSubjects.Add(newGeneralUserTeachingSubject);
                    }
                }

                // Save TeachingLevels
                if (toBeCreatedgeneralUser.TeachingLevels != null)
                {
                    foreach (int teachingLevelId in toBeCreatedgeneralUser.TeachingLevels)
                    {
                        GeneralUserTeachingLevel newGeneralUserTeachingLevel = new GeneralUserTeachingLevel
                        {
                            TeachingLevelId = teachingLevelId
                        };
                        newGeneralUser.GeneralUserTeachingLevels.Add(newGeneralUserTeachingLevel);
                    }
                }

                // Save Interests
                if (toBeCreatedgeneralUser.Interests != null)
                {
                    foreach (int interestId in toBeCreatedgeneralUser.Interests)
                    {
                        GeneralUserInterest newGeneralUserInterest = new GeneralUserInterest
                        {
                            InterestId = interestId
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