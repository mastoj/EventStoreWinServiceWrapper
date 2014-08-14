using System;
using System.Configuration;

namespace EventStoreWinServiceWrapper
{
    public class ServiceInstanceCollection : ConfigurationElementCollection
    {
        protected override string ElementName
        {
            get { return "instance"; }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }


        public ServiceInstance this[int index]
        {
            get { return BaseGet(index) as ServiceInstance; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceInstance();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceInstance)element).Name;
        }

        protected override bool IsElementName(string elementName)
        {
            return !String.IsNullOrEmpty(elementName) && elementName == ElementName;
        }
    }
}