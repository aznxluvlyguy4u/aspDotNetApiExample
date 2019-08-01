using samsung.api.DataSource.Models;
using samsung.api.Enumerations;
using SamsungApiAws.DataSource.Models;

namespace samsung_api.DataSource.Models
{
    public class BuddyRequest : BaseEntity
    {
        public int RequestingGeneralUserId { get; set; }
        public int? ReceivingGeneralUserId { get; set; }

        public BuddyRequestState RequestState { get; set; }
        public GeneralUser ReceivingGeneralUser { get; set; }
        public GeneralUser RequestingGeneralUser { get; set; }
    }
}