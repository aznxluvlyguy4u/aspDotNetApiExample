using samsung.api.Controllers;
using System.Threading.Tasks;

namespace samsung.api.Repositories.Profiles
{
    public interface IBuddiesRepository
    {
        Task<IBuddy> GetPendingBuddyRequests();
        Task CreateBuddyRequestAsync(int requestingUserId, int receivingUserId);
    }
}