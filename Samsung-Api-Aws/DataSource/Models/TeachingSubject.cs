using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class TeachingSubject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GeneralUserTeachingSubject> GeneralUserTeachingSubjects { get; set; } = new HashSet<GeneralUserTeachingSubject>();
    }
}