using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CreateBuddyRequest
    {
        [JsonRequired]
        public int GeneralUserId { get; set; }
    }
}