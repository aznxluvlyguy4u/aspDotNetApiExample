using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        Task<IEnumerable<ILink>> FindWithSimilarPreferenceAsync(IGeneralUser loggedInUser, int limit);
    }
}