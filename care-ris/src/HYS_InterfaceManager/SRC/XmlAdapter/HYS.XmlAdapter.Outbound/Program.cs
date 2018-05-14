using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.XmlAdapter.Outbound.Forms;
using HYS.XmlAdapter.Outbound.Objects;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Common.Net;

namespace HYS.XmlAdapter.Outbound
{
    static class Program
    {
        public static Logging Log;
        public static XIMOutboundConfigMgt ConfigMgt;
        public const string AppName = "XIM(HL7) Outbound Adapter";

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
            Log = new Logging(Application.StartupPath + "\\XIMOutbound.log");
            LoggingHelper.EnableApplicationLogging(Log);
            LoggingHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);

            ConfigMgt = new XIMOutboundConfigMgt();
            ConfigMgt.FileName = Application.StartupPath + "\\" + ConfigMgt.FileName;
            if (ConfigMgt.Load())
            {
                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                ConfigMgt.Config = new XIMOutboundConfig();
                ConfigMgt.Save();
            }

            XIMTransformHelper.EnableXSLTLogging(Log);
            SocketHelper.EnableSocketLogging(Log, ConfigMgt.Config.DumpSocketData); 
        }

        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}