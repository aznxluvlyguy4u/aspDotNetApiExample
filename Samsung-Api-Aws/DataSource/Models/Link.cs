using samsung.api.Enumerations;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.DataSource.Models
{
    public class Link : ISoftDeletable
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public UploadImageType ImageType { get; set; }
        public string ImageWebUrl { get; set; }
        public bool IsDeleted { get; set; }

        public int? GeneralUserId { get; set; }
        public GeneralUser GeneralUser { get; set; }

        public ICollection<FavoriteLink> FavoriteLinks { get; set; } = new HashSet<FavoriteLink>();

        public ICollection<LinkInterest> LinkInterests { get; set; } = new HashSet<LinkInterest>();
    }
}