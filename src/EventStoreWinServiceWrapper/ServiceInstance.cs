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

        [ConfigurationProperty("tcpPort", IsRequired = true)]
        public int TcpPort
        {
            get { return (int)this["tcpPort"]; }
            set { this["tcpPort"] = value; }
        }

        [ConfigurationProperty("httpPort", IsRequired = true)]
        public int HttpPort
        {
            get { return (int)this["httpPort"]; }
            set { this["httpPort"] = value; }
        }

        [ConfigurationProperty("dbPath", IsRequired = true)]
        public string DbPath
        {
            get { return (string)this["dbPath"]; }
            set { this["dbPath"] = value; }
        }

        [ConfigurationProperty("address", IsRequired = false)]
        public string Address
        {
            get { return (string)this["address"]; }
            set { this["address"] = value; }
        }

        [ConfigurationProperty("prefixes", IsRequired = false)]
        public string Prefixes
        {
            get { return (string)this["prefixes"]; }
            set { this["prefixes"] = value; }
        }
    }
}