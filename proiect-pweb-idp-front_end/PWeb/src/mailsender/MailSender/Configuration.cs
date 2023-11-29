using Newtonsoft.Json;

namespace MailSender
{
    public class Configuration
    {
        public readonly ConfigurationData? MailsenderConfiguration;

        public static DateTime LastUpdate = DateTime.UtcNow;

        private static Configuration? instance = null;
        private static readonly object s_lock = new();

        private Configuration() => MailsenderConfiguration = ConfigurationData.FromFile();

        public static Configuration Instance
        {
            get
            {
                Monitor.Enter(s_lock);

                if ((DateTime.UtcNow - LastUpdate).TotalHours > 1.0)
                {
                    Interlocked.Exchange(ref instance, null);
                    LastUpdate = DateTime.UtcNow;
                }

                if (instance != null)
                {
                    Monitor.Exit(s_lock);
                    return instance;
                }

                Configuration config = new();
                Interlocked.Exchange(ref instance, config);
                Monitor.Exit(s_lock);

                return instance;
            }
        }
    }
    public class ConfigurationData
    {
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

        [JsonProperty("SmtpUsername")]
        public string SmtpUsername { get; set; } = "";

        [JsonProperty("SmtpPassword")]
        public string SmtpPassword { get; set; } = "";

        [JsonProperty("SmtpClient")]
        public string SmtpClient { get; set; } = "";

        [JsonProperty("SmtpPort")]
        public int SmtpPort { get; set; }

        public static ConfigurationData? FromFile()
        {
            using StreamReader streamReader = new(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

            return JsonConvert.DeserializeObject<ConfigurationData>(streamReader.ReadToEnd());
        }
    }
}
