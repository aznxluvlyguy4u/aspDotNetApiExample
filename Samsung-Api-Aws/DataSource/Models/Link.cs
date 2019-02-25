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
        public bool IsDeleted { get; set; }

        public int? GeneralUserId { get; set; }
        public GeneralUser GeneralUser { get; set; }

        public ICollection<GeneralUserLink> GeneralUserLinks { get; set; } = new HashSet<GeneralUserLink>();
    }
}