using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.Common.Logging;

namespace Management.Test
{
    static class Program
    {
        public const string AppName = "ManagementService.Test";

        public static LogControler Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PreLoading();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            BeforeExit();
        }

        internal static void PreLoading()
        {
            LogConfig cfg = new LogConfig();
            cfg.LogType = LogType.Debug;
            cfg.DurationDay = 7;

            Log = new LogControler(AppName, cfg);
            LogHelper.EnableApplicationLogging(Log);
            LogHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);
        }
        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}
