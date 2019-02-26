using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CreateFavoriteLinkRequest
    {
        [JsonRequired]
        public int LinkId { get; set; }
    }
}