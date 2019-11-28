using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;

namespace HostBasedStartUpExample.Logic
{
    public class ServiceDefinition : IServiceDefinition
    {

        private readonly IHostEnvironment _env;
        public ServiceDefinition(IHostEnvironment env,HostBuilderContext ctx)
        {
            _env = env;
            Console.WriteLine($"Environment that is set manually through hostsetting.json is {ctx.Configuration["ASPNETCORE_ENVIRONMENT"]}");
        }

        public bool Start()
        {
            Console.WriteLine($"Host Environment is {_env.EnvironmentName}");
            return true;
        }

        public bool Stop()
        {
        
            return true;
        }

        #region IDisposable implementation

        [ExcludeFromCodeCoverage]
        ~ServiceDefinition()
        {
            // the finalizer also has to release unmanaged resources,
            // in case the developer forgot to dispose the object.
            Dispose(false);
        }

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);

            // this tells the garbage collector not to execute the finalizer
            GC.SuppressFinalize(this);
        }

        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // clean up managed resources:
                // dispose child objects that implement IDisposable
                //_messageSubscriber.SafeDispose();
            }

            // clean up unmanaged resources
        }

        #endregion
    }
}