using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base;
using System.IO;

namespace HYS.IM.IPMonitor
{
    static class Program
    {
        public const string AppName = "IPMonitorService";

        public static LogControler Log;
        public static ConfigManager<MonitorConfig> ConfigMgt;
        public static string ConfigPath = "";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            if (!PreLoading()) return;

            bool flag = false;
            string name = "";
            if (ConfigMgt.Config.PolicyIP.Enable)
            {
                name = "CSHIPSwitcher";
            }
            else
            {
                name = "CSHFlagService";
            }
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, name, out flag);
            if (!flag)
            {
                MessageBox.Show("Application already exit. Please close it before start another one.", name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            BeforeExit();
        }

        internal static bool PreLoading()
        {
            EntityInitializeArgument arg = new EntityInitializeArgument();
            arg.ConfigFilePath = Application.StartupPath;
            arg.Description = "WinForm GUI";
            arg.LogConfig = new LogConfig();
            arg.LogConfig.LogType = LogType.Debug;
            arg.LogConfig.DurationDay = 7;
            
            if (PreLoading(arg)) return true;

            if (System.Windows.Forms.MessageBox.Show("Cannot load " + AppName + " configuration file. \r\n" +
                ConfigMgt.FileName + "\r\n\r\nDo you want to create a configuration file with default setting and continue?",
                AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return false;

            ConfigMgt.Config = new MonitorConfig();
            if (ConfigMgt.Save())
            {
                Log.Write("Save config succeeded. " + ConfigMgt.FileName);
                return true;
            }
            else
            {
                Log.Write(LogType.Error, "Save config failed. " + ConfigMgt.FileName);
                Log.Write(ConfigMgt.LastError);
                return false;
            }
        }
        internal static bool PreLoading(EntityInitializeArgument arg)
        {
            Log = new LogControler(AppName, arg.LogConfig);
            LogHelper.EnableApplicationLogging(Log);
            LogHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);
            Log.Write(arg.ToLog());

            ConfigPath = arg.ConfigFilePath;
            string FileName = ConfigHelper.GetFullPath(Path.Combine(arg.ConfigFilePath, MonitorConfig.ConfigFileName));

            ConfigMgt = new ConfigManager<MonitorConfig>(FileName);

            if (ConfigMgt.Load())
            {
                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
                return true;
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                Log.Write(LogType.Error, ConfigMgt.LastError.ToString());
                return false;
            }
        }
        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}
