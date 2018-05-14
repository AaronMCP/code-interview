using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Configuration;
using HYS.SocketAdapter.Common;

namespace HYS.SocketAdapter.SocketOutboundAdapter
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

        public static  Logging Log = new Logging(Application.StartupPath + "\\SocketOutboundAdapter.log");        

        public static void Preloading()
        {
                                    
            Log.WriteAppStart("SocketOutboundAdapter");

            string FileName = Application.StartupPath + "\\" + SocketOutboundAdapterConfigMgt.FileName;
            //SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig = SocketOutboundAdapterConfigMgt.BuildSampleConfig();
            //SocketOutboundAdapterConfigMgt.Save(FileName);

            if (! SocketOutboundAdapterConfigMgt.Load(FileName))
            {
                Log.Write(LogType.Error, "Load configuration failed. \r\n" + FileName + "\r\n" + SocketOutboundAdapterConfigMgt.LastException.Message);
            }
            
        }

        public static void BeforeExit()
        {
            Log.WriteAppExit("SocketOutboundAdapter");
        }
    }
}