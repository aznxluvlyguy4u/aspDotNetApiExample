using samsung_api.DataSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.DataSource.Models
{
    public class GeneralUser
    {
        public int Id { get; set; }
        public Guid IdentityId { get; set; }
        public virtual AppUser Identity { get; set; }  // navigation property
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }

        public IEnumerable<Buddies> RequestingBuddy { get; set; } = new HashSet<Buddies>();
        public IEnumerable<Buddies> ReceivingBuddy { get; set; } = new HashSet<Buddies>();
    }
}
