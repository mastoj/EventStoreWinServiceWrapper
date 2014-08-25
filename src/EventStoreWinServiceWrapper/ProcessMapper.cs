using System;
using System.Diagnostics;
using System.Net;
using System.Text;

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
            var sb = new StringBuilder();
            sb.AppendFormat("--log {0} ", instance.LogPath);
            sb.AppendFormat("--db {0} ", instance.DbPath);

            if (!string.IsNullOrWhiteSpace(instance.Addresses))
            {
                sb.AppendFormat("--httpprefixes \"{0}\"", instance.Addresses);
            }
            if (!string.IsNullOrWhiteSpace(instance.ExternalIP))
            {
                sb.AppendFormat("--ext-ip \"{0}\"", instance.ExternalIP);                
            }
            if (!string.IsNullOrWhiteSpace(instance.InternalIP))
            {
                sb.AppendFormat("--int-ip \"{0}\"", instance.InternalIP);
            }

            return sb.ToString();
        }
    }
}