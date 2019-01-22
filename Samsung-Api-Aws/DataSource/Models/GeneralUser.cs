﻿using samsung_api.DataSource.Models;
using SamsungApiAws.DataSource.Models;
using System;
using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class GeneralUser
    {
        public int Id { get; set; }

        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }

        public virtual City City { get; set; }
        public virtual AppUser Identity { get; set; }  
        public virtual TeachingAgeGroup TeachingAgeGroup { get; set; }

        public ICollection<GeneralUserTeachingSubject> GeneralUserTeachingSubjects { get; set; } = new HashSet<GeneralUserTeachingSubject>();
        public ICollection<GeneralUserTeachingLevel> GeneralUserTeachingLevels { get; set; } = new HashSet<GeneralUserTeachingLevel>();
        public ICollection<GeneralUserInterest> GeneralUserInterests { get; set; } = new HashSet<GeneralUserInterest>();
        public ICollection<Buddy> RequestingBuddy { get; set; } = new HashSet<Buddy>();
        public ICollection<Buddy> ReceivingBuddy { get; set; } = new HashSet<Buddy>();
    }
}