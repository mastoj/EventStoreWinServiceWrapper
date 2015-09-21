using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace EventStoreWinServiceWrapper
{
    public class ProcessMapper
    {
        public ProcessStartInfo GetProcessStartInfo(string executable, ServiceInstance instance)
        {
            var arguments = GetProcessArguments(instance);

            return new ProcessStartInfo(executable, arguments)
            {
                UseShellExecute = false
            };
        }

        private string GetProcessArguments(ServiceInstance instance)
        {
            var configParameters = new Dictionary<string, string>();
            configParameters.Add("log", instance.LogPath);
            configParameters.Add("db", instance.DbPath);
            configParameters.Add("run-projections", instance.RunProjections);

            if (!string.IsNullOrWhiteSpace(instance.InternalAddresses))
            {
                configParameters.Add("int-http-prefixes", instance.InternalAddresses);
            }

            if (!string.IsNullOrWhiteSpace(instance.ExternalAddresses))
            {
                configParameters.Add("ext-http-prefixes", instance.ExternalAddresses);
            }

            var externalIp = GetIp(instance.ExternalIP);
            configParameters.Add("ext-ip", externalIp);
            var internalIp = GetIp(instance.InternalIP);
            configParameters.Add("int-ip", internalIp);

            return configParameters.Aggregate("",
                (acc, next) => string.Format("{0} --{1} \"{2}\"", acc, next.Key, next.Value));
        }

        private string GetIp(string externalIp)
        {
            if (!string.IsNullOrWhiteSpace(externalIp))
            {
                return externalIp;
            }
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.First(y => y.AddressFamily == AddressFamily.InterNetwork).ToString();
        }
    }
}