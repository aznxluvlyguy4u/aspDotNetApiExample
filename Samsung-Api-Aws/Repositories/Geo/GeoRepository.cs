using AutoMapper;
using samsung.api.DataSource;
using samsung.api.Extensions;
using SamsungApiAws.Services.Geo;
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

        public async Task<Dictionary<int, string>> GetCitiesAsync(string countryCode, string searchText)
        {
            var cities = _dbContext.Cities
                .Where(x =>
                    x.CountryCode == countryCode
                    &&
                    (
                        x.CityName.StartsWith(searchText.ToLowerInvariant())
                        || x.CityAccentName.StartsWith(searchText)
                    )
                )
                .Select(x => new { x.CityAccentName, x.Id });
            var dict = new Dictionary<int, string>();
            cities.ForEach(x => dict.Add(x.Id, x.CityAccentName));
            return dict;
        }

        public async Task<string> GetCityNameAsync(int cityId)
        {
            return _dbContext.Cities.FirstOrDefault(x => x.Id == cityId)?.CityAccentName;
        }
    }
}