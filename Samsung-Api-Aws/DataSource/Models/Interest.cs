using SamsungApiAws.DataSource.Models;
using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class Interest : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<GeneralUserInterest> GeneralUserInterests { get; set; } = new HashSet<GeneralUserInterest>();

        public ICollection<LinkInterest> LinkInterests { get; set; } = new HashSet<LinkInterest>();
    }
}