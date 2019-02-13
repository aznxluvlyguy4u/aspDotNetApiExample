using System.Threading.Tasks;
using SamsungApiAws.Controllers;

namespace SamsungApiAws.Services.Links
{
    public interface ILinkRepository
    {
        Task CreateLinkAsync(ILink link);
    }
}