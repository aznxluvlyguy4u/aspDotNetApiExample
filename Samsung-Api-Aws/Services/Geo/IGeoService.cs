using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Geo
{
    public interface IGeoService
    {
        Task<Dictionary<int,string>> GetCountryCitiesAsync(string countryCode, string searchText);
        Task<string> GetCityNameById(int cityId);
    }
}