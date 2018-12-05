using samsung_api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
