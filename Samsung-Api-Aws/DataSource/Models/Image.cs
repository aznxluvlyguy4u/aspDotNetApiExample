using samsung_api.Models.Interfaces;

namespace samsung.api.DataSource.Models
{
    public class Image : IImage
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string S3Url { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileExtension { get; set; }
    }
}