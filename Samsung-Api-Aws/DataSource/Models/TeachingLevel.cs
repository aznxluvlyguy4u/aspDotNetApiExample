using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class TeachingLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GeneralUserTeachingLevel> GeneralUserTeachingLevels { get; set; } = new HashSet<GeneralUserTeachingLevel>();
    }
}