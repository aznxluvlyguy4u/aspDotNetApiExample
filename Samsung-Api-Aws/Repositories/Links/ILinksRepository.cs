using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using samsung_api.Models.Interfaces;

namespace samsung.api.Repositories.Links
{
    public interface ILinksRepository
    {
        Task<ILink> CreateLinkForUserAsync(ILink link, IGeneralUser generalUser);

        Task CreateFavoriteLinkForUserAsync(ILink toBeFavoritedLink, IGeneralUser generalUser);

        Task<IEnumerable<ILink>> GetLinksByUserAsync(IGeneralUser generalUser);

        Task<IEnumerable<ILink>> GetFavoriteLinksByUserAsync(IGeneralUser generalUser);

        Task DeleteLinkForUserByIdAsync(int linkId, IGeneralUser generalUser);

        Task DeleteFavoriteLinkForUserByIdAsync(int linkId, IGeneralUser generalUser);
    }
}