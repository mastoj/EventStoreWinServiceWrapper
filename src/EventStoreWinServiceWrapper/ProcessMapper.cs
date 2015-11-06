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

            if (!string.IsNullOrWhiteSpace(instance.InternalHeartBeatTimeout))
            {
                var internalHeartBeatTimeout = instance.InternalHeartBeatTimeout;
                configParameters.Add("int-tcp-heartbeat-timeout", internalHeartBeatTimeout);
            }
            if (!string.IsNullOrWhiteSpace(instance.InternalHeartBeatInterval))
            {
                var internalHeartBeatInterval = instance.InternalHeartBeatInterval;
                configParameters.Add("int-tcp-heartbeat-interval", internalHeartBeatInterval);
            }

            if (!string.IsNullOrWhiteSpace(instance.ExternalHeartBeatTimeout))
            {
                var externalHeartBeatTimeout = instance.ExternalHeartBeatTimeout;
                configParameters.Add("ext-tcp-heartbeat-timeout", externalHeartBeatTimeout);
            }
            if (!string.IsNullOrWhiteSpace(instance.ExternalHeartBeatInterval))
            {
                var externalHeartBeatInterval = instance.ExternalHeartBeatInterval;
                configParameters.Add("ext-tcp-heartbeat-interval", externalHeartBeatInterval);
            }

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