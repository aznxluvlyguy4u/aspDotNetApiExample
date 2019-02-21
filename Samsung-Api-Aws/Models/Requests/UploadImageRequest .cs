using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung.api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class UploadImageRequest
    {
        public string FileExtension { get; set; }
        public string Body { get; set; }
    }
}