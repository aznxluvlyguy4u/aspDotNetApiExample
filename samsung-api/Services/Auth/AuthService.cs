using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using samsung.api.DataSource.Models;
using samsung.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtIssuerOptions _jwtOptions;

        public AuthService(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, UserManager<AppUser> userManager)
        {
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _userManager = userManager;
        }

        public async Task<JwtToken> GenerateJwtAsync(ClaimsIdentity identity, string userName)
        {
            var Id = new Guid(identity.Claims.Single(c => c.Type == "id").Value);
            var AuthToken = await _jwtFactory.GenerateEncodedTokenAsync(userName, identity);
            var ExpiresIn = (int)_jwtOptions.ValidFor.TotalSeconds;

            return new JwtToken(Id, AuthToken, ExpiresIn);
        }

        public async Task<ClaimsIdentity> GetClaimsIdentityAsync(string userName, string password)
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
