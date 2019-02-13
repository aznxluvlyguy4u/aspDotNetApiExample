using samsung.api.DataSource.Models;
using samsung.api.Enumerations;

namespace samsung_api.DataSource.Models
{
    public class Buddy
    {
        public int RequestingGeneralUserId { get; set; }
        public int? ReceivingGeneralUserId { get; set; }

        public BuddyRequestState RequestState { get; set; }
        public GeneralUser ReceivingGeneralUser { get; set; }
        public GeneralUser RequestingGeneralUser { get; set; }
    }
}