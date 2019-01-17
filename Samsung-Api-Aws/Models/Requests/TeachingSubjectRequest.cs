using Newtonsoft.Json;

namespace samsung.api.Models.Requests
{
    public class TeachingSubjectRequest
    {
        [JsonRequired]
        public int Id { get; set; }
    }
}