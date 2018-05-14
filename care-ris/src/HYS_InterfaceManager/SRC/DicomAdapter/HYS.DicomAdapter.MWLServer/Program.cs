using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.DicomAdapter.MWLServer.Forms;
using HYS.DicomAdapter.MWLServer.Objects;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Device;
using HYS.Common.DataAccess;
using HYS.Adapter.Base;
using HYS.Common.Dicom;
using HYS.Common.Xml;

namespace HYS.DicomAdapter.MWLServer
{
    static class Program
    {
        public static Logging Log;
        public static DataBase Database;
        public static DeviceDirManager DeviceMgt;
        public static MWLServerConfigMgt ConfigMgt;
        public const string AppName = "DICOM MWL SCP";
        public static string InterfaceName
        {
            get
            {
                if (DeviceMgt == null) return "";
                return DeviceMgt.DeviceDirInfor.Header.Name;
            }
        }
        public static bool StandAlone;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            StandAlone = true;

            PreLoading();

            Log.Write(AppName + " is running independently.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            BeforeExit();
        }

        internal static void PreLoading()
        {
            Log = new Logging(Application.StartupPath + "\\MWLServer.log");
            LoggingHelper.EnableApplicationLogging(Log);
            LoggingHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);

            ConfigMgt = new MWLServerConfigMgt();
            ConfigMgt.FileName = Application.StartupPath + "\\" + ConfigMgt.FileName;
            if (ConfigMgt.Load())
            {
                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
                DHelper.CharacterSetName = ConfigMgt.Config.CharacterSetName;
                DHelper.PersonNameEncodingRule = ConfigMgt.Config.PersonNameEncodingRule;
                PrivateTagHelper.PrivateTagList = ConfigMgt.Config.PrivateTagList;
                Log.Write("Character Set: " + DHelper.CharacterSetName);
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
            }

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

            Database = new DataBase(Program.ConfigMgt.Config.GWDataDBConnection);
            LoggingHelper.EnableDatabaseLogging(Database, Program.Log);
        }

        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }

    }
}