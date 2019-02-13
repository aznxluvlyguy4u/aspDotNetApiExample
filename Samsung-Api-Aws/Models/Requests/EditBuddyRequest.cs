using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class EditBuddyRequest
    {
        [JsonRequired]
        public int RequestingGeneralUserId { get; set; }

        [JsonRequired]
        public bool AcceptBuddyRequest { get; set; }
    }
}