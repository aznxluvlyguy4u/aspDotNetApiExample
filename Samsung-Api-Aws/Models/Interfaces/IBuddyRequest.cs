using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung.api.Enumerations;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IBuddyRequest
    {
        BuddyRequestState RequestState { get; set; }
        IGeneralUser ReceivingGeneralUser { get; set; }
        IGeneralUser RequestingGeneralUser { get; set; }
    }
}