using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.Models.Requests
{
    public class LoginRequest
    {
        [JsonRequired]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [JsonRequired]
        public string password { get; set; }
    }
}