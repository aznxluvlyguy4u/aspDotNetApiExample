using samsung_api.Models.Interfaces;

namespace samsung.api.DataSource.Models
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