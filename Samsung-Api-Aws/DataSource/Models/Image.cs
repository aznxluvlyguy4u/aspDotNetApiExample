using samsung_api.Models.Interfaces;

namespace SamsungApiAws.DataSource.Models
{
    public class Image : IImage
    {
        public string Body { get; set; }
        public string S3Url { get; set; }
        public string FileExtension { get; set; }
    }
}