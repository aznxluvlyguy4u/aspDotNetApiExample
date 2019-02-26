using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class UploadLinkImageRequest
    {
        [JsonRequired]
        [RegularExpression(@"^base64|url$",
         ErrorMessage = "Upload type not supported.")]
        public string UploadType { get; set; }

        [JsonRequired]
        [RegularExpression(@"^jpg|png|jpeg$",
         ErrorMessage = "Extension not supported.")]
        public string FileExtension { get; set; }

        [JsonRequired]
        public string Body { get; set; }
    }
}