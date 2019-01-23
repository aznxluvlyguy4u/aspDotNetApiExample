using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Controllers
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class FindImageRequest
    {
        public string Url { get; set; }
    }
}