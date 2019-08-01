using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung.api.Enumerations;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IImage
    {
        UploadImageType ImageType { get; set; }
        string ImageWebUrl { get; set; }
        string Body { get; set; }
        string Url { get; set; }
        string FileExtension { get; set; }
    }
}