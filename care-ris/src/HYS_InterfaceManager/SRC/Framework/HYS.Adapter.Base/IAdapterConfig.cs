using System;
using System.Collections.Generic;
using HYS.Common.Objects.Rule;

namespace HYS.Adapter.Base
{
    public interface IAdapterConfig : IAdapterBase
    {
        /// <summary>
        /// Get options of adapter service
        /// </summary>
        AdapterOption Option { get; }

        /// <summary>
        /// Get configuration GUI of this adapter.
        /// This GUI will be embeded into Adapter Configuration GUI.
        /// 
        /// Note: Adapter can provide more than one TabPage GUI to AdapterConfig.exe
        /// </summary>
        /// <returns>Return instance of configuration GUI of this adapter.</returns>
        IConfigUI[] GetConfigUI();

        /// <summary>
        /// Get look up tables this adapter wants to create.
        /// 
        /// Note: these table will be use only by this interface. If you want to create a look up table that is shared across all interface, please create this table on IM GUI.
        /// </summary>
        /// <returns>Return array of look up tables or NULL for not to create loop up tables.</returns>
        LookupTable[] GetLookupTables();

        /// <summary>
        /// Get configuration description to be display on IM's interface view.
        /// </summary>
        /// <returns>Return configuration description.</returns>
        string GetConfigurationSummary();
    }
}
