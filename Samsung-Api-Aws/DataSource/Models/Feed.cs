using System.Collections.Generic;
using samsung_api.Models.Interfaces;

namespace SamsungApiAws.DataSource.Models
{
    // TODO: Figure out if this should be saved to DB or not
    public class Feed : IFeed
    {
        public IGeneralUser MatchedGeneralUser { get; set; }
        public IEnumerable<ILink> MatchedLinks { get; set; }
    }
}