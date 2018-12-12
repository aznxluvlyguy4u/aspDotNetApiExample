using samsung_api.Models.Interfaces;

namespace samsung.api.Models.Response
{
    public class GeneralUserCreateResponse
    {
        public GeneralUserCreateResponse()
        {
        }

        public GeneralUserCreateResponse(IGeneralUser generalUser)
        {
            if (generalUser == null)
            {
                return;
            }
            Id = generalUser.Id;
            FirstName = generalUser.FirstName;
            LastName = generalUser.LastName;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}