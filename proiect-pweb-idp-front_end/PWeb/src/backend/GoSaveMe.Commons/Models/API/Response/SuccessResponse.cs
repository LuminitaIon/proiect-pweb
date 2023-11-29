using Newtonsoft.Json;

namespace GoSaveMe.Commons.Models.API
{
    public class SuccessResponse<T> : IResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; } = "success";
        [JsonProperty("data")]
        public T? Data { get; set; }

        public SuccessResponse() { }

        public SuccessResponse(T? data)
        {
            Data = data;
        }
    }
}
