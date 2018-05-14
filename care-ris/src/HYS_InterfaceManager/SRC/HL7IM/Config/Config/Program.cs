using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.IM.Common.Logging;
using System.IO;
using HYS.IM.Messaging.Base.Config;
using System.Reflection;
using HYS.IM.Messaging.Base.Controler;

namespace HYS.IM.Config
{
    static class Program
    {
        public const string AppName = "HL7GatewayConfig";
        public static ConfigManager<HL7GatewayConfigConfig> ConfigMgr;
        public static LogControler Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            PreLoading();

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            if (!HandleArguments(args))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }

            BeforeExit();
        }
        static bool HandleArguments(string[] args)
        {
            if (args == null || args.Length < 1) return false;

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                switch (arg)
                {
                    case "-c":
                        {
                            Program.Log.Write("Begin configure the configuration GUI of HL7 Gateway.");
                            FormConfig frm = new FormConfig();
                            DialogResult res = frm.ShowDialog();
                            Program.Log.Write("End configure the configuration GUI of HL7 Gateway. Result: " + res.ToString());
                            return true;
                        }
                }
            }

            return false;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                Log.Write(LogType.Error, "Failed to resolve: " + args.Name);
                string fname = args.Name.Substring(0, args.Name.IndexOf(',')) + ".dll";
                string fpath = ConfigHelper.DismissDotDotInThePath(EntityLoader.LoadingAssemblyFileName);
                fpath = Path.GetDirectoryName(fpath);
                fname = Path.Combine(fpath, fname);
                Log.Write(LogType.Error, "Try to resolve: " + fname);

                return Assembly.LoadFile(fname);
            }
            catch (Exception err)
            {
                Log.Write(err);
                return null;
            }
        }

        internal static void PreLoading()
        {
            LogConfig lcfg = new LogConfig();
            lcfg.DumpData = true;
            lcfg.LogType = LogType.Debug;
            Log = new LogControler(AppName, lcfg);
            LogHelper.EnableApplicationLogging(Log);
            LogHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);

            string FileName = ConfigHelper.GetFullPath(HL7GatewayConfigConfig.ConfigFileName);
            ConfigMgr = new ConfigManager<HL7GatewayConfigConfig>(FileName);

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
                HL7GatewayConfigConfig cfg = new HL7GatewayConfigConfig();
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
