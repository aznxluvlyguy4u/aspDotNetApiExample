using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IImage
    {
        int Id { get; set; }
        string Body { get; set; }
        string S3Url { get; set; }
        string FileName { get; set; }
        string FileSize { get; set; }
        string FileExtension { get; set; }
    }
}