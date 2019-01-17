using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung_api.Models.Interfaces;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GeneralUserCreateResponse
    {
        public GeneralUserCreateResponse()
        {
        }

        public GeneralUserCreateResponse(IGeneralUser generalUser, JwtToken jwt)
        {
            if (generalUser == null || jwt == null)
            {
                return;
            }

            UserName = generalUser.Email;
            FirstName = generalUser.FirstName;
            LastName = generalUser.LastName;
            AuthToken = jwt.AuthToken;
            ExpiresIn = jwt.ExpiresIn;
        }


        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AuthToken { get; set; }

        public int ExpiresIn { get; set; }

    }
}