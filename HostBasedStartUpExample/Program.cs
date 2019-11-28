using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HostBasedStartUpExample.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;

namespace HostBasedStartUpExample
{

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // Run with console or service
            var asService = !(Debugger.IsAttached || args.Contains("--console"));


            var builder = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    if (asService)
                    {
                        var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                        var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                        Directory.SetCurrentDirectory(pathToContentRoot);
                        configHost.AddJsonFile("hostsettings.json");
                    }
                    else
                    {
                        configHost.SetBasePath(Directory.GetCurrentDirectory());
                        configHost.AddJsonFile("hostsettings.json", true);
                    }
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile("appsettings.json", true);
                    configApp.AddJsonFile($"appsettings.{hostContext.Configuration["ASPNETCORE_ENVIRONMENT"]}.json",
                        true);

                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IServiceDefinition, ServiceDefinition>();
                    services.AddHostedService<YourHostedService>();
                })
                ;

            if (asService)
                await builder.RunAsServiceAsync();
            else
                await builder.RunConsoleAsync();
        }
    }
}