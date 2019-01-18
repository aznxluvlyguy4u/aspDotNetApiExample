using System;

namespace samsung.api.Models
{
    public class JwtToken
    {
        public JwtToken()
        {
        }

        public JwtToken(Guid id, string authToken, int expiresIn)
        {
            Id = id;
            AuthToken = authToken;
            ExpiresIn = expiresIn;
        }

        public Guid Id { get; set; }

        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}