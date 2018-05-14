using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Config;
using HYS.RdetAdapter.Configuration;
using HYS.RdetAdapter.Common;

namespace HYS.RdetAdapter.RdetOutboundAdapterConfiguration
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
            Application.Run(new FRdetOutConfiguration());
        }

        public static Logging log = new Logging( "RdetOutboundAdapterConfiguration.log");

        public static void PreLoading()
        {
            string FileName = Application.StartupPath + "\\" + RdetOutboundAdapterConfigMgt.FileName;

            //RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig = RdetOutboundAdapterConfigMgt.BuildSampleConfig();
            //RdetOutboundAdapterConfigMgt.Save(FileName);
            //LoadConfigXMLFile();            
            
            if (!RdetOutboundAdapterConfigMgt.Load(FileName))
            {
                log.Write(LogType.Error, "Cannot Load Configuration file: " + FileName);
                if (MessageBox.Show("Cannot load configuration file. Do you want to create an empty configuration file?",
                         "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    RdetOutboundAdapterConfigMgt.Save(FileName);
                }
            }
            
        }
    }
}