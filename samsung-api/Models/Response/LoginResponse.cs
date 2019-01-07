using System;

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

            authToken = jwt.AuthToken;
            expiresIn = jwt.ExpiresIn;
        }

        public string authToken { get; set; }

        public int expiresIn { get; set; }
    }
}