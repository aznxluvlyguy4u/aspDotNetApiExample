using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IImage
    {
        string Body { get; set; }
        string S3Url { get; set; }
        string FileExtension { get; set; }
    }
}