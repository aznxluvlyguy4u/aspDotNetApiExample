using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CheckExistingEmailResponse
    {
        public CheckExistingEmailResponse()
        {
        }

        public CheckExistingEmailResponse(bool isEmailAvailable)
        {
            IsEmailAvailable = isEmailAvailable;
        }

        public bool IsEmailAvailable { get; set; }
    }
}