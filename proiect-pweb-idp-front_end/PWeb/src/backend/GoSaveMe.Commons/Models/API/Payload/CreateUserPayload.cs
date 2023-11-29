using Newtonsoft.Json;

namespace GoSaveMe.Commons.Models.API.Payload
{
    public class GetUserPayload
    {
        [JsonProperty("username")]
        public string? Username { get; set; }
    }
}
