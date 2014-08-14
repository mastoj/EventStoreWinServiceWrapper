using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Topshelf;

namespace EventStoreWinServiceWrapper
{
    class Program
    {
        public static void Main()
        {
            var configuration = (EventStoreServiceConfiguration)ConfigurationManager.GetSection("eventStore");
            var address = GetIPAddress();

            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.EnableShutdown();
                x.EnableServiceRecovery(c => c.RestartService(1));

                x.Service<ServiceWrapper>(s =>
                {
                    s.ConstructUsing(name => new ServiceWrapper(address, configuration));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.SetDescription("EventStoreServiceWrapper");
                x.SetDisplayName("EventStoreServiceWrapper");
                x.SetServiceName("EventStoreServiceWrapper");
            });

            Console.ReadLine();
        }

        private static IPAddress GetIPAddress()
        {
            string hostName = Dns.GetHostName();
            return Dns.GetHostAddresses(hostName).First(address =>
            {
                if (address.AddressFamily != AddressFamily.InterNetwork)
                    return false;

                return !Equals(address, IPAddress.Loopback);
            });
        }
    }
}
