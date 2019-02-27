using samsung_api.Models.Interfaces;

namespace SamsungApiAws.DataSource.Models
{
    // TODO: Figure out if this should be saved to DB or not
    public class Feed : IFeed
    {
        public IGeneralUser GeneralUser { get; set; }
        public ILink Link { get; set; }
    }
}