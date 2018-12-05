using samsung.api.Controllers;
using samsung.api.DataSource.Models;
using System.Collections.Generic;

namespace samsung_api.DataSource.Models
{
    public class Buddies
    {
        public int RequestingGeneralUserId { get; set; }
        public int ReceivingGeneralUserId { get; set; }

        public BuddyRequestState RequestState { get; set; }

        public GeneralUser ReceivingGeneralUser { get; set; }

        public GeneralUser RequestingGeneralUser { get; set; }
    }
}