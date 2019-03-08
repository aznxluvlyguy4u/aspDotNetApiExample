using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IFeed
    {
        IGeneralUser MatchedGeneralUser { get; set; }

        IEnumerable<ILink> MatchedLinks { get; set; }
    }
}