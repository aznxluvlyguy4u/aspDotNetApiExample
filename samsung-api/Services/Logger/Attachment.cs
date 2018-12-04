using Newtonsoft.Json;
using System.Collections.Generic;

namespace samsung_api.Services.Logger
{
    public class Attachment
    {
        [JsonProperty("title")] public string Title { set; get; }

        [JsonProperty("fallback")] public string Fallback { set; get; }

        [JsonProperty("pretext")] public string Pretext { set; get; }

        [JsonProperty("text")] public string Text { set; get; }

        [JsonProperty("color")] public string Color { set; get; }

        [JsonProperty("fields")] public IEnumerable<Field> Fields { set; get; }
    }
}