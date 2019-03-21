using samsung.api.Enumerations;
using samsung_api.Models.Interfaces;
using SamsungApiAws.DataSource.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.DataSource.Models
{
    public class Link : BaseEntity, ISoftDeletable
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(60, ErrorMessage = "Title cannot be longer than 60 characters.")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)?[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$",
         ErrorMessage = "Invalid url.")]
        [MaxLength(2048, ErrorMessage = "Url cannot be longer than 2048 characters.")]
        public string Url { get; set; }

        [Required]
        public UploadImageType ImageType { get; set; }

        public string ImageWebUrl { get; set; }
        public bool IsDeleted { get; set; }

        public int? GeneralUserId { get; set; }
        public GeneralUser GeneralUser { get; set; }

        public ICollection<FavoriteLink> FavoriteLinks { get; set; } = new HashSet<FavoriteLink>();

        public ICollection<LinkInterest> LinkInterests { get; set; } = new HashSet<LinkInterest>();

        public ICollection<GeneralUserSeenLink> GeneralUserSeenLinks { get; set; } = new HashSet<GeneralUserSeenLink>();
    }
}