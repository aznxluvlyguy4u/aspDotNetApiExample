using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.Models.Requests
{
    public class GeneralUserCreateRequest
    {
        [JsonRequired]
        public string firstName { get; set; }

        [JsonRequired]
        public string lastName { get; set; }

        [JsonRequired]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [JsonRequired]
        public string password { get; set; }

        [JsonRequired]
        public int techLevel { get; set; }

        public string phoneNumber { get; set; }

        public string city { get; set; }


        //subjects (aparte GET endpoints)

        //interests (aparte GET endpoint)

        //schoolLevel (vwo wo aparte GET endpoint)

        //ageGroup (12+ 16+ etc aparte GET endpoint)

        public int? linkedInId { get; set; }

        public int? facebookId { get; set; }
    }
}