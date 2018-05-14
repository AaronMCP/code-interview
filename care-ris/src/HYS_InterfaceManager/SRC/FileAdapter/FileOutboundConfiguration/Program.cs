using System;
using System.Collections.Generic;
using System.Windows.Forms;

using HYS.Adapter.Base;
using HYS.Common.Objects;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Translation;
using HYS.FileAdapter.Common;
using HYS.FileAdapter.Configuration;



namespace HYS.FileAdapter.FileOutboundAdapterConfiguration
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
            Application.Run(new FFileOutConfig());
        }

        public static bool bRunSingle = false;

        public static Logging log = new Logging( "FileOutboundAdapterConfiguration.log");

        public static void PreLoading()
        {
            //LoadConfigXMLFile();
            string FileName = Application.StartupPath + "\\" + FileOutboundAdapterConfigMgt.FileName;
            if (!FileOutboundAdapterConfigMgt.Load(FileName))
            {
                log.Write(LogType.Error, "Cannot Load Configuration file: " + FileName);
                if (MessageBox.Show("Cannot load configuration file. Do you want to create an empty configuration file?",
                         "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    FileOutboundAdapterConfigMgt.Save(FileName);
                }
            }
            else
            {
                //log.LogLevel = FileOutboundAdapterConfigMgt.FileOutAdapterConfig.OutGeneralParams.LogLevel;
            }

            //ConfigMain cm = new ConfigMain();
            //IOutboundRule[] rs = cm.GetRules();
        }
    }
}