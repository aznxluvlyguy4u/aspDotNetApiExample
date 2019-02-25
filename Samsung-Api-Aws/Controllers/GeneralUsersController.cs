using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using samsung.api.DataSource.Models;
using samsung.api.Models;
using samsung.api.Models.Requests;
using samsung.api.Models.Response;
using samsung.api.Services.Auth;
using samsung.api.Services.AwsS3;
using samsung.api.Services.Buddies;
using samsung.api.Services.GeneralUsers;
using samsung_api.Models.Interfaces;
using samsung_api.Services.Logger;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace samsung_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GeneralUsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGeneralUsersService _generalUsersService;
        private readonly IAuthService _authService;
        private readonly IAwsS3Service _awsS3Service;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;

        public GeneralUsersController(
            IMapper mapper,
            IGeneralUsersService usersService,
            IBuddiesService buddiesService,
            IAuthService authService,
            IAwsS3Service awsS3Service,
            UserManager<AppUser> userManager,
            ILogger logger
        )
        {
            _mapper = mapper;
            _generalUsersService = usersService;
            _authService = authService;
            _awsS3Service = awsS3Service;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// GET generalUser profile of current logged in client
        /// </summary>
        /// <returns></returns>
        [HttpGet("me", Name = "GetMe")]
        public async Task<JsonResponse> GetMeAsync()
        {
            try
            {
                IGeneralUser generalUser = await _generalUsersService.FindByIdentityAsync(base.User);
                var response = _mapper.Map<IGeneralUser, GetGeneralUserResponse>(generalUser);

                return new JsonResponse(response, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex);

                return new JsonResponse(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// GET: api/v1/GeneralUsers/5
        /// GET a generalUser profile by id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<JsonResponse> GetByIdAsync(int id)
        {
            try
            {
                dynamic generalUser = await _generalUsersService.FindByIdAsync(id, base.User);
                var response = _mapper.Map<GetGeneralUserResponse>(generalUser);

                return new JsonResponse(response, System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex);

                return new JsonResponse(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// CREATE a new generalUser profile
        /// </summary>
        /// <param name="createGeneralUserRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResponse> Post([FromBody]CreateGeneralUserRequest createGeneralUserRequest)
        {
            try
            {
                // Create GeneralUser
                var toBeCreatedUser = _mapper.Map<CreateGeneralUserRequest, IGeneralUser>(createGeneralUserRequest);
                var createdUser = await _generalUsersService.CreateGeneralUserAsync(toBeCreatedUser);

                // Create identity and jwt
                JwtToken jwt = null;
                var identity = await _authService.GetClaimsIdentityAsync(createdUser.Email, toBeCreatedUser.Password);
                jwt = await _authService.GenerateJwtAsync(identity, createdUser.Email);

                var response = new CreateGeneralUserResponse(createdUser, jwt);
                return new JsonResponse(response, System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex);

                return new JsonResponse(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Users/5
        /// <summary>
        /// Edit a generalUser profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("images")]
        [AllowAnonymous]
        public async Task<JsonResponse> FindImagesOnUrl(FindImageRequest findImageRequest)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetStringAsync(findImageRequest.Url);
                    if (string.IsNullOrWhiteSpace(result))
                        return new JsonResponse(null, System.Net.HttpStatusCode.NoContent);

                    MatchCollection m1 = Regex.Matches(result, "(?:src|href)=\"(http[s]{0,1}:\\/\\/[^\\s]{1,100}\\.(?:jpg|png|jpeg|gif))\"");

                    var images = m1.Select(m => m.Groups?[1]?.Value ?? null).Where(x => !string.IsNullOrWhiteSpace(x));

                    return new JsonResponse(images, System.Net.HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex);

                return new JsonResponse(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        //[HttpPost("uploadImage")]
        //[AllowAnonymous]
        //public async Task<JsonResponse> UploadImage([FromBody]UploadImageRequest uploadImageRequest)
        //{
        //    try
        //    {
        //        // Map to IImage
        //        IImage toBeUploadedImage = _mapper.Map<UploadImageRequest, IImage>(uploadImageRequest);
        //        var response = await _awsS3Service.UploadProfileImageByUserAsync(toBeUploadedImage, _userManager.GetUserId(base.User));

        //        return new JsonResponse(response, System.Net.HttpStatusCode.OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        await _logger.LogErrorAsync(ex.Message, ex);

        //        return new JsonResponse(ex.Message, System.Net.HttpStatusCode.BadRequest);
        //    }
        //}
    }
}