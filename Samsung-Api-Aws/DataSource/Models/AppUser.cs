using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.DataSource.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        [MaxLength(50, ErrorMessage = "FirstName cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "LastName cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        public int LinkedInId { get; set; }

        public int FacebookId { get; set; }
    }
}