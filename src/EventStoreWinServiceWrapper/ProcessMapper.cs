using System;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace EventStoreWinServiceWrapper
{
    public class ProcessMapper
    {
        public ProcessStartInfo GetProcessStartInfo(string file, IPAddress address, ServiceInstance instance)
        {
            var arguments = GetProcessArguments(address, instance);

            return new ProcessStartInfo(file, arguments)
            {
                UseShellExecute = false
            };
        }

        private string GetProcessArguments(IPAddress address, ServiceInstance instance)
        {
            if (address == null) throw new ArgumentNullException("address");
            var sb = new StringBuilder();
            sb.AppendFormat("--ip {0} ", address);
            sb.AppendFormat("--tcp-port {0} ", instance.TcpPort);
            sb.AppendFormat("--http-port {0} ", instance.HttpPort);
            sb.AppendFormat("--db {0}", instance.DbPath);
            return sb.ToString();
        }
    }
}