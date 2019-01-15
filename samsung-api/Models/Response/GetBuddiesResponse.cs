using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetBuddiesResponse
    {
        public GetBuddiesResponse()
        {
        }

        public IEnumerable<IBuddy> IncomingRequests { get; set; }
        public IEnumerable<IBuddy> OutGoingRequests { get; set; }
        public IEnumerable<IBuddy> Matched { get; set; }
    }
}