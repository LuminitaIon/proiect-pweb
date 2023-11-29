namespace GoSaveMe.Commons.Models.Database
{
    public class NewsReaction 
    {
        public int NewsId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Reaction { get; set; } = string.Empty;
    }
}
