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

            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.EnableShutdown();
                x.EnableServiceRecovery(c => c.RestartService(1));

                x.Service<ServiceWrapper>(s =>
                {
                    s.ConstructUsing(name => new ServiceWrapper(configuration));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.SetDescription("EventStoreServiceWrapper");
                x.SetDisplayName("EventStoreServiceWrapper");
                x.SetServiceName("EventStoreServiceWrapper");
            });
        }

    }
}
