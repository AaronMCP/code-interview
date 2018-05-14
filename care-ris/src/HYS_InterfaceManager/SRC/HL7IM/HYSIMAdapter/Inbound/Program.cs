using System;
using System.IO;
using System.Windows.Forms;
using HYS.IM.Common.Logging;
using HYS.IM.MessageDevices.CSBAdapter.Inbound.Config;
using HYS.IM.MessageDevices.CSBAdapter.Inbound.Forms;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound
{
    static class Program
    {
        //public const string AppName = "CSBrokerInboundAdapter";

        //public static LogControler Log;
        //public static ConfigManager<CSBrokerInboundConfig> ConfigMgr;
        //public static EntityInitializeArgument AppArgument;

        public static ProgramContext Context = new ProgramContext();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Context.PreLoading();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            Context.BeforeExit();
        }

    //    internal static void PreLoading()
    //    {
    //        EntityInitializeArgument arg = new EntityInitializeArgument();
    //        arg.ConfigFilePath = Application.StartupPath;
    //        arg.Description = "WinForm Entry";
    //        arg.LogConfig = new LogConfig();
    //        arg.LogConfig.DumpData = true;
    //        arg.LogConfig.LogType = LogType.Debug;
    //        PreLoading(arg);
    //    }
    //    internal static void PreLoading(EntityInitializeArgument arg)
    //    {
    //        AppArgument = arg;

    //        Log = new LogControler(AppName, arg.LogConfig);
    //        LogHelper.EnableApplicationLogging(Log);
    //        LogHelper.EnableXmlLogging(Log);
    //        Log.WriteAppStart(AppName);
    //        Log.Write(arg.ToLog());

    //        string FileName = ConfigHelper.GetFullPath(Path.Combine(arg.ConfigFilePath, CSBrokerInboundConfig.CONFIG_FILE_NAME));

    //        ConfigMgr = new ConfigManager<CSBrokerInboundConfig>(FileName);

    //        if (ConfigMgr.Load())
    //        {
    //            Log.Write("Load config succeeded. " + ConfigMgr.FileName);
    //        }
    //        else
    //        {
    //            Log.Write(LogType.Error, "Load config failed. " + ConfigMgr.FileName);
    //            Log.Write(LogType.Error, ConfigMgr.LastError.ToString());

    //            if (System.Windows.Forms.MessageBox.Show("Cannot load " + AppName + " configuration file. \r\n" +
    //                ConfigMgr.FileName + "\r\n\r\nDo you want to create a configuration file with default setting and continue?",
    //                AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    //            {
    //                CreateDefaultConfig();
    //            }
    //        }
    //    }
    //    internal static void CreateDefaultConfig()
    //    {
    //        try
    //        {
    //            CSBrokerInboundConfig cfg = new CSBrokerInboundConfig();
    //            ConfigMgr.Config = cfg;

    //            if (!ConfigMgr.Save())
    //            {
    //                Log.Write(ConfigMgr.LastError);
    //            }
    //        }
    //        catch (Exception err)
    //        {
    //            Log.Write(err);
    //        }
    //        return;
    //    }

    //    internal static void BeforeExit()
    //    {
    //        Log.WriteAppExit(AppName);
    //    }
    }
}
internal class ProgramContext
{
    public LogControler Log;
    public string AppName = "CSBrokerInboundAdapter";
    public ConfigManager<CSBrokerInboundConfig> ConfigMgr;
    public EntityInitializeArgument AppArgument;

    public void PreLoading()
    {
        EntityInitializeArgument arg = new EntityInitializeArgument();
        arg.ConfigFilePath = Application.StartupPath;
        arg.Description = "WinForm Entry";
        arg.LogConfig = new LogConfig();
        arg.LogConfig.DumpData = true;
        arg.LogConfig.LogType = LogType.Debug;
        PreLoading(arg);
    }
    public void PreLoading(EntityInitializeArgument arg)
    {
        AppArgument = arg;

        Log = new LogControler(arg.GetLogFileName(AppName), arg.LogConfig);
        LogHelper.EnableApplicationLogging(Log);
        LogHelper.EnableXmlLogging(Log);
        Log.WriteAppStart(AppName);
        Log.Write(arg.ToLog());

        string FileName = ConfigHelper.GetFullPath(Path.Combine(arg.ConfigFilePath, CSBrokerInboundConfig.CONFIG_FILE_NAME));

        ConfigMgr = new ConfigManager<CSBrokerInboundConfig>(FileName);

        if (ConfigMgr.Load())
        {
            ConfigMgr.Config._context = this;
            ConfigMgr.Config.EnsureBrokerErrorMessageFile();
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
                ConfigMgr.Config._context = this;
                ConfigMgr.Config.EnsureBrokerErrorMessageFile();
            }
        }
    }
    public void CreateDefaultConfig()
    {
        try
        {
            CSBrokerInboundConfig cfg = new CSBrokerInboundConfig();
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