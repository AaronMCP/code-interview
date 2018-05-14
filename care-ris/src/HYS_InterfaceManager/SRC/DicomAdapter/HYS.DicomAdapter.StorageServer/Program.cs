using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Dicom;
using HYS.Common.Objects.Logging;
using HYS.DicomAdapter.StorageServer.Dicom;
using HYS.DicomAdapter.StorageServer.Forms;
using HYS.DicomAdapter.StorageServer.Objects;
using HYS.Adapter.Base;

namespace HYS.DicomAdapter.StorageServer
{
    static class Program
    {
        public static Logging Log;
        public static StorageServerConfigMgt ConfigMgt;
        public const string AppName = "DICOM Storage SCP";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PreLoading();

            Log.Write(AppName + " is running independently.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            BeforeExit();
        }

        internal static void PreLoading()
        {
            Log = new Logging(Application.StartupPath + "\\StorageServer.log");
            LoggingHelper.EnableApplicationLogging(Log);
            LoggingHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);

            ConfigMgt = new StorageServerConfigMgt();
            ConfigMgt.FileName = Application.StartupPath + "\\" + ConfigMgt.FileName;
            if (ConfigMgt.Load())
            {
                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
                PrivateTagHelper.PrivateTagList = ConfigMgt.Config.PrivateTagList;
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
            }
        }

        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }

    }
}