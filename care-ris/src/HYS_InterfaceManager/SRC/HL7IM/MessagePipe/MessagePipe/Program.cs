using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.MessageDevices.MessagePipe.Forms;
using HYS.Common.Logging;
using HYS.Messaging.Base.Config;
using HYS.MessageDevices.MessagePipe.Config;
using HYS.Messaging.Base;
using System.IO;

namespace HYS.MessageDevices.MessagePipe
{
    /// <summary>
    /// This class contains the global variables of this message entity. 
    /// This class contains the .NET CLR entry point when this message entity run indenpendently (e.g. for unit testing purpose).
    /// </summary>
    static class Program
    {
        public const string AppName = "MessagePipe";

        public static LogControler Log;
        public static ConfigManager<MessagePipeConfig> ConfigMgr;
        public static EntityInitializeArgument AppArgument;

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
            arg.Description = "WinForm Entry";
            arg.LogConfig = new LogConfig();
            arg.LogConfig.DumpData = true;
            arg.LogConfig.LogType = LogType.Debug;
            PreLoading(arg);
        }
        internal static void PreLoading(EntityInitializeArgument arg)
        {
            AppArgument = arg;

            Log = new LogControler(AppName, arg.LogConfig);
            LogHelper.EnableApplicationLogging(Log);
            LogHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);
            Log.Write(arg.ToLog());

            string FileName = ConfigHelper.GetFullPath(Path.Combine(arg.ConfigFilePath, MessagePipeConfig.MessagePipeConfigFileName));

            ConfigMgr = new ConfigManager<MessagePipeConfig>(FileName);

            if (ConfigMgr.Load())
            {
                Log.Write("Load config succeeded. " + ConfigMgr.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgr.FileName);
                Log.Write(LogType.Error, ConfigMgr.LastError.ToString());

                if (System.Windows.Forms.MessageBox.Show("Cannot load " + AppName + " configuration file. \r\n" +
                    ConfigMgr.FileName + "\r\n\r\nDo you want to create a configuration file with default setting and continue?",
                    AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CreateDefaultConfig();
                }
            }
        }
        internal static void CreateDefaultConfig()
        {
            try
            {
                MessagePipeConfig cfg = new MessagePipeConfig();
                ConfigMgr.Config = cfg;

                if (!ConfigMgr.Save())
                {
                    Log.Write(ConfigMgr.LastError);
                }
            }
            catch (Exception err)
            {
                Log.Write(err);
            }
            return;
        }
        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}
