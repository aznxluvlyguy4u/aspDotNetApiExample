using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CreateLinkRequest : ISoftDeletable
    {
        [JsonRequired]
        [RegularExpression(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$",
         ErrorMessage = "Invalid url.")]
        public string Url { get; set; }

        [JsonRequired]
        public string Title { get; set; }

        public UploadLinkImageRequest LinkImage { get; set; }

        [JsonRequired]
        public string Description { get; set; }

        [JsonRequired]
        public List<int> Interests { get; set; }

        bool ISoftDeletable.IsDeleted { get; set; }
    }
}