using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung.api.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class UploadLinkImageRequest
    {
        [JsonRequired]
        public UploadImageType ImageType { get; set; }

        [RegularExpression(@"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$",
         ErrorMessage = "Invalid url.")]
        public string ImageWebUrl { get; set; }

        public UploadImageRequest Base64Image { get; set; }
    }
}