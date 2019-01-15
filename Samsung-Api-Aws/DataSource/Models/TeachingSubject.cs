﻿using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class TeachingSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<GeneralUserTeachingSubject> GeneralUserTeachingSubjects { get; set; }
    }
}