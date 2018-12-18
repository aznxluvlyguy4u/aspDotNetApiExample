using samsung.api.Services.Auth;
using System.Linq;
using System.Security.Claims;

namespace samsung.api.Models.Response
{
    public class LoginResponse
    {
        public LoginResponse()
        {
        }

        public LoginResponse(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions)
        {
            if (identity == null)
            {
                return;
            }

            Id = identity.Claims.Single(c => c.Type == "id").Value;
            AuthToken = jwtFactory.GenerateEncodedTokenAsync(userName, identity).GetAwaiter().GetResult();
            ExpiresIn = (int)jwtOptions.ValidFor.TotalSeconds;
        }

        public string Id { get; set; }

        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}