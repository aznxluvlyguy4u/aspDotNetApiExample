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

        // GET: api/GeneralUsers
        [HttpGet]
        public string GetAll()
        {
            var wtf = "fu".ToJson();

            return wtf;
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
                _logger.LogErrorAsync(ex.Message, ex).GetAwaiter().GetResult();

                return new JsonResponse(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }

        // GET: api/GeneralUsers/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResponse> Post([FromBody] GeneralUserCreateRequest generalUserCreateRequest)
        {
            try
            {
                // Create GeneralUser
                var toBeCreatedUser = _mapper.Map<GeneralUserCreateRequest, IGeneralUser>(generalUserCreateRequest);
                var createdUser = await _generalUsersService.CreateGeneralUserAsync(toBeCreatedUser);

                // Create identity and jwt
                JwtToken jwt = null;
                if (createdUser != null)
                {
                    var identity = await _authService.GetClaimsIdentityAsync(createdUser.Email, toBeCreatedUser.Password);
                    jwt = await _authService.GenerateJwtAsync(identity, createdUser.Email);
                }

                var response = new GeneralUserCreateResponse(createdUser, jwt);
                return new JsonResponse(response, System.Net.HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogErrorAsync(ex.Message, ex).GetAwaiter().GetResult();

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
    }
}