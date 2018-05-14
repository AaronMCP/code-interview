using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Management;
using HYS.IM.Messaging.Management.Config;

namespace HYS.IM.Messaging.Management.Service
{
    static class Program
    {
        public const string AppName = "ManagementService";

        public static LogControler Log;
        public static ConfigManager<ServiceConfig> ConfigMgt;

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
            EntityInitializeArgument arg = new EntityInitializeArgument();
            arg.ConfigFilePath = Application.StartupPath;
            arg.Description = "WinForm Test";
            arg.LogConfig = new LogConfig();
            PreLoading(arg);
        }
        internal static void PreLoading(EntityInitializeArgument arg)
        {
            Log = new LogControler(AppName, arg.LogConfig);
            LogHelper.EnableApplicationLogging(Log);
            LogHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);
            Log.Write(arg.ToLog());

            string FileName = ConfigHelper.GetFullPath(Path.Combine(arg.ConfigFilePath, ServiceConfig.FileName));

            ConfigMgt = new ConfigManager<ServiceConfig>(FileName);

            if (ConfigMgt.Load())
            {
                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                Log.Write(LogType.Error, ConfigMgt.LastError.ToString());

                //if (System.Windows.Forms.MessageBox.Show("Cannot load " + AppName + " configuration file. \r\n" +
                //    ConfigMgr.FileName + "\r\n\r\nDo you want to create a configuration file with default setting and continue?",
                //    AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                ConfigMgt.Config = new ServiceConfig();
                    if (ConfigMgt.Save())
                    {
                        Log.Write("Save config succeeded. " + ConfigMgt.FileName);
                    }
                    else
                    {
                        Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                        Log.Write(ConfigMgt.LastError);
                    }
                //}
            }
        }
        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}