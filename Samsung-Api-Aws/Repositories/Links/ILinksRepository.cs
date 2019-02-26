using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using samsung_api.Models.Interfaces;

namespace samsung.api.Repositories.Links
{
    public interface ILinksRepository
    {
        Task<ILink> CreateLinkForUserAsync(ILink link, IGeneralUser user);

        Task CreateFavoriteLinkForUserAsync(ILink toBeFavoritedLink, IGeneralUser user);

        Task<IEnumerable<ILink>> GetLinksByUserAsync(IGeneralUser user);

        Task<IEnumerable<ILink>> GetFavoriteLinksByUserAsync(IGeneralUser user);
    }
}