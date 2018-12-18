using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using samsung.api.Services.Auth;
using samsung.api.DataSource.Models;
using samsung.api.Models;
using samsung.api.Models.Requests;
using System.Security.Claims;
using System.Threading.Tasks;
using samsung.api.Models.Response;

namespace samsung.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthController(UserManager<AppUser> userManager, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        // POST api/v1/auth/login
        [HttpPost("login")]
        public async Task<JsonResponse> Post([FromBody]LoginRequest credentials)   
        {
            var identity = await GetClaimsIdentity(credentials.Email, credentials.Password);

            if (identity == null)
            {
                return new JsonResponse("Invalid email or password.", System.Net.HttpStatusCode.BadRequest);
            }

            var response = new LoginResponse(identity, _jwtFactory, credentials.Email, _jwtOptions);
            return new JsonResponse(response, System.Net.HttpStatusCode.OK);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = await _userManager.FindByNameAsync(userName);
            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }
    }
}
