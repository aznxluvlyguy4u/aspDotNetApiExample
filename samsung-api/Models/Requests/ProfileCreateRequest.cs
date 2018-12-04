using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.Models.Requests
{
    public class ProfileCreateRequest
    {
        [JsonRequired]
        public string FirstName { get; set; }

        [JsonRequired]
        public string LastName { get; set; }

        [JsonRequired]
        public string EmailAddress { get; set; }

        [JsonRequired]
        public string Password { get; set; }

        [JsonRequired]
        public int TechLevel { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public int? ImageId { get; set; }

        public int? LinkedInId { get; set; }

        public int? FacebookId { get; set; }
    }
}
