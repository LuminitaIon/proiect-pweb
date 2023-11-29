namespace GoSaveMe.Commons.Models.Database
{
    public class Country
    {
        public int Id { get; set; }
        public string ISO { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ISO3 { get; set; }
        public int NumCode { get; set; }
        public int PhoneCode { get; set; }
    }
}
