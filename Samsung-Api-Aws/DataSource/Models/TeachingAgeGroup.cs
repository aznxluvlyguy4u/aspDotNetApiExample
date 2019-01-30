using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class TeachingAgeGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GeneralUserTeachingAgeGroup> GeneralUserTeachingAgeGroups { get; set; } = new HashSet<GeneralUserTeachingAgeGroup>();
    }
}