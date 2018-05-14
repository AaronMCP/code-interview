using System;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.Common.DataAccess;
using SQLInboundAdapter.Objects;
using SQLInboundAdapter.Forms;
using HYS.SQLInboundAdapterObjects;

namespace SQLInboundAdapter
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
            LoggingHelper.EnableDatabaseLogging(db,Log);
            LoggingHelper.EnableApplicationLogging(Log);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            BeforeExit();
        }

        public static Logging Log = new Logging(Application.StartupPath + "\\SQLInboundAdapter.log");
        
        public static AdapterDataBase db = new AdapterDataBase("");//TODO:Add Connection string Here

        public static void Preloading()
        {
            string FileName = Application.StartupPath + "\\" +  SQLInAdapterConfigMgt._FileName;
               
            
            Log.WriteAppStart("SQLInboundAdapter");

            if (!SQLInAdapterConfigMgt.Load(FileName))
            {
                Log.Write(LogType.Error, "Load configuration failed. \r\n" + FileName + "\r\n" + SQLInAdapterConfigMgt.LastException.Message);
            }
            else
            {
                if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.FileConnection)
                    db.ConnectionString = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.FileConnectionString;
                else
                    db.ConnectionString = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr;
            }
        }

        public static void BeforeExit()
        {
            Log.WriteAppExit("SQLInboundAdapter");
        }
    }
}