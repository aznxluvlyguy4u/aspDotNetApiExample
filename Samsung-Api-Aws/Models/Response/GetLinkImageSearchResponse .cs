using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetLinkImageSearchResponse
    {
        public GetLinkImageSearchResponse()
        {
        }

        public GetLinkImageSearchResponse(string url)
        {
        }

        public string Url { get; set; }
    }
}