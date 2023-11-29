using Newtonsoft.Json;

namespace GoSaveMe.Commons.Configuration
{
    public class GoSaveMeConfigurationData
    {
        [JsonProperty("DbConnectionString")]
        public string? DbConnectionString { get; set; }

        [JsonProperty("RabbitMqIP")]
        public string RabbitMqIP { get; set; } = "127.0.0.1";

        [JsonProperty("RabbitMqQueue")]
        public string RabbitMqQueue { get; set; } = "gosavemequeue";

        [JsonProperty("RabbitMqPort")]
        public int RabbitMqPort { get; set; } = 5672;

        [JsonProperty("RabbitMqUser")]
        public string RabbitMqUser { get; set; } = "guest";

        [JsonProperty("RabbitMqPassword")]
        public string RabbitMqPassword { get; set; } = "guest";

        [JsonProperty("RabbitMqVHost")]
        public string RabbitMqVHost { get; set; } = "/";

        public static GoSaveMeConfigurationData? FromFile()
        {
            using StreamReader streamReader = new(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

            return JsonConvert.DeserializeObject<GoSaveMeConfigurationData>(streamReader.ReadToEnd());
        }
    }
}
