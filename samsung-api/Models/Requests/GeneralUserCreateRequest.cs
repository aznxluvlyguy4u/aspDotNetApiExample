using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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



        //subjects (aparte GET endpoints)

        //interests (aparte GET endpoint)

        //schoolLevel (vwo wo aparte GET endpoint)

        //ageGroup (12+ 16+ etc aparte GET endpoint)

        public int? LinkedInId { get; set; }

        public int? FacebookId { get; set; }
    }
}