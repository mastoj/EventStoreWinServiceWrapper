using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace EventStoreWinServiceWrapper
{
    public class ServiceWrapper
    {
        private IPAddress _address;
        private readonly EventStoreServiceConfiguration _configuration;
        private ServiceInstanceCollection _instances;
        private List<Process> _processes;

        public ServiceWrapper(IPAddress address, EventStoreServiceConfiguration configuration)
        {
            _address = address;
            _configuration = configuration;
            _instances = _configuration.Instances;
            _processes = new List<Process>();
        }

        public void Start()
        {
            string file = Path.Combine(_configuration.Executable);
            var processMapper = new ProcessMapper();

            foreach (ServiceInstance instance in _instances)
            {
                var address = string.IsNullOrEmpty(instance.Address) ? _address : IPAddress.Parse(instance.Address);
                var info = processMapper.GetProcessStartInfo(file, address, instance);
                var process = Process.Start(info);
                process.Exited += (sender, args) => Stop();
                _processes.Add(process);
            }
        }

        public void Stop()
        {
            _processes.ForEach(p =>
            {
                p.Refresh();

                if (p.HasExited) return;

                p.Kill();
                p.WaitForExit();
                p.Dispose();
            });
        }
    }
}