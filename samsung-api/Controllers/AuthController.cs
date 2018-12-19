using Microsoft.AspNetCore.Mvc;
using samsung.api.Models;
using samsung.api.Models.Requests;
using samsung.api.Models.Response;
using samsung.api.Services.Auth;
using System.Threading.Tasks;

namespace samsung.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/v1/auth/login
        [HttpPost("login")]
        public async Task<JsonResponse> Post([FromBody]LoginRequest credentials)
        {
            var identity = await _authService.GetClaimsIdentityAsync(credentials.Email, credentials.Password);
            if (identity == null) return new JsonResponse("Invalid email or password.", System.Net.HttpStatusCode.BadRequest);

            var jwt = await _authService.GenerateJwtAsync(identity, credentials.Email);
            var response = new LoginResponse(jwt);
            return new JsonResponse(response, System.Net.HttpStatusCode.OK);
        }
    }
}