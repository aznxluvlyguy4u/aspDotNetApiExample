using System.Threading.Tasks;

namespace SamsungApiAws.Controllers
{
    public interface ILinkService
    {
        Task CreateLinkAsync(ILink link);
    }
}