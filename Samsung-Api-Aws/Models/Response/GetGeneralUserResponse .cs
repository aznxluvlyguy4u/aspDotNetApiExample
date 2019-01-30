using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung_api.Models.Interfaces;
using SamsungApiAws.DataSource.Models;
using System.Collections.Generic;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetGeneralUserResponse
    {
        public GetGeneralUserResponse()
        {
        }

        public GetGeneralUserResponse(IGeneralUser generalUser)
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int TechLevel { get; set; }
        public int LinkedInId { get; set; }
        public int FacebookId { get; set; }
        public string IdentityId { get; set; }
        public string Location { get; set; }
        public string Locale { get; set; }
        public string Gender { get; set; }
        public ICity City { get; set; }
        public ITeachingAgeGroup TeachingAgeGroup { get; set; }
        public List<ITeachingSubject> TeachingSubjects { get; set; }
        public List<ITeachingLevel> TeachingLevels { get; set; }
        public List<IInterest> Interests { get; set; }
    }
}