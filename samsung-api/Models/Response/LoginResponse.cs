using samsung.api.Services.Auth;
using System;
using System.Linq;
using System.Security.Claims;

namespace samsung.api.Models.Response
{
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

            Id = jwt.Id;
            AuthToken = jwt.AuthToken;
            ExpiresIn = jwt.ExpiresIn;
        }

        public Guid Id { get; set; }

        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}