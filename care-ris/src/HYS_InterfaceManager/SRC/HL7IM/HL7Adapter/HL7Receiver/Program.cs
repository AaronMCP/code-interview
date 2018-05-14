using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Forms;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Common.HL7v2.MLLP;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Config;
using HYS.IM.Messaging.Base.Config;
using System.IO;
using HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Controler;
using HYS.IM.Common.HL7v2.Encoding;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver
{
    /// <summary>
    /// This class contains the global variables of this message entity. 
    /// This class contains the .NET CLR entry point when this message entity run indenpendently (e.g. for unit testing purpose).
    /// </summary>
    static class Program
    {
        //public static string GetAge(string birthDate, string examDate)
        //{
        //    if (birthDate == null || birthDate.Length < 8) return "";
        //    if (examDate == null || examDate.Length < 8) return "";

        //    DateTime dtBirth;
        //    if (!DateTime.TryParseExact(birthDate.Substring(0, 8), "yyyyMMdd",
        //        null, System.Globalization.DateTimeStyles.None, out dtBirth))
        //        return "";

        //    DateTime dtExam;
        //    if (!DateTime.TryParseExact(examDate.Substring(0, 8), "yyyyMMdd",
        //        null, System.Globalization.DateTimeStyles.None, out dtExam))
        //        return "";

        //    int year = dtExam.Year - dtBirth.Year;
        //    if (year > 0) return year.ToString() + "Y";
        //    else if (year < 0) return "";

        //    TimeSpan span = dtExam.Subtract(dtBirth);
        //    if (span.Days > 30) return ((int)(span.Days / 30)).ToString() + "M";
        //    else if (span.Days > 0) return span.Days.ToString() + "D";
        //    else return "";
        //}

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

    internal class ProgramContext
    {
        public LogControler Log;
        public string AppName = "HL7InboundAdapter";
        public ConfigManager<HL7InboundConfig> ConfigMgr;
        public EntityInitializeArgument AppArgument;

        public void PreLoading()
        {
            EntityInitializeArgument arg = new EntityInitializeArgument();
            arg.ConfigFilePath = Application.StartupPath;
            arg.Description = "WinForm Entry";
            arg.LogConfig = new LogConfig();
            arg.LogConfig.LogType = LogType.Debug;
            arg.LogConfig.DumpData = true;
            PreLoading(arg);
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

            string FileName = ConfigHelper.GetFullPath(Path.Combine(arg.ConfigFilePath, HL7InboundConfig.HL7InboundConfigFileName));

            ConfigMgr = new ConfigManager<HL7InboundConfig>(FileName);

            if (ConfigMgr.Load())
            {
                ConfigMgr.Config._contextForDump = this;
                ConfigMgr.Config._contextForTemplate = this;
                ConfigMgr.Config._contextForTemplate2 = this;
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
                    //CreateDefaultHL7AckTempalte();        // the templates are packed in the PublishTempaltes folder when build

                    ConfigMgr.Config._contextForDump = this;
                    ConfigMgr.Config._contextForTemplate = this;
                    ConfigMgr.Config._contextForTemplate2 = this;
                }
            }
        }

        public void CreateDefaultConfig()
        {
            try
            {
                HL7InboundConfig cfg = new HL7InboundConfig();
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
        //public void CreateDefaultHL7AckTempalte()
        //{
        //    try
        //    {
        //        Program.ConfigMgr.Config.WriteHL7AckAATemplate(HL7MessageTemplates.SuccessResponse);
        //        Program.ConfigMgr.Config.WriteHL7AckAETemplate(HL7MessageTemplates.ErrorResponse);
        //    }
        //    catch (Exception err)
        //    {
        //        Log.Write(err);
        //    }
        //}

        public void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}
