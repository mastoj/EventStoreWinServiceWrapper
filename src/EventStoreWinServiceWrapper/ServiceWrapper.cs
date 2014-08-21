using System.Collections.Generic;
using System.Diagnostics;

namespace EventStoreWinServiceWrapper
{
    public class ServiceWrapper
    {
        private readonly EventStoreServiceConfiguration _configuration;
        private readonly List<Process> _processes;

        public ServiceWrapper(EventStoreServiceConfiguration configuration)
        {
            _configuration = configuration;
            _processes = new List<Process>();
        }

        public void Start()
        {
            var processMapper = new ProcessMapper();

            foreach (ServiceInstance instance in _configuration.Instances)
            {
                var info = processMapper.GetProcessStartInfo(_configuration.Executable, instance);
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