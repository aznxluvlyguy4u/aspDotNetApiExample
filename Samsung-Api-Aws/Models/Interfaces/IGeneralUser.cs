using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung.api.DataSource.Models;
using SamsungApiAws.DataSource.Models;
using System.Collections.Generic;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface IGeneralUser
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string PhoneNumber { get; set; }
        int TechLevel { get; set; }
        int LinkedInId { get; set; }
        int FacebookId { get; set; }
        string IdentityId { get; set; }
        string Location { get; set; }
        string Locale { get; set; }
        string Gender { get; set; }
        ICity City { get; set; }
        List<ITeachingAgeGroup> TeachingAgeGroups { get; set; }
        List<ITeachingSubject> TeachingSubjects { get; set; }
        List<ITeachingLevel> TeachingLevels { get; set; }
        List<IInterest> Interests { get; set; }
    }
}