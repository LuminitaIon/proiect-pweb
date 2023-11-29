namespace GoSaveMe.Commons.Models.Database
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string? ImageURI { get; set; }
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Approved { get; set; }
        public string? Reaction { get; set; }
    }
}
