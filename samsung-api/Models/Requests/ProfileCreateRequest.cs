using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samsung.api.Models.Requests
{
    public class ProfileCreateRequest
    {
        [JsonRequired]
        public string Email { get; set; }

        [JsonRequired]
        public string Password { get; set; }
    }
}
