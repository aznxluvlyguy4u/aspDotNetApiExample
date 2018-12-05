using Newtonsoft.Json;

namespace samsung_api.Models.Requests
{
    public class UserContactRequest
    {
        [JsonRequired]
        public int UserId { get; set; }
    }
}