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

        [InverseProperty("RequestingGeneralUser")]
        public ICollection<Buddy> RequestingBuddies { get; set; } = new HashSet<Buddy>();

        [InverseProperty("ReceivingGeneralUser")]
        public ICollection<Buddy> ReceivingBuddies { get; set; } = new HashSet<Buddy>();
    }
}