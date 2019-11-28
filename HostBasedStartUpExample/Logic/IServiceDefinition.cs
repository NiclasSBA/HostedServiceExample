using System;
using System.Collections.Generic;
using System.Text;

namespace HostBasedStartUpExample.Logic
{
    public interface IServiceDefinition : IDisposable
    {
        /// <summary>
        ///     Start the service and any underlying systems
        /// </summary>
        /// <returns>true if statup was successful</returns>
        bool Start();

        /// <summary>
        ///     Stop the service and any underlying systems
        /// </summary>
        /// <returns>True if stop was successful</returns>
        bool Stop();
    }
}
