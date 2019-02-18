﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using samsung.api.DataSource;
using samsung.api.DataSource.Models;
using samsung_api.Models.Interfaces;

namespace samsung.api.Repositories.Links
{
    public class LinksRepository : ILinksRepository
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