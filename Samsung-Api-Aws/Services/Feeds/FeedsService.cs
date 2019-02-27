using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamsungApiAws.Services.Feeds
{
    public class FeedsService : IFeedsService
    {
        public FeedsService()
        {
        }

        Task<IEnumerable<IFeed>> IFeedsService.GetFeedsAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}