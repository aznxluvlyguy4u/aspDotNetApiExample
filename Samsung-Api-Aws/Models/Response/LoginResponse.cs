using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class LoginResponse
    {
        public LoginResponse()
        {
        }

        public LoginResponse(JwtToken jwt)
        {
            if (jwt == null)
            {
                return;
            }

            AuthToken = jwt.AuthToken;
            ExpiresIn = jwt.ExpiresIn;
        }

        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}