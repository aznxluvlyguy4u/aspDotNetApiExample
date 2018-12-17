using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace samsung.api.Models.Requests
{
    public class LoginRequest
    {
        [JsonRequired]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [JsonRequired]
        public string Password { get; set; }
    }
}