using Newtonsoft.Json;

namespace samsung_api.Models.Interfaces
{
    public interface ILink : ISoftDeletable
    {
        int Id { get; set; }
        string Title { get; set; } 
        string Description { get; set; }
        string Url { get; set; }
        IImage Image { get; set; } // link of base64

        [JsonIgnore]
        IGeneralUser GeneralUser { get; set; }
    }
}