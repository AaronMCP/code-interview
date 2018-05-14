using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Logging;
using HYS.Adapter.Base;
using HYS.Adapter.Monitor.Utility;

namespace HYS.Adapter.Monitor
{
    static class Program
    {
        public static bool InIM;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            Log.WriteAppStart("Adapter Monitor Tool"/*Application.ProductName*/);

            // read arguments
            foreach (string a in args)
            {
                switch (a.ToLower())
                {
                    case AdapterConfigArgument.InIM:
                        {
                            InIM = true;
                            break;
                        }
                }
            }

            if (!PreLoading()) {
                Log.Write(LogType.Error, "Open monitor failed!");
                return (int)AdapterConfigExitCode.Cancel;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LogTest());
            Application.Run(new Monitor());

            Log.WriteAppExit("Adapter Monitor Tool"/*Application.ProductName*/);

            return (int)AdapterConfigExitCode.Cancel;
        }

        public static Logging Log = new Logging("AdapterMonitor");
        public static AdapterMonitorCfgMgt ConfigMgt;
        public static DeviceDirManager DeviceMgt;
        public static DataAccess DataAccess;        
        internal static bool PreLoading()
        {
            #region Load DeviceDir
            DeviceMgt = new DeviceDirManager();
            DeviceMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
            if (!DeviceMgt.LoadDeviceDir())
            {
                Log.Write(LogType.Error, "Load DeviceDir failed. " + DeviceMgt.FileName);
                MessageBox.Show("Cannot load DeviceDir file.");
                return false;
            }
            else
            {
                // initialize config
                ConfigMgt = new AdapterMonitorCfgMgt();
                ConfigMgt.FileName = Application.StartupPath + "\\" + ConfigMgt.FileName;
                if (!ConfigMgt.Load())
                {
                    Log.Write(LogType.Error, "Load configuration failed. " + ConfigMgt.FileName);
                    MessageBox.Show("Cannot load configuration file.");
                    return false;
                }
                else
                {
                    string connStr = ConfigMgt.Config.DataDBConnection;
                    DataAccess = new DataAccess(connStr);
                    return true;
                }
            }
            #endregion
        }
    }
}