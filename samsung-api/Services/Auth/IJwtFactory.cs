using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Auth
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedTokenAsync(string userName, ClaimsIdentity identity);

        ClaimsIdentity GenerateClaimsIdentity(string userName, Guid guid);
    }
}