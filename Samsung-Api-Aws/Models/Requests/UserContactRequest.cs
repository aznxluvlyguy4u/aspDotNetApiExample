using Newtonsoft.Json;

namespace samsung_api.Models.Requests
{
    public class BuddyRequest
    {
        [JsonRequired]
        public int UserId { get; set; }
    }
}