using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Geo
{
    public class GeoService : IGeoService
    {
        private readonly IGeoRepository _geoRepository;

        public GeoService(IGeoRepository geoRepository)
        {
            _geoRepository = geoRepository;
        }

        public async Task<Dictionary<int, string>> GetCountryCitiesAsync(string countryCode, string searchText)
            => await _geoRepository.GetCitiesAsync(countryCode, searchText);
    }
}
