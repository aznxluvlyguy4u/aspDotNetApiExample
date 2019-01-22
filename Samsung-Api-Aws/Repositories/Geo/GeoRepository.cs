using AutoMapper;
using samsung.api.DataSource;
using SamsungApiAws.Services.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsungApiAws.Repositories.Geo
{
    public class GeoRepository : IGeoRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public GeoRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<string>> GetCitiesAsync(string countryCode)
        {
            return _dbContext.Cities
                .Where(x => x.CountryCode == countryCode)
                .Select(x => x.CityAccentName)
                .ToList();
        }
    }
}
