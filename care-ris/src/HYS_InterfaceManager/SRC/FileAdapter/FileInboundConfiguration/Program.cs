using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Logging;
using HYS.FileAdapter.Configuration;
using HYS.FileAdapter.Common;

namespace HYS.FileAdapter.FileInboundAdapterConfiguration
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
            bRunSingle = true;

            PreLoading();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FFileInConfig());
        }

        public static bool bRunSingle = false;
        public static Logging log = new Logging( "FileInboundAdapterConfiguration.log");

        public static void PreLoading()
        {
            //LoadConfigXMLFile();
            string FileName = Application.StartupPath + "\\" + FileInboundAdapterConfigMgt.FileName;
            if (!FileInboundAdapterConfigMgt.Load(FileName))
            {
                log.Write(LogType.Error, "Cannot Load Configuration file: " + FileName);
                if (MessageBox.Show("Cannot load configuration file. Do you want to create an empty configuration file?",
                         "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    FileInboundAdapterConfigMgt.Save(FileName);
                }
            }
        }
    }
}