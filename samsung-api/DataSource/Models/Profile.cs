using Microsoft.AspNetCore.Identity;
using samsung_api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.DataSource.Models
{
    public class Profile : IdentityUser, IProfile
    {
        public int Id { get; set; }

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
    }
}
