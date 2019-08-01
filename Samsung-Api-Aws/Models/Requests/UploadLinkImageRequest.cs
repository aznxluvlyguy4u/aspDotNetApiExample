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

        [RegularExpression(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$",
         ErrorMessage = "Invalid image url.")]
        public string ImageWebUrl { get; set; }

        public UploadImageRequest Base64Image { get; set; }
    }
}