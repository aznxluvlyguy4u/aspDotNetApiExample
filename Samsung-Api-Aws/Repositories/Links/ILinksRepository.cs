using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using samsung_api.Models.Interfaces;

namespace samsung.api.Repositories.Links
{
    public interface ILinksRepository
    {
        Task<ILink> CreateLinkAsync(ILink link, IGeneralUser user);

        Task<IEnumerable<ILink>> GetLinksByUserAysnc(int generalUserId);
    }
}