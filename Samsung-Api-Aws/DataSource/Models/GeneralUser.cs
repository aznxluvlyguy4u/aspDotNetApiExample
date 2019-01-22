using samsung_api.DataSource.Models;
using SamsungApiAws.DataSource.Models;
using System;
using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class GeneralUser
    {
        public int Id { get; set; }
        public Guid IdentityId { get; set; }
        public virtual AppUser Identity { get; set; }  // navigation property
        public int? AgeGroupId { get; set; }
        public virtual AgeGroup AgeGroup { get; set; } // navigation property
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }

        public int CitiesId { get; set; }
        public virtual City City { get; set; } // navigation property

        public ICollection<GeneralUserTeachingSubject> GeneralUserTeachingSubjects { get; set; } = new HashSet<GeneralUserTeachingSubject>();
        public ICollection<GeneralUserTeachingLevel> GeneralUserTeachingLevels { get; set; } = new HashSet<GeneralUserTeachingLevel>();
        public ICollection<GeneralUserInterest> GeneralUserInterests { get; set; } = new HashSet<GeneralUserInterest>();
        public ICollection<Buddy> RequestingBuddy { get; set; } = new HashSet<Buddy>();
        public ICollection<Buddy> ReceivingBuddy { get; set; } = new HashSet<Buddy>();
    }
}