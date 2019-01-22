using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Geo
{
    public interface IGeoRepository
    {
        Task<IEnumerable<string>> GetCitiesAsync(string countryCode);
    }
}