using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace HYS.Adapter.Base
{
    public interface IConfigUI
    {
        /// <summary>
        /// Return System.Windows.Forms.Control instance of configuration GUI.
        /// This instance will be embeded into Adapter Configuration window.
        /// </summary>
        /// <returns>Return System.Windows.Forms.Control instance of configuration GUI.</returns>
        Control GetControl();

        /// <summary>
        /// Load config.
        /// This method will be invoked when Adapter Configuration window is loaded.
        /// </summary>
        /// <returns>Return true when loading configuration succeed.</returns>
        bool LoadConfig();

        /// <summary>
        /// Save config.
        /// This method will be invoked when Adapter Configuration window is closed when user wants to save the configurtaion.
        /// </summary>
        /// <returns>Return true when saving configuration succeed</returns>
        bool SaveConfig();

        /// <summary>
        /// TabPage title on AdapterConfig.exe
        /// </summary>
        string Name { get; }
    }
}
