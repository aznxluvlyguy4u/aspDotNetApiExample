using Microsoft.AspNetCore.Identity;
using System;

namespace samsung.api.DataSource.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int LinkedInId { get; set; }

        public int FacebookId { get; set; }
    }
}