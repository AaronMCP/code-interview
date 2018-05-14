using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.FileAdapter.Configuration;
using HYS.FileAdapter.Common;

namespace HYS.FileAdapter.FileOutboundAdapter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bRunSingle = true;

            Preloading();

            //TestCase.BuildTestConfigFile(); //DEBUG

            // for stand alone runtime only
            LoggingHelper.EnableXmlLogging(Log);
            LoggingHelper.EnableApplicationLogging(Log);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            BeforeExit();
        }
        public static bool bRunSingle = false;

        public static  Logging Log = new Logging(Application.StartupPath + "\\FileOutboundAdapter.log");        


        public static void Preloading()
        {
                                    
            Log.WriteAppStart("FileOutboundAdapter");

            string FileName = Application.StartupPath + "\\" + FileOutboundAdapterConfigMgt.FileName;
            //FileOutboundAdapterConfigMgt.FileOutAdapterConfig = FileOutboundAdapterConfigMgt.BuildSampleConfig();
            //FileOutboundAdapterConfigMgt.Save(FileName);

            if (! FileOutboundAdapterConfigMgt.Load(FileName))
            {
                Log.Write(LogType.Error, "Load configuration failed. \r\n" + FileName + "\r\n" + FileOutboundAdapterConfigMgt.LastException.Message);
            }
            
        }

        public static void BeforeExit()
        {
            Log.WriteAppExit("FileOutboundAdapter");
        }
    }
}