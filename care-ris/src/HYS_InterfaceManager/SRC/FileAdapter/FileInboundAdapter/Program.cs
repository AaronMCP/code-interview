using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Logging;
using HYS.FileAdapter.Configuration;
using HYS.FileAdapter.Common;

namespace HYS.FileAdapter.FileInboundAdapter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
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

        public static Logging Log = new Logging(Application.StartupPath + "\\FileInboundAdapter.log");        

        public static void Preloading()
        {
                                    
            Log.WriteAppStart("FileInboundAdapter");

            string FileName = Application.StartupPath + "\\" + FileInboundAdapterConfigMgt.FileName;
            //FileInboundAdapterConfigMgt.FileInAdapterConfig = FileInboundAdapterConfigMgt.BuildSampleConfig();
            //FileInboundAdapterConfigMgt.Save(FileName);

            if (! FileInboundAdapterConfigMgt.Load(FileName))
            {
                Log.Write(LogType.Error, "Load configuration failed. \r\n" + FileName + "\r\n" + FileInboundAdapterConfigMgt.LastException.Message);
            }
            
        }

        public static void BeforeExit()
        {
            Log.WriteAppExit("FileInboundAdapter");
        }
    }
}