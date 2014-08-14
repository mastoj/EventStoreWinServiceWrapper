using System.Configuration;

namespace EventStoreWinServiceWrapper
{
    public class EventStoreServiceConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true, IsKey = false, IsRequired = true)]
        public ServiceInstanceCollection Instances
        {
            get { return (ServiceInstanceCollection)this[""]; }
            set { this[""] = value; }
        }

        [ConfigurationProperty("executable", IsRequired = true)]
        public string Executable
        {
            get { return (string)this["executable"]; }
            set { this["executable"] = value; }
        }
    }
}