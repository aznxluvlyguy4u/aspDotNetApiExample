using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using samsung.api.Models;
using samsung.api.Models.Requests;
using samsung.api.Models.Response;
using samsung.api.Services.Auth;
using samsung.api.Services.GeneralUsers;
using samsung_api.Services.Logger;
using System;
using System.Net;
using System.Threading.Tasks;

namespace samsung.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IGeneralUsersService _generalUsersService;
        private readonly ILogger _logger;

        public AuthController(ILogger logger, IAuthService authService, IGeneralUsersService generalUsersService)
        {
            _authService = authService;
            _generalUsersService = generalUsersService;
            _logger = logger;
        }

        // POST api/v1/auth/login
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponse(typeof(SwaggerSuccess<LoginResponse>), HttpStatusCode.OK)]
        [ProducesResponse(typeof(SwaggerError), HttpStatusCode.BadRequest)]
        public async Task<JsonResponse> LoginAsync([FromBody]LoginRequest credentials)
        {
            var identity = await _authService.GetClaimsIdentityAsync(credentials.Email, credentials.Password);
            if (identity == null) return new JsonResponse("Invalid email or password.", HttpStatusCode.Unauthorized);

            try
            {
                var jwt = await _authService.GenerateJwtAsync(identity, credentials.Email);
                //var generalUser = await _generalUsersService.FindByIdentity

                var response = new LoginResponse(jwt);
                return new JsonResponse(response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        // POST api/v1/auth/checkEmail
        [HttpPost("checkEmail")]
        [AllowAnonymous]
        public async Task<JsonResponse> CheckEmailAsync([FromBody]CheckExistingEmailRequest request)
        {
            try
            {
                bool isEmailAvailable = await _authService.IsEmailAvailable(request.Email);
                var response = new CheckExistingEmailResponse(isEmailAvailable);

                return new JsonResponse(response, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(ex.Message, ex).ConfigureAwait(false);
                // TODO: When creating a release, don't send ex.Message in response
                return new JsonResponse(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}