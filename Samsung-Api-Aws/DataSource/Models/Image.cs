using samsung.api.Enumerations;
using samsung_api.Models.Interfaces;

namespace SamsungApiAws.DataSource.Models
{
    // TODO: Figure out if this should be saved to DB or not
    public class Image : IImage
    {
        public string Body { get; set; }
        public string Url { get; set; }
        public string FileExtension { get; set; }
        public UploadImageType ImageType { get; set; }
        public string ImageWebUrl { get; set; }
    }
}