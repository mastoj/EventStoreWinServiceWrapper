using System.Configuration;

namespace EventStoreWinServiceWrapper
{
    public class ServiceInstance : ConfigurationElement
    {
        [ConfigurationProperty("internalip", IsRequired = false)]
        public string InternalIP
        {
            get { return (string)this["internalip"]; }
            set { this["internalip"] = value; }
        }

        [ConfigurationProperty("externalip", IsRequired = false)]
        public string ExternalIP
        {
            get { return (string)this["externalip"]; }
            set { this["externalip"] = value; }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("dbPath", IsRequired = true)]
        public string DbPath
        {
            get { return (string)this["dbPath"]; }
            set { this["dbPath"] = value; }
        }

        [ConfigurationProperty("logPath", IsRequired = false, DefaultValue = @"..\Logs")]
        public string LogPath
        {
            get { return (string)this["logPath"]; }
            set { this["logPath"] = value; }
        }

        [ConfigurationProperty("internaladdresses", IsRequired = false)]
        public string InternalAddresses
        {
            get { return (string)this["internaladdresses"]; }
            set { this["internaladdresses"] = value; }
        }

        [ConfigurationProperty("externaladdresses", IsRequired = false)]
        public string ExternalAddresses
        {
            get { return (string)this["externaladdresses"]; }
            set { this["externaladdresses"] = value; }
        }

        [ConfigurationProperty("runProjections", IsRequired = false)]
        public string RunProjections
        {
            get { return (string)this["runProjections"]; }
            set { this["runProjections"] = value; }
        }
    }
}