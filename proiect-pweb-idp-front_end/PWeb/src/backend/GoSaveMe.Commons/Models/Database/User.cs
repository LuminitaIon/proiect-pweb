namespace GoSaveMe.Commons.Models.Database
{
    public class User
    {
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Citizenship { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsPrivileged { get; set; }
        public bool IsOrg { get; set; }
        public string? ImageURI { get; set; }
        public double Rating { get; set; }
    }
}
