using samsung_api.Models.Interfaces;
using System.Threading.Tasks;

namespace samsung.api.Services.Links
{
    public interface ILinksService
    {
        Task CreateLinkAsync(ILink link);
    }
}