using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung.api.Enumerations;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetFeedResponse
    {
        public GetFeedResponse()
        {
        }

        public GetFeedResponse(IFeed feed)
        {
        }

        public FeedType Type { get; set; }
        public dynamic Body { get; set; }
    }
}