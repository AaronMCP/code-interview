using System;
using System.Diagnostics;
using System.Windows.Forms;
using HYS.Common.Objects.License2;
using HYS.IM.Common.Logging;
using HYS.HL7IM.Manager.Config;
using HYS.HL7IM.Manager.Logging;
using HYS.HL7IM.Manager.Forms;
using HYS.HL7IM.Manager.Controler;

namespace CSH.eHeath.HL7Gateway.Manager
{
    static class Program
    {
        public const string AppName = "HL7Gateway Interface Manager";
        //public const string AllowMultiInstance = "-m";
        public static ConfigMgt<ManagerConfig> ConfigMgt;
        public static GWLicense License;
        public static LogControler Log;
        public static GWLoggingWapper LogWapper;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (!PreLoading(args)) return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!MultiInstanceCheck())
            {
                FormLogin dlg = new FormLogin();
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    License = dlg.License;
                    FormMain frm = new FormMain();
                    //frm.WindowState = FormWindowState.Normal;
                    Application.Run(frm);
                }
            }

            BeforeExit();
        }

        private static bool MultiInstanceCheck()
        {
            Process cp = Process.GetCurrentProcess();
            Process[] ps = Process.GetProcessesByName(cp.ProcessName);
            bool isRunning = false;
            foreach (Process p in ps)
            {
                if (p.Id != cp.Id && p.MainModule.FileName == cp.MainModule.FileName)
                {
                    HandleRunningInstance(p);
                    isRunning = true;
                }
            }

            return isRunning;
        }

        public static void HandleRunningInstance(Process instance)
        {
            //Make   sure   the   window   is   not   minimized   or   maximized  
            Win32API.ShowWindowAsync(instance.MainWindowHandle, Win32API.WS_SHOWNORMAL);
            //Set   the   real   intance   to   foreground   window
            Win32API.SetForegroundWindow(instance.MainWindowHandle);
        }


        static bool PreLoading(string[] args)
        {
            Log = new LogControler(AppName);
            LogHelper.EnableApplicationLogging(Log);
            LogHelper.EnableXmlLogging(Log);
            LogWapper = new GWLoggingWapper(Log);

            Log.WriteAppStart(AppName);

            // initialize config
            ConfigMgt = new ConfigMgt<ManagerConfig>();
            ConfigMgt.FileName = Application.StartupPath + "\\" + ManagerConfig.FileName;
            if (ConfigMgt.Load(LogWapper))
            {
                Log = new LogControler(AppName,ConfigMgt.Config.LogType);
                LogHelper.EnableApplicationLogging(Log);
                LogHelper.EnableXmlLogging(Log);
                LogWapper = new GWLoggingWapper(Log);

                Program.ConfigMgt.Config.RefreshConfigInfo();
                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                Log.Write(ConfigMgt.LastErrorInfor);

                if (MessageBox.Show("Cannot load " + AppName + " config file. \r\n" +
                    ConfigMgt.FileName + "\r\n\r\nDo you want to create a config file with default setting?",
                    AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ConfigMgt.Config = ManagerConfig.CreateDefaultConfig();
                    Program.ConfigMgt.Config.RefreshConfigInfo();

                    if (ConfigMgt.Save(LogWapper))
                    {
                        Log.Write("Create config file succeeded. " + ConfigMgt.FileName);
                    }
                    else
                    {
                        Log.Write(LogType.Error, "Create config file failed. " + ConfigMgt.FileName);
                        Log.Write(ConfigMgt.LastErrorInfor);
                    }
                }

                Log.WriteAppExit(AppName);
                return false;
            }

            return true;
        }
        static void BeforeExit()
        {
            // exit
            Log.WriteAppExit(AppName);
        }

        // Restart
        public static void Restart()
        {
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.Exit();
        }
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Process proc = Process.GetCurrentProcess();
            string fname = proc.MainModule.FileName;
            Process.Start(fname);
        }

        internal static bool SaveConfig()
        {
            if (ConfigMgt.Save(LogWapper))
            {
                Log.Write("Save config file succeeded. " + ConfigMgt.FileName);
                return true;
            }
            else
            {
                Log.Write(LogType.Error, "Save config file failed. " + ConfigMgt.FileName);
                Log.Write(ConfigMgt.LastErrorInfor);
                return false;
            }
        }
    }
}