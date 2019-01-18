﻿using System.Collections.Generic;

namespace samsung_api.Models.Interfaces
{
    public interface IGeneralUser
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string City { get; set; }
        string PhoneNumber { get; set; }
        int TechLevel { get; set; }
        int LinkedInId { get; set; }
        int FacebookId { get; set; }
        string IdentityId { get; set; }
        string Location { get; set; }
        string Locale { get; set; }
        string Gender { get; set; }
        List<int> TeachingSubjects { get; set; }
    }
}