using SamsungApiAws.Controllers;

namespace SamsungApiAws.Models
{
    public class Link : ILink
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsDeleted { get; set; }
    }
}