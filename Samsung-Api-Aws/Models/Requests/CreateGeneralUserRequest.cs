using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.Models.Requests
{
    public class CreateGeneralUserRequest
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

        [JsonRequired]
        public int City { get; set; }

        [JsonRequired]
        public int TeachingAgeGroup { get; set; }

        [JsonRequired]
        public List<int> TeachingSubjects { get; set; }

        [JsonRequired]
        public List<int> TeachingLevels { get; set; }

        [JsonRequired]
        public List<int> Interests { get; set; }

        public int? LinkedInId { get; set; }

        public int? FacebookId { get; set; }
    }
}