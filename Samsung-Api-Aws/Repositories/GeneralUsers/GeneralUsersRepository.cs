using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung.api.Repositories.Buddies;
using samsung.api.Services.AwsS3;
using samsung_api.Models.Interfaces;
using SamsungApiAws.DataSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;

namespace samsung.api.Repositories.GeneralUsers
{
    public class GeneralUsersRepository : IGeneralUsersRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAwsS3Service _awsS3Service;
        private readonly IBuddiesRepository _buddiesRepository;

        public GeneralUsersRepository(DatabaseContext dbContext, IMapper mapper, UserManager<AppUser> userManager, IAwsS3Service awsS3Service, IBuddiesRepository buddiesRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
            _awsS3Service = awsS3Service;
            _buddiesRepository = buddiesRepository;
        }

        /// <summary>
        /// CreateGeneralUserAsync
        /// </summary>
        /// <param name="toBeCreatedgeneralUser"></param>
        /// <returns></returns>
        public async Task<IGeneralUser> CreateGeneralUserAsync(IGeneralUser toBeCreatedgeneralUser)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            IGeneralUser IGeneralUser = default;

            await strategy.Execute(async () =>
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // First try to create AppUser
                    var userIdentity = _mapper.Map<IGeneralUser, AppUser>(toBeCreatedgeneralUser);
                    var createAppUserResult = await _userManager.CreateAsync(userIdentity, toBeCreatedgeneralUser.Password);

                    if (!createAppUserResult.Succeeded)
                    {
                        throw new ArgumentException(createAppUserResult.Errors.FirstOrDefault()?.Description);
                    }

                    // Validate related id's
                    var newGeneralUser = new GeneralUser
                    {
                        Identity = userIdentity,
                        TechLevel = toBeCreatedgeneralUser.TechLevel,
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
                    IGeneralUser = await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(newGeneralUser));

                    // Upload profile image to S3 only after succesfully AppUser creation
                    if (toBeCreatedgeneralUser.ProfileImage != null)
                    {
                        IImage profileImage = await _awsS3Service.UploadProfileImageByUserAsync(toBeCreatedgeneralUser.ProfileImage, userIdentity.Id.ToString());
                        IGeneralUser.ProfileImage = profileImage;
                    }

                    // Commit transaction if all commands succeed, transaction will auto-rollback
                    // when disposed if either commands fails
                    transaction.Complete();
                }
            });

            return IGeneralUser;
        }

        /// <summary>
        /// FindByIdAsync
        /// </summary>
        /// <param name="generalUserId"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IGeneralUser> FindByIdAsync(int generalUserId, ClaimsPrincipal user)
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
                .FirstOrDefault(g => g.Id == generalUserId);

            IGeneralUser IGeneralUser = await Task.FromResult(_mapper.Map<GeneralUser, IGeneralUser>(generalUser));

            // Make call to AWS S3 to see if any profile image is linked to this GeneralUser
            if (IGeneralUser != null)
            {
                IImage profileImage = await _awsS3Service.GetProfileImageByUserAsync(IGeneralUser.IdentityId);
                if (profileImage != null)
                {
                    IGeneralUser.ProfileImage = profileImage;
                }
            }

            return IGeneralUser;
        }

        /// <summary>
        /// FindByIdentityAsync
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

            // Make call to AWS S3 to see if any profile image is linked to this GeneralUser
            if (IGeneralUser != null)
            {
                IImage profileImage = await _awsS3Service.GetProfileImageByUserAsync(appUserId);
                if (profileImage != null)
                {
                    IGeneralUser.ProfileImage = profileImage;
                }
            }

            return IGeneralUser;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IGeneralUser>> FindWithSimilarPreferenceAsync(IGeneralUser loggedInUser, int limit)
        {
            IEnumerable<IGeneralUser> existingUserRequestsUsers = await _buddiesRepository.GetBuddyRequestedUsersByStateAsync(loggedInUser.Id);
            IEnumerable<int> existingUserRequestsUsersIds = existingUserRequestsUsers.Select(x => x.Id);

            Dictionary<int, int[]> techLevelMatchingTable = new Dictionary<int, int[]>();
            techLevelMatchingTable.Add(1, new int[] { 3, 4, (5), (2) });
            techLevelMatchingTable.Add(2, new int[] { 3, (4), (5), (1) });
            techLevelMatchingTable.Add(3, new int[] { 4, 5, (1), (2) });
            techLevelMatchingTable.Add(4, new int[] { 5, (3), (2), (1) });
            techLevelMatchingTable.Add(5, new int[] { 4, 3, (2), (1) });

            int[] techLevelMatchingSequence = techLevelMatchingTable[loggedInUser.TechLevel];

            IEnumerable<IGeneralUser> matchedUsers = _dbContext.GeneralUsers
                .Where(x =>
                    // Match techlevel pre order
                    techLevelMatchingSequence.Contains(x.TechLevel)
                    // Not already have a buddy request initiated
                    && !existingUserRequestsUsersIds.Contains(x.Id)
                    // Not those already seen
                    && !x.HasSeenGeneralUsers.Select(h => h.LoggedInGeneralUserId).Contains(loggedInUser.Id)
                )
                .Include(x => x.Identity)
                .Include(x => x.City)
                .Include(x => x.GeneralUserTeachingAgeGroups)
                    .ThenInclude(t => t.TeachingAgeGroup)
                .Include(x => x.GeneralUserTeachingSubjects)
                    .ThenInclude(t => t.TeachingSubject)
                .Include(x => x.GeneralUserTeachingLevels)
                    .ThenInclude(t => t.TeachingLevel)
                .Include(x => x.GeneralUserInterests)
                    .ThenInclude(t => t.Interest)
                .AsEnumerable()
                // ordering
                .OrderBy(d => techLevelMatchingSequence.IndexOf(d.TechLevel))
                .ThenByDescending(d => d.GeneralUserInterests.Select(g => g.Interest).Where(i => loggedInUser.Interests.Select(x => x.Id).Contains(i.Id)).Count())
                .ThenByDescending(d => d.City.Id == loggedInUser.City.Id)
                .ThenByDescending(d => d.GeneralUserTeachingAgeGroups.Select(g => g.TeachingAgeGroup).Where(i => loggedInUser.TeachingAgeGroups.Select(x => x.Id).Contains(i.Id)).Count())
                .ThenByDescending(d => d.GeneralUserTeachingLevels.Select(g => g.TeachingLevel).Where(i => loggedInUser.TeachingLevels.Select(x => x.Id).Contains(i.Id)).Count())
                .Take(limit)
                .Select(x => _mapper.Map<IGeneralUser>(x))
                .ToList();

            // Flag as seen users
            foreach (IGeneralUser IGeneralUser in matchedUsers)
            {
                // Make call to AWS S3 to see if any profile image is linked to this GeneralUser
                IImage profileImage = await _awsS3Service.GetProfileImageByUserAsync(IGeneralUser.IdentityId);
                if (profileImage != null)
                {
                    IGeneralUser.ProfileImage = profileImage;
                }

                // Flag as seen
                //_dbContext.GeneralUserSeenGeneralUser.Add(new GeneralUserSeenGeneralUser { LoggedInGeneralUserId = loggedInUser.Id, HasSeenGeneralUserId = IGeneralUser.Id });
                //_dbContext.SaveChanges();
            }

            return matchedUsers;
        }
    }
}