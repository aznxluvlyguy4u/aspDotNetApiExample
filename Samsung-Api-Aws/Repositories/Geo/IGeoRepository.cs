using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Geo
{
    public interface IGeoRepository
    {
        Task<Dictionary<int, string>> GetCitiesAsync(string countryCode, string searchText);
    }
}