using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Adapter.Base
{
    public interface IAdapterService : IAdapterBase
    {
        /// <summary>
        /// Start adapter.
        /// This method is called by Adapter Service when starting the adapter NT service.
        /// </summary>
        /// <param name="arguments">Arguments for starting.</param>
        /// <returns>Return true when starting succeed.</returns>
        bool Start(string[] arguments);

        /// <summary>
        /// Stop adapter.
        /// This method is called by Adapter Service when stopping the adapter NT service.
        /// </summary>
        /// <param name="arguments">Arguments for stopping.</param>
        /// <returns>Return true when stopping succeed.</returns>
        bool Stop(string[] arguments);

        /// <summary>
        /// Get runing status of adapter
        /// </summary>
        AdapterStatus Status { get; }
    }
}
