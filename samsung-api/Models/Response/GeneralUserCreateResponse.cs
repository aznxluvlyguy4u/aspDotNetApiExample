using samsung_api.Models.Interfaces;

namespace samsung.api.Models.Response
{
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

            userName = generalUser.email;
            firstName = generalUser.firstName;
            lastName = generalUser.lastName;
            authToken = jwt.AuthToken;
            expiresIn = jwt.ExpiresIn;
        }


        public string userName { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string authToken { get; set; }

        public int expiresIn { get; set; }

    }
}