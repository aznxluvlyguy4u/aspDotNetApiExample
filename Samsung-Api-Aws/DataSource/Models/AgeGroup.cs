using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class AgeGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GeneralUser> GeneralUsers { get; set; }
    }
}