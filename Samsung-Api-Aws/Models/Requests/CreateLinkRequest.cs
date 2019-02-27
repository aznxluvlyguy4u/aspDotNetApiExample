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
        [RegularExpression(@"^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$",
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