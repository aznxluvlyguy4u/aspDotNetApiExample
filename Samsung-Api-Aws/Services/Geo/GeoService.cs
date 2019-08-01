using System.Collections.Generic;
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

        public Task<string> GetCityNameById(int cityId)
            => _geoRepository.GetCityNameAsync(cityId);

        public async Task<Dictionary<int, string>> GetCountryCitiesAsync(string countryCode, string searchText)
            => await _geoRepository.GetCitiesAsync(countryCode, searchText);
    }
}