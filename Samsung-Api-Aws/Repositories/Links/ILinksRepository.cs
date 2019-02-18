using System.Threading.Tasks;
using samsung_api.Models.Interfaces;

namespace samsung.api.Repositories.Links
{
    public interface ILinksRepository
    {
        Task CreateLinkAsync(ILink link);
    }
}