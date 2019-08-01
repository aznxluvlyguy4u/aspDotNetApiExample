using Newtonsoft.Json;
using System.Collections.Generic;

namespace samsung_api.Services.Logger
{
    public class SlackPayload
    {
        [JsonProperty("channel")] public string Channel { get; set; }

        [JsonProperty("username")] public string Username { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("attachments")] public IEnumerable<Attachment> Attachments { get; set; }
    }
}