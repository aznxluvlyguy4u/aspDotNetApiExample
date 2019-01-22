using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Geo
{
    public interface IGeoService
    {
        Task<IEnumerable<string>> GetCountryCitiesAsync(string countryCode);
    }
}