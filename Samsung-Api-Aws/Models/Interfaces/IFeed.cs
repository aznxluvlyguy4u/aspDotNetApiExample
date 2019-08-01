using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung.api.Enumerations;
using System.Collections.Generic;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IFeed
    {
        FeedType Type { get; set; }
        dynamic Body { get; set; }
    }
}