using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HostBasedStartUpExample.Logic;
using Microsoft.Extensions.Hosting;

namespace HostBasedStartUpExample
{
    public class YourHostedService : IHostedService
    {
        private readonly IServiceDefinition _serviceDefinition;
        public YourHostedService(IServiceDefinition serviceDefinition)
        {
            _serviceDefinition = serviceDefinition;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"[{nameof(YourHostedService)}] has been started.....");

            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

            await Task.Run(() =>
            {
                _serviceDefinition.Start();

                Debug.WriteLine("Service started");
            }, cancellationToken);

            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {

                Debug.WriteLine("Service stopped");
            }, cancellationToken);
        }
    }
}
