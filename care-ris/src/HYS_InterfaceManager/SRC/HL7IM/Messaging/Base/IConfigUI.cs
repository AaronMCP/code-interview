using System;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HYS.IM.Messaging.Base
{
    public interface IConfigUI
    {
        /// <summary>
        /// Return System.Windows.Forms.Control instance of configuration GUI.
        /// This instance will be embeded into Framework Configuration window.
        /// </summary>
        /// <returns>Return System.Windows.Forms.Control instance of configuration GUI.</returns>
        Control GetControl();

        /// <summary>
        /// Load config.
        /// This method will be invoked when Framework Configuration window is loaded.
        /// </summary>
        /// <returns>Return true when loading configuration succeed.</returns>
        bool LoadConfig();

        /// <summary>
        /// Validate UI input and save UI input into configuration object.
        /// This method will be invoked before Framework Configuration window is closing when user wants to save the configurtaion.
        /// </summary>
        /// <returns>Return true when validating and saving UI input into configuration object succeed</returns>
        bool ValidateConfig();

        /// <summary>
        /// TabPage title on Messaging.Config.exe
        /// </summary>
        string Title { get; }
    }
}
