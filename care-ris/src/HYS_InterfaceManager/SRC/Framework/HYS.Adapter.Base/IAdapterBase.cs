using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Adapter.Base
{
    public interface IAdapterBase
    {
        /// <summary>
        /// Initialize adapter.
        /// This method is called by Adapter Service when loading the adapter.
        /// </summary>
        /// <param name="arguments">Arguments for initialization.</param>
        /// <returns>Return true when initialization succeed.</returns>
        bool Initialize(string[] arguments);

        /// <summary>
        /// Exit adapter.
        /// The method is called before Adapter Service unload the adapter.
        /// </summary>
        /// <param name="arguments">Arguments for exiting.</param>
        /// <returns>Return true when exiting succeed.</returns>
        bool Exit(string[] arguments);

        /// <summary>
        /// Get last exception information of this adapter.
        /// </summary>
        Exception LastError { get; }
    }
}
