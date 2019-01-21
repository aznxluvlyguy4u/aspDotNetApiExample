using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetTeachingLevelsResponse
    {
        public GetTeachingLevelsResponse()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}