namespace GoSaveMe.Commons.Models.API.Payload
{
    public class NewsCreatePayload
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? ImageURI { get; set; }
        public string? Username { get; set; }
        public bool Approved { get; set; }
    }
}
