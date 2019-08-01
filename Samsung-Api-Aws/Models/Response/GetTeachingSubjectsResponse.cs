using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetTeachingSubjectsResponse
    {
        public GetTeachingSubjectsResponse()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}