using samsung_api.DataSource.Models;
using SamsungApiAws.DataSource.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace samsung.api.DataSource.Models
{
    public class GeneralUser
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 5)]
        public int TechLevel { get; set; }
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }

        [Required]
        public virtual City City { get; set; }

        [Required]
        public virtual AppUser Identity { get; set; }

        public ICollection<GeneralUserTeachingAgeGroup> GeneralUserTeachingAgeGroups { get; set; } = new HashSet<GeneralUserTeachingAgeGroup>();
        public ICollection<GeneralUserTeachingSubject> GeneralUserTeachingSubjects { get; set; } = new HashSet<GeneralUserTeachingSubject>();
        public ICollection<GeneralUserTeachingLevel> GeneralUserTeachingLevels { get; set; } = new HashSet<GeneralUserTeachingLevel>();
        public ICollection<GeneralUserInterest> GeneralUserInterests { get; set; } = new HashSet<GeneralUserInterest>();
        public ICollection<Link> Links { get; set; } = new HashSet<Link>();


        public ICollection<FavoriteLink> FavoriteLinks { get; set; } = new HashSet<FavoriteLink>();

        [InverseProperty("RequestingGeneralUser")]
        public ICollection<BuddyRequest> RequestingBuddies { get; set; } = new HashSet<BuddyRequest>();

        [InverseProperty("ReceivingGeneralUser")]
        public ICollection<BuddyRequest> ReceivingBuddies { get; set; } = new HashSet<BuddyRequest>();

        [InverseProperty("LoggedInGeneralUser")]
        public ICollection<GeneralUserSeenGeneralUser> LoggedInGeneralUsers { get; set; } = new HashSet<GeneralUserSeenGeneralUser>();

        [InverseProperty("HasSeenGeneralUser")]
        public ICollection<GeneralUserSeenGeneralUser> HasSeenGeneralUsers { get; set; } = new HashSet<GeneralUserSeenGeneralUser>();

        public ICollection<GeneralUserSeenLink> GeneralUserSeenLinks { get; set; } = new HashSet<GeneralUserSeenLink>();
    }
}