using samsung.api.Repositories.Links;
using samsung_api.Models.Interfaces;
using System.Threading.Tasks;

namespace samsung.api.Services.Links
{
    public class LinksService : ILinksService
    {
        private readonly ILinksRepository _linkRepository;

        public LinksService(ILinksRepository linkRepository)
        {
            _linkRepository = linkRepository;
        }

        public Task CreateLinkAsync(ILink link)
            => _linkRepository.CreateLinkAsync(link);
    }
}
