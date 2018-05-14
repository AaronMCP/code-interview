using System;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using SQLOutboundAdapter.Objects;
using SQLOutboundAdapter.Forms;
using SQLOutboundAdapter.Adapters;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Objects.Logging;

namespace SQLOutboundAdapter
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

            // for stand alone runtime only
            LoggingHelper.EnableXmlLogging(Log);
            LoggingHelper.EnableDatabaseLogging(db,Log);
            LoggingHelper.EnableApplicationLogging(Log);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            BeforeExit();
        }

        public static Logging Log = new Logging();
        public static AdapterDataBase db;// = new AdapterDataBase("");//TODO:ADD CONNECTION STRING
        
        public static void Preloading()
        {
            string FileName = Application.StartupPath + "\\" + SQLOutAdapterConfigMgt._FileName;

            //TestCase.BuildTestConfigFile();   //DEBUG


            Log.WriteAppStart(Application.StartupPath + "\\SQLOutboundAdapter.log");

            db = new AdapterDataBase("");
            if (!SQLOutAdapterConfigMgt.Load(FileName))
            {
                Log.Write(LogType.Error, "Load configuration failed. \r\n" + FileName + "\r\n");//+ SQLOutAdapterConfigMgt.LastError);
            }
            else
            {
                //db.ConnectionString = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr;
                if (SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.FileConnection)
                    db.ConnectionString = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.FileConnectionString;
                else
                    db.ConnectionString = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr;
            }
        }

        public static void BeforeExit()
        {
            Log.WriteAppExit("SQLOutboundAdapter");
        }
    }
}