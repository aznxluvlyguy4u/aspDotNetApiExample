using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using samsung.api.DataSource;
using samsung.api.Models;
using samsung.api.Models.Requests;
using samsung.api.Models.Response;
using samsung.api.Services.Auth;
using samsung.api.Services.GeneralUsers;
using samsung_api.Extensions;
using samsung_api.Models.Interfaces;
using samsung_api.Services.Logger;
using System;
using System.Collections.Generic;
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
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly IGeneralUsersService _generalUsersService;
        private readonly IAuthService _authService;
        private readonly ILogger _logger;

        public GeneralUsersController(
            DatabaseContext context,
            IMapper mapper,
            IGeneralUsersService usersService,
            IAuthService authService,
            ILogger logger
        )
        {
            _databaseContext = context;
            _mapper = mapper;
            _generalUsersService = usersService;
            _authService = authService;
            _logger = logger;
        }

        // GET: api/GeneralUsers/me
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

        // POST: api/Users
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

                var response = new GeneralUserCreateResponse(createdUser, jwt);
                return new JsonResponse(response, System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex);

                return new JsonResponse(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Users/5
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

                    MatchCollection m1 = Regex.Matches(result, "(?:src|href)=\"(.*\\.(?:jpg|png|jpeg|gif))\"");

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
    }
}