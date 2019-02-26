using Newtonsoft.Json;
using samsung.api.Enumerations;

namespace samsung_api.Models.Interfaces
{
    public interface ILink : ISoftDeletable
    {
        int Id { get; set; }
        string Title { get; set; } 
        string Description { get; set; }
        string Url { get; set; }
        UploadImageType ImageType { get; set; }
        string ImageWebUrl { get; set; }
        IImage Image { get; set; } // link of base64

        [JsonIgnore]
        IGeneralUser GeneralUser { get; set; }
    }
}