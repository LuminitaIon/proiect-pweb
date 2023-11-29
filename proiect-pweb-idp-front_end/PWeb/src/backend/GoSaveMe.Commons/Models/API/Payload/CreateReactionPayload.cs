namespace GoSaveMe.Commons.Models.API.Payload
{
    public class CreateReactionPayload
    {
        public int? NewsId { get; set; }
        public string? Username { get; set; }
        public string? Reaction { get; set; }
    }
}
