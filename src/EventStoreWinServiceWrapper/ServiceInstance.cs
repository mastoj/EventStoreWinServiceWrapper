using System.Configuration;

namespace EventStoreWinServiceWrapper
{
    public class ServiceInstance : ConfigurationElement
    {
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

        [ConfigurationProperty("addresses", IsRequired = false)]
        public string Addresses
        {
            get { return (string)this["addresses"]; }
            set { this["addresses"] = value; }
        }
    }
}