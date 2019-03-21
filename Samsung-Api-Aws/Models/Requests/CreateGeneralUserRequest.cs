using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.Models.Requests
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CreateGeneralUserRequest
    {
        [JsonRequired]
        [MaxLength(50, ErrorMessage = "FirstName cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [JsonRequired]
        [MaxLength(50, ErrorMessage = "LastName cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [JsonRequired]
        [DataType(DataType.EmailAddress)]
        [MaxLength(254, ErrorMessage = "Email cannot be longer than 254 characters.")]
        public string Email { get; set; }

        [JsonRequired]
        public string Password { get; set; }

        [JsonRequired]
        public int TechLevel { get; set; }

        [MaxLength(50, ErrorMessage = "PhoneNumber cannot be longer than 50 characters.")]
        public string PhoneNumber { get; set; }

        [JsonRequired]
        public int City { get; set; }

        public UploadImageRequest ProfileImage { get; set; }  // base64 encoded string

        [JsonRequired]
        public List<int> TeachingAgeGroups { get; set; }

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