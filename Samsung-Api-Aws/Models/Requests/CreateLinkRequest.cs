using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SamsungApiAws.Controllers
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CreateLinkRequest : ILink
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        bool ISoftDeletable.IsDeleted { get; set; }
    }
}