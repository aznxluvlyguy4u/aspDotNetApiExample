using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IFeed
    {
        IGeneralUser GeneralUser { get; set; }

        ILink Link { get; set; }
    }
}