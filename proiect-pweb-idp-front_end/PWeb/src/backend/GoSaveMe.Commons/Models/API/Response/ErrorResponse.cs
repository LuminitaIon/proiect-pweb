using Newtonsoft.Json;

namespace GoSaveMe.Commons.Models.API
{
    public class ErrorResponse : IResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; } = "error";
        [JsonProperty("message")]
        public string? Message { get; set; }

        public ErrorResponse() { }
        public ErrorResponse(string message)
        {
            Message = message;
        }
    }
}
