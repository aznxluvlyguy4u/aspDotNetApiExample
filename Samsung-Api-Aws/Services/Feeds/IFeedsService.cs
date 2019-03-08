using samsung_api.Models.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Feeds
{
    public interface IFeedsService
    {
        Task<IFeed> GetFeedsAsync(ClaimsPrincipal user);
    }
}