using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.IM.Forms;
using HYS.IM.Properties;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.License2;
using HYS.Adapter.Base;
using HYS.IM.WCFServer;

namespace HYS.IM
{
    static class Program
    {
        public const string AppName = "Interface Manager";
        public const string AllowMultiInstance = "-m";
        public static IMCfgMgt ConfigMgt;
        public static GWLicense License;
        public static DataBase ConfigDB;
        public static DataBase DataDB;
        public static Logging Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (!PreLoading(args)) return;

            // process arguments
            if (!ProcessArguments(args))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                FormLogin dlg = new FormLogin();
                if (dlg.ShowDialog()==DialogResult.OK)
                {
                    NotifyAdapterServer server = new NotifyAdapterServer("StatusNotifier");
                    server.Start();

                    License = dlg.License;
                    FormMain frm = new FormMain();
                    frm.WindowState = FormWindowState.Maximized;
                    //US28109
                    #region
                    System.Threading.Thread.Sleep(4000);
                    #endregion
                    Application.Run(frm);

                    server.Stop();
                }
            }

            BeforeExit();
        }

        static bool PreLoading(string[] args)
        {
            // initialize logging
            Log = new Logging(Application.StartupPath + "\\InterfaceManager.log");
            LoggingHelper.EnableApplicationLogging(Log);
            //LoggingHelper.EnableDatabaseLogging(Log);
            LoggingHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName, args);

            // check multi instance
            if (CheckMultiInstance(args))
            {
                Log.WriteAppExit(AppName);
                return false;
            }

            // initialize config
            ConfigMgt = new IMCfgMgt();
            ConfigMgt.FileName = Application.StartupPath + "\\" + ConfigMgt.FileName;
            if (ConfigMgt.Load())
            {
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
                    if (ConfigMgt.Save())
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

            // log config parameters
            string osqlFileName = ConfigMgt.Config.OSqlFileName;
            string osqlParameter = ConfigMgt.Config.OSqlParameter;
            string deviceFolder = Application.StartupPath + "\\" + ConfigMgt.Config.DeviceFolder;
            string interfaceFolder = Application.StartupPath + "\\" + ConfigMgt.Config.InterfaceFolder;

            Log.Write("Device folder: " + deviceFolder, false);
            Log.Write("Interface folder: " + interfaceFolder, false);
            Log.Write("OSql.exe file name: " + osqlFileName, false);
            //Log.Write("OSql.exe paramter: " + osqlParameter, false);      //contains db pwd

            // apply config paramters
            DataBase.OSQLFileName = osqlFileName;
            DataBase.OSQLArgument = osqlParameter;

            ConfigDB = new DataBase(ConfigMgt.Config.ConfigDBConnection);
            LoggingHelper.EnableDatabaseLogging(ConfigDB, Log);

            DataDB = new DataBase(ConfigMgt.Config.DataDBConnection);
            LoggingHelper.EnableDatabaseLogging(DataDB, Log);

            return true;
        }
        static void BeforeExit()
        {
            // exit
            Log.WriteAppExit(AppName);
        }

        private static bool ProcessArguments(string[] args)
        {
            if (args!=null)
            {
                string str = "";
                foreach (string a in args)
                {
                    str = str + " " + a;
                    switch (a.ToLower())
                    {
                        case "-dbm":
                            {
                                Application.Run(new FormDBMgt());
                                return true;
                            }
                        case "-lut":
                            {
                                Application.Run(new FormLUTMgt());
                                return true;
                            }
                        case "-pwd":
                            {
                                Application.Run(new FormPWD());
                                return true;
                            }
                        case "-updateinterface":
                            {
                                if (IM.BusinessControl.SystemControl.ScriptControl.UpdateDBConnection(
                                    Program.ConfigMgt.Config.InterfaceFolder,
                                    Program.ConfigMgt.Config.DataDBConnection,
                                    Program.ConfigMgt.Config.ConfigDBConnection))
                                {
                                    Program.Log.Write("Update database connection in interfaces succeeded. ");
                                    MessageBox.Show("Update database connection in interfaces succeeded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    Program.Log.Write("Update database connection in interfaces failed. ");
                                    Program.Log.Write(IM.BusinessControl.GCError.LastError);

                                    MessageBox.Show("Update database connection in interfaces failed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                return true;
                            }
                    }
                }
                Log.Write("Argument:" + str, false);
            }
            return false;
        }
        private static bool CheckMultiInstance(string[] args)
        {
            if (args != null)
            {
                foreach (string a in args)
                {
                    switch (a.ToLower())
                    {
                        case AllowMultiInstance:
                            {
                                Log.Write(LogType.Warning, "Allow multi instance, skip checking. ");
                                return false;
                            }
                    }
                }
            }

            try
            {
                Process cProc = Process.GetCurrentProcess();
                if (cProc == null)
                {
                    Log.Write(LogType.Warning, "Cannot find current process, skip checking. ");
                    return false;
                }

                string pName = cProc.ProcessName;
                Process[] pList = Process.GetProcessesByName(pName);
                if (pList != null)
                {
                    if (pList.Length > 1)
                    {
                        Log.Write(LogType.Warning, pList.Length.ToString() + " instances of " + pName + " are runing in the system. Current instance will exit.");

                        foreach (Process p in pList)
                        {
                            if (p.Id != cProc.Id)
                            {
                                Log.Write("Send SW_SHOW message to process ID: " + p.Id + " window handle: " + p.MainWindowHandle);
                                Win32Api.ShowWindow(p.MainWindowHandle, Win32Api.SW_SHOW);
                                Win32Api.PostMessage(p.MainWindowHandle, Win32Api.SW_SHOW, 0, 0);
                            }
                        }

                        return true;
                    }
                }

                Log.Write("One instance of " + pName + " is runing in the system. Multi instance checking completed.");
            }
            catch (Exception e)
            {
                Log.Write(LogType.Error, "Meet with exception when checking multi instance. ");
                Log.Write(e);
            }

            return false;
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
            Process.Start(fname, AllowMultiInstance);
        }
    }
}