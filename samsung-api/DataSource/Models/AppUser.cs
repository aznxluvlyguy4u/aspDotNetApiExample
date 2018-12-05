using Microsoft.AspNetCore.Identity;
using samsung_api.DataSource.Models;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;

namespace samsung.api.DataSource.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public int TechLevel { get; set; }

        public int LinkedInId { get; set; }

        public int FacebookId { get; set; }

        public int? ImageId { get; set; }

        public Image Image { get; set; }
    }
}