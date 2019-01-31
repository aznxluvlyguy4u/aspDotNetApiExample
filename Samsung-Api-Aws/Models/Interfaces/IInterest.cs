using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IInterest
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}