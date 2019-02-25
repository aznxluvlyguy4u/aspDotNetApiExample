using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class UploadImageRequest
    {
        [JsonRequired]
        [RegularExpression(@"^jpg|png|jpeg$",
         ErrorMessage = "Extension not supported.")]
        public string FileExtension { get; set; }

        [JsonRequired]
        public string Body { get; set; }
    }
}