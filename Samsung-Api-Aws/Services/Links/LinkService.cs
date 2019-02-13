using SamsungApiAws.Controllers;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Links
{
    public class LinkService : ILinkService
    {
        private readonly ILinkRepository _linkRepository;

        public LinkService(ILinkRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public Task CreateLinkAsync(ILink link)
            => _linkRepository.CreateLinkAsync(link);
    }
}
