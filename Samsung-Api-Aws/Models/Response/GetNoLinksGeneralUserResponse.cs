using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using samsung_api.Models.Interfaces;

namespace samsung.api.Models.Response
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class GetNoLinksGeneralUserResponse
    {
        public GetNoLinksGeneralUserResponse()
        {
        }

        public GetNoLinksGeneralUserResponse(IGeneralUser generalUser)
        {
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ImageResponse ProfileImage { get; set; }  // base64 encoded string
    }
}