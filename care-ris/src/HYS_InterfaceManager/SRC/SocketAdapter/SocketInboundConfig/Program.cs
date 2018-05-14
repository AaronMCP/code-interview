using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Configuration;
using HYS.SocketAdapter.Common;

namespace HYS.SocketAdapter.SocketInboundAdapterConfiguration
{
    static class Program
    {
        public static string GWDataDBConnection;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PreLoading();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FSocketInConfiguration());
        }

        public static Logging log = new Logging( "SocketInboundAdapterConfiguration.log");

        public static void PreLoading()
        {
            //LoadConfigXMLFile();
            string FileName = Application.StartupPath + "\\" + SocketInboundAdapterConfigMgt.FileName;
            if (!SocketInboundAdapterConfigMgt.Load(FileName))
            {
                log.Write(LogType.Error, "Cannot Load Configuration file: " + FileName);
                if (MessageBox.Show("Cannot load configuration file. Do you want to create an empty configuration file?",
                         "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    SocketInboundAdapterConfigMgt.Save(FileName);
                }
            }
            else
            {
                //log.LogLevel = SocketInboundAdapterConfigMgt.SocketInAdapterConfig.InGeneralParams.LogLevel;
            }
        }
    }
}