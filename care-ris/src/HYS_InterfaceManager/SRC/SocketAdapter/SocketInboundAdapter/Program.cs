using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Configuration;
using HYS.SocketAdapter.Common;

namespace HYS.SocketAdapter.SocketInboundAdapter
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

        public static Logging Log = new Logging(Application.StartupPath + "\\SocketInboundAdapter.log");        

        public static void Preloading()
        {
                                    
            Log.WriteAppStart("SocketInboundAdapter");

            string FileName = Application.StartupPath + "\\" + SocketInboundAdapterConfigMgt.FileName;
            //SocketInboundAdapterConfigMgt.SocketInAdapterConfig = SocketInboundAdapterConfigMgt.BuildSampleConfig();
            //SocketInboundAdapterConfigMgt.Save(FileName);

            if (! SocketInboundAdapterConfigMgt.Load(FileName))
            {
                Log.Write(LogType.Error, "Load configuration failed. \r\n" + FileName + "\r\n" + SocketInboundAdapterConfigMgt.LastException.Message);
            }
            
        }

        public static void BeforeExit()
        {
            Log.WriteAppExit("SocketInboundAdapter");
        }
    }
}