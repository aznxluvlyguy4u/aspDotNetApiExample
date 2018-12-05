using samsung.api.Controllers;
using samsung.api.DataSource.Models;

namespace samsung_api.DataSource.Models
{
    public class Buddies
    {
        public int RequestingProfileId { get; set; }
        public int ReceivingProfileId { get; set; }

        public ContactRequestState RequestState { get; set; }

        public Profile RequestingProfile { get; set; }
        public Profile ReceivingProfile { get; set; }
    }
}