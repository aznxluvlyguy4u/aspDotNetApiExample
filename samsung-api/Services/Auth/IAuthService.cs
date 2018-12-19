using samsung.api.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Auth
{
    public interface IAuthService
    {
        Task<JwtToken> GenerateJwtAsync(ClaimsIdentity identity, string userName);

        Task<ClaimsIdentity> GetClaimsIdentityAsync(string userName, string password);
    }
}