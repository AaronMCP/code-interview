using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.IM.Common.Logging;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Forms;
using HYS.IM.MessageDevices.HL7Adapter.HL7Sender.Config;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Sender
{
    /// <summary>
    /// This class contains the global variables of this message entity. 
    /// This class contains the .NET CLR entry point when this message entity run indenpendently (e.g. for unit testing purpose).
    /// </summary>
    static class Program
    {
        public static ProgramContext Context = new ProgramContext();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Context.Args = args;
            Context.PreLoading();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            foreach (string arg in args)
            {
                Program.Context.Log.Write(arg);
            }

            Context.BeforeExit();
        }
    }

    internal class ProgramContext
    {
        public LogControler Log;
        public string AppName = "HL7OutboundAdapter";
        public ConfigManager<HL7OutboundConfig> ConfigMgr;
        public EntityInitializeArgument AppArgument;
        public string[] Args;

        public void PreLoading()
        {
            EntityInitializeArgument arg = new EntityInitializeArgument();
            arg.ConfigFilePath = Application.StartupPath;
            arg.Description = "WinForm Entry";
            arg.LogConfig = new LogConfig();
            arg.LogConfig.LogType = GetLogType();
            arg.LogConfig.DumpData = true;
            PreLoading(arg);
        }

        private LogType GetLogType()
        {
            if (Args == null || Args.Length == 0)
            {
                return LogType.Debug;
            }

            switch (Args[0].Trim())
            {
                case "-e":
                case "-E": return LogType.Error;
                case "-d":
                case "-D": return LogType.Debug;
                case "-i":
                case "-I": return LogType.Information;
                case "-w":
                case "-W": return LogType.Warning;
                default: return LogType.Debug;
            }
        }

        public void PreLoading(EntityInitializeArgument arg)
        {
            AppArgument = arg;

            Log = new LogControler(arg.GetLogFileName(AppName), arg.LogConfig);
            LogHelper.EnableApplicationLogging(Log);
            LogHelper.EnableXmlLogging(Log);
            SocketHelper.EnableSocketLogging(Log, arg.LogConfig.DumpData);
            Log.WriteAppStart(AppName);
            Log.Write(arg.ToLog());

            string FileName = ConfigHelper.GetFullPath(Path.Combine(arg.ConfigFilePath, HL7OutboundConfig.HL7OutboundConfigFileName));

            ConfigMgr = new ConfigManager<HL7OutboundConfig>(FileName);

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
        public void CreateDefaultConfig()
        {
            try
            {
                HL7OutboundConfig cfg = new HL7OutboundConfig();
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
        public void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}
