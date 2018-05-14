using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.Common.Logging;

namespace HYS.MessageDevices.CSBAdapter.Test
{
    static class Program
    {
        public const string AppName = "CSBAdapter.Test";
        public static LogControler Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LogConfig lcfg = new LogConfig();
            lcfg.DumpData = true;
            lcfg.LogType = LogType.Debug;
            Log = new LogControler(AppName, lcfg);
            LogHelper.EnableApplicationLogging(Log);
            LogHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            Log.WriteAppExit(AppName);
        }
    }
}
