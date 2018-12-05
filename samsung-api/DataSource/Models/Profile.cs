using Microsoft.AspNetCore.Identity;
using samsung_api.DataSource.Models;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class Profile : IdentityUser<int>, IProfile
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public int TechLevel { get; set; }

        public int LinkedInId { get; set; }

        public int FacebookId { get; set; }

        public int? ImageId { get; set; }

        public Image Image { get; set; }

        public IEnumerable<Buddies> RequestingBuddy { get; set; } = new HashSet<Buddies>();
        public IEnumerable<Buddies> ReceivingBuddy { get; set; } = new HashSet<Buddies>();
    }
}