using Newtonsoft.Json;

namespace samsung_api.Models.Requests
{
    public class BuddyRequest
    {
        [JsonRequired]
        public int GeneralUserId { get; set; }
    }
}