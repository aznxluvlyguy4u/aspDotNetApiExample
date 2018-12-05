using samsung_api.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.Models.Response
{
    public class ProfileCreateResponse
    {
        public ProfileCreateResponse()
        {
        }

        public ProfileCreateResponse(IProfile profile)
        {
            if (profile == null)
            {
                return;
            }
            Id = profile.Id;
            FirstName = profile.FirstName;
            LastName = profile.LastName;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
