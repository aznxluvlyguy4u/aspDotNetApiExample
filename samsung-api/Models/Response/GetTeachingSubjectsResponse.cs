using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung_api.Models.Interfaces;
using System.Collections.Generic;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetTeachingSubjectsResponse
    {
        public GetTeachingSubjectsResponse()
        {
        }

        //public GeneralUserCreateResponse(IGeneralUser generalUser, JwtToken jwt)
        //{
        //    if (generalUser == null || jwt == null)
        //    {
        //        return;
        //    }

        //    userName = generalUser.email;
        //    firstName = generalUser.firstName;
        //    lastName = generalUser.lastName;
        //    authToken = jwt.AuthToken;
        //    expiresIn = jwt.ExpiresIn;
        //}

        public string Id { get; set; }

        public string Name { get; set; }
    }
}