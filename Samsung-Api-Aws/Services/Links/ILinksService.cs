using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace samsung.api.Services.Links
{
    public interface ILinksService
    {
        Task<ILink> CreateLinkAsync(ILink link, ClaimsPrincipal user);

        Task CreateFavoriteLinkAsync(ILink link, ClaimsPrincipal user);

        Task<IEnumerable<ILink>> GetMyLinksAsync(ClaimsPrincipal user);
    }
}