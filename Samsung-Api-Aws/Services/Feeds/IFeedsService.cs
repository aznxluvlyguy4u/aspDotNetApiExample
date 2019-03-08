using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Feeds
{
    public interface IFeedsService
    {
        Task<IFeed> GetFeedsAsync(ClaimsPrincipal user);
    }
}