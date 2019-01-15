using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetTeachingSubjectsResponse
    {
        public GetTeachingSubjectsResponse()
        {
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}