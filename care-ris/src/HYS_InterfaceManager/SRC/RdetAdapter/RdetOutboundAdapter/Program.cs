using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Logging;
using HYS.RdetAdapter.Configuration;
using HYS.RdetAdapter.Common;
using HYS.Common.Objects.Device;

namespace HYS.RdetAdapter.RdetOutboundAdapter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bStandalone = true;

            PreLoading();

            //TestCase.BuildTestConfigFile(); //DEBUG

            // for stand alone runtime only
            LoggingHelper.EnableXmlLogging(Log);
            LoggingHelper.EnableApplicationLogging(Log);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            BeforeExit();
        }

        public static bool bStandalone = false;

        public static Logging Log = new Logging(Application.StartupPath + "\\RdetOutboundAdapter.log");
        public static DataBase Database;
        public static DeviceDirManager DeviceMgt;        
        public static string InterfaceName
        {
            get
            {
                if (DeviceMgt == null) return "";
                return DeviceMgt.DeviceDirInfor.Header.Name;
            }
        }

        public static void PreLoading()
        {
            string FileName = Application.StartupPath + "\\" + RdetOutboundAdapterConfigMgt.FileName;
                        
            Log.WriteAppStart("RdetOutboundAdapter");

            //RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig = RdetOutboundAdapterConfigMgt.BuildSampleConfig();
            //RdetOutboundAdapterConfigMgt.Save(FileName);

            if (! RdetOutboundAdapterConfigMgt.Load(FileName))
            {
                Log.Write(LogType.Error, "Load configuration failed. \r\n" + FileName + "\r\n" + RdetOutboundAdapterConfigMgt.LastException.Message);
            }
            Database = new DataBase(RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.GWDataDBConnection);
            DeviceMgt = new DeviceDirManager();
            DeviceMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
            if (DeviceMgt.LoadDeviceDir())
            {
                Log.Write("Load DeviceDir succeeded. " + DeviceMgt.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load DeviceDir failed. " + DeviceMgt.FileName);
            }

        }

        public static void BeforeExit()
        {
            Log.WriteAppExit("RdetOutboundAdapter");
        }
    }
}