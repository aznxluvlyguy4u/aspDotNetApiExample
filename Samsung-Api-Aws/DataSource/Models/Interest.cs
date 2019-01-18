using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class Interest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<GeneralUserInterest> GeneralUserInterests { get; set; }
    }
}