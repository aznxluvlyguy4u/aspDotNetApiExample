using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace samsung_api.Models.Interfaces
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public interface ILimitedGeneralUser
    {
        int Id { get; set; }
        string FirstName { get; set; }
        int LinkedInId { get; set; }
        int FacebookId { get; set; }
        string IdentityId { get; set; }
        string Location { get; set; }
        string Locale { get; set; }
        string Gender { get; set; }
        ICity City { get; set; }
        IImage ProfileImage { get; set; }
        List<ITeachingAgeGroup> TeachingAgeGroups { get; set; }
        List<ITeachingSubject> TeachingSubjects { get; set; }
        List<ITeachingLevel> TeachingLevels { get; set; }
        List<IInterest> Interests { get; set; }
        List<ILink> Links { get; set; }
    }
}