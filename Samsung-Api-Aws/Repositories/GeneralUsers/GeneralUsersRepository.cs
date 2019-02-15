using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung.api.Repositories.Buddies;
using samsung.api.Services.Buddies;
using samsung_api.Models.Interfaces;
using SamsungApiAws.DataSource.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;

namespace samsung.api.Repositories.GeneralUsers
{
    public class GeneralUsersRepository : IGeneralUsersRepository
    {
        private readonly DatabaseContext _dbContext;
        //private readonly IBuddiesRepository _buddiesRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public GeneralUsersRepository(DatabaseContext dbContext, IMapper mapper, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
            //_buddiesRepository = buddiesRepository;
        }

        public async Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser toBeCreatedgeneralUser)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            IGeneralUser returnValue = default;

            await strategy.Execute(async () =>
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var userIdentity = _mapper.Map<IGeneralUser, AppUser>(toBeCreatedgeneralUser);
                    var result = await _userManager.CreateAsync(userIdentity, toBeCreatedgeneralUser.Password);

                    if (!result.Succeeded)
                    {
                        throw new ArgumentException(result.Errors.FirstOrDefault()?.Description);
                    }

                    // Validate related id's
                    var newGeneralUser = new GeneralUser
                    {
                        Identity = userIdentity,
                        Location = toBeCreatedgeneralUser.Location,
                        Locale = toBeCreatedgeneralUser.Locale,
                        Gender = toBeCreatedgeneralUser.Gender
                    };

                    // TODO: simplify mapping mechanism so that these relational objects don't need to be saved separately
                    // Validate and save city
                    City city = _dbContext.Cities.SingleOrDefault(c => c.Id == toBeCreatedgeneralUser.City.Id);
                    newGeneralUser.City = city ?? throw new ArgumentException($"City ID: {toBeCreatedgeneralUser.City.Id} could not be found.");

                    // Validate and save TeachingAgeGroups
                    if (toBeCreatedgeneralUser.TeachingAgeGroups != null)
                    {
                        foreach (ITeachingAgeGroup iTeachingAgeGroup in toBeCreatedgeneralUser.TeachingAgeGroups)
                        {
                            TeachingAgeGroup teachingAgeGroup = _dbContext.TeachingAgeGroups.SingleOrDefault(t => t.Id == iTeachingAgeGroup.Id);
                            if (teachingAgeGroup == default)
                            {
                                throw new ArgumentException($"TeachingGroup ID: {iTeachingAgeGroup.Id} could not be found.");
                            }

                            GeneralUserTeachingAgeGroup newGeneralUserTeachingAgeGroup = new GeneralUserTeachingAgeGroup
                            {
                                TeachingAgeGroupId = iTeachingAgeGroup.Id
                            };
                            newGeneralUser.GeneralUserTeachingAgeGroups.Add(newGeneralUserTeachingAgeGroup);
                        }
                    }

                    // Validate and Save TeachingSubjects
                    if (toBeCreatedgeneralUser.TeachingSubjects != null)
                    {
                        foreach (ITeachingSubject iTeachingSubject in toBeCreatedgeneralUser.TeachingSubjects)
                        {
                            TeachingSubject teachingSubject = _dbContext.TeachingSubjects.SingleOrDefault(t => t.Id == iTeachingSubject.Id);
                            if (teachingSubject == default)
                            {
                                throw new ArgumentException($"TeachingSubject ID: {iTeachingSubject.Id} could not be found.");
                            }

                            GeneralUserTeachingSubject newGeneralUserTeachingSubject = new GeneralUserTeachingSubject
                            {
                                TeachingSubjectId = iTeachingSubject.Id
                            };
                            newGeneralUser.GeneralUserTeachingSubjects.Add(newGeneralUserTeachingSubject);
                        }
                    }

                    // Validate and Save TeachingLevels
                    if (toBeCreatedgeneralUser.TeachingLevels != null)
                    {
                        foreach (ITeachingLevel iTeachingLevel in toBeCreatedgeneralUser.TeachingLevels)
                        {
                            TeachingLevel teachingLevel = _dbContext.TeachingLevels.SingleOrDefault(t => t.Id == iTeachingLevel.Id);
                            if (teachingLevel == default)
                            {
                                throw new ArgumentException($"TeachingLevel ID: {iTeachingLevel.Id} could not be found.");
                            }

                            GeneralUserTeachingLevel newGeneralUserTeachingLevel = new GeneralUserTeachingLevel
                            {
                                TeachingLevelId = iTeachingLevel.Id
                            };
                            newGeneralUser.GeneralUserTeachingLevels.Add(newGeneralUserTeachingLevel);
                        }
                    }

                    // Validate and Save Interests
                    if (toBeCreatedgeneralUser.Interests != null)
                    {
                        foreach (IInterest iInterest in toBeCreatedgeneralUser.Interests)
                        {
                            Interest interest = _dbContext.Interests.SingleOrDefault(i => i.Id == iInterest.Id);
                            if (interest == default)
                            {
                                throw new ArgumentException($"Interest ID: {iInterest.Id} could not be found.");
                            }

                            GeneralUserInterest newGeneralUserInterest = new GeneralUserInterest
                            {
                                InterestId = iInterest.Id
                            };
                            newGeneralUser.GeneralUserInterests.Add(newGeneralUserInterest);
                        }
                    }

                    await _dbContext.GeneralUsers.AddAsync(newGeneralUser);
                    await _dbContext.SaveChangesAsync();

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Complete();

                    returnValue = await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(newGeneralUser));
                }
            });

            return returnValue;
        }

        public async Task<dynamic> FindByIdAsync(int generalUserId, ClaimsPrincipal user)
        {
            var generalUser = _dbContext.GeneralUsers
                .Include(g => g.Identity)
                .Include(g => g.City)
                .Include(g => g.GeneralUserTeachingAgeGroups)
                    .ThenInclude(t => t.TeachingAgeGroup)
                .Include(g => g.GeneralUserTeachingSubjects)
                    .ThenInclude(t => t.TeachingSubject)
                .Include(g => g.GeneralUserTeachingLevels)
                    .ThenInclude(t => t.TeachingLevel)
                .Include(g => g.GeneralUserInterests)
                    .ThenInclude(t => t.Interest)
                .FirstOrDefault(g => g.Id == generalUserId);

            //dynamic result;
            //if (await _buddiesRepository.IsMatchedBuddyAsync(FindByIdentityAsync(user).Id, generalUserId))
            //{
            //    result = await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(generalUser));
            //} else
            //{
            //    result = await Task.FromResult(_mapper.Map<GeneralUser, ILimitedGeneralUser>(generalUser));
            //}

            return await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(generalUser));
        }

        public async Task<IGeneralUser> FindByIdentityAsync(ClaimsPrincipal user)
        {
            var appUserId = _userManager.GetUserId(user);
            var generalUser = _dbContext.GeneralUsers
                .Include(g => g.Identity)
                .Include(g => g.City)
                .Include(g => g.GeneralUserTeachingAgeGroups)
                    .ThenInclude(t => t.TeachingAgeGroup)
                .Include(g => g.GeneralUserTeachingSubjects)
                    .ThenInclude(t => t.TeachingSubject)
                .Include(g => g.GeneralUserTeachingLevels)
                    .ThenInclude(t => t.TeachingLevel)
                .Include(g => g.GeneralUserInterests)
                    .ThenInclude(t => t.Interest)
                .FirstOrDefault(g => g.Identity.Id == new Guid(appUserId));

            IGeneralUser IGeneralUser = await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(generalUser));
            return IGeneralUser;
        }
    }
}