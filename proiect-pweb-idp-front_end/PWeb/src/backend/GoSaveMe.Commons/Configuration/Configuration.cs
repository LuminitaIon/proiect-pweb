using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSaveMe.Commons.Configuration
{
    public class Configuration
    {
        public readonly GoSaveMeConfigurationData? GoSaveMeConfiguration;

        public static DateTime LastUpdate = DateTime.UtcNow;

        private static Configuration? instance = null;
        private static readonly object s_lock = new();

        private Configuration() => GoSaveMeConfiguration = GoSaveMeConfigurationData.FromFile();

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
}
