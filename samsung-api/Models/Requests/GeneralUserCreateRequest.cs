using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.Models.Requests
{
    public class GeneralUserCreateRequest
    {
        [JsonRequired]
        public string FirstName { get; set; }

        [JsonRequired]
        public string LastName { get; set; }

        [JsonRequired]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [JsonRequired]
        public string Password { get; set; }

        [JsonRequired]
        public int TechLevel { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public int? ImageId { get; set; }

        public int? LinkedInId { get; set; }

        public int? FacebookId { get; set; }
    }
}
