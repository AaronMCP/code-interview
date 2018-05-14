using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.IM.MessageDevices.FileAdapter.FileReader.Forms;
using HYS.IM.Common.Logging;
using HYS.IM.MessageDevices.FileAdapter.FileReader.Config;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base;
using System.IO;
using HYS.IM.Common.HL7v2.Encoding;

namespace HYS.IM.MessageDevices.FileAdapter.FileReader
{
    static class Program
    {

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
    }

    public class ProgramContext
    {

        public  string AppName = "FileReader";

        public  LogControler Log;
        public  ConfigManager<FileReaderConfig> ConfigMgr;
        public  EntityInitializeArgument AppArgument;

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

            string FileName = ConfigHelper.GetFullPath(Path.Combine(arg.ConfigFilePath, FileReaderConfig.CONFIG_FILE_NAME));

            ConfigMgr = new ConfigManager<FileReaderConfig>(FileName);

            if (ConfigMgr.Load())
            {
                ConfigMgr.Config._contextForTemplate = this;
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
                    ConfigMgr.Config._contextForTemplate = this;
                    CreateDefaultHL7AckTempalte();
                }
            }
        }
        internal  void CreateDefaultConfig()
        {
            try
            {
                FileReaderConfig cfg = new FileReaderConfig();
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

        internal  void CreateDefaultHL7AckTempalte()
        {
            try
            {
                ConfigMgr.Config.WriteHL7AckAATemplate(HL7MessageTemplates.SuccessResponse);
                ConfigMgr.Config.WriteHL7AckAETemplate(HL7MessageTemplates.ErrorResponse);
            }
            catch (Exception err)
            {
                Log.Write(err);
            }
        }

        public void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}
