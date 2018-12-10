using samsung_api.DataSource.Models;
using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class GeneralUser
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  // navigation property
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }

        public IEnumerable<Buddies> RequestingBuddy { get; set; } = new HashSet<Buddies>();
        public IEnumerable<Buddies> ReceivingBuddy { get; set; } = new HashSet<Buddies>();
    }
}