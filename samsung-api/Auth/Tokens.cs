using Newtonsoft.Json;
using samsung.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Auth
{
    public static class Tokens
    {
        public static async Task<object> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions)
        {
            return new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedTokenAsync(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };
        }
    }
}
