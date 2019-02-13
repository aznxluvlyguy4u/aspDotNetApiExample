using Newtonsoft.Json;
using samsung.api.Enumerations;

namespace samsung_api.Models.Requests
{
    public class EditBuddyRequest
    {
        [JsonRequired]
        public int RequestingGeneralUserId { get; set; }

        [JsonRequired]
        public bool AcceptBuddyRequest { get; set; }
    }
}