using Newtonsoft.Json;

namespace samsung_api.Models.Requests
{
    public class CreateBuddyRequest
    {
        [JsonRequired]
        public int GeneralUserId { get; set; }
    }
}