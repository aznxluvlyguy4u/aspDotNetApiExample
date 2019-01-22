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

        public async Task<IEnumerable<string>> GetCountryCitiesAsync(string countryCode)
            => await _geoRepository.GetCitiesAsync(countryCode);
    }
}
