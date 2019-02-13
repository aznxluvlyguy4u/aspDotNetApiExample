using System;
using System.Threading.Tasks;
using AutoMapper;
using samsung.api.DataSource;
using SamsungApiAws.Controllers;
using SamsungApiAws.Models;
using SamsungApiAws.Services.Links;

namespace SamsungApiAws.Repositories.Links
{
    public class LinksRepository : ILinkRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;

        public LinksRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task CreateLinkAsync(ILink link)
        {
            var dbLink = link as Link;
            if (dbLink == default)
                throw new ArgumentNullException(nameof(dbLink));
            _databaseContext.Links.Add(dbLink);
            await Task.CompletedTask;
        }
    }
}