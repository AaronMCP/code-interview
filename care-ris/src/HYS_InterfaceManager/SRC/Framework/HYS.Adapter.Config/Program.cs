using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Adapter.Config.Forms;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Config
{
    static class Program
    {
        public static bool InIM;
        public static bool InIMWizard;

        public const string AppName = "Adapter Config Tool";
        internal static AdapterConfigExitCode ExitCode = AdapterConfigExitCode.Cancel;
        public static AdapterAgent<IOutboundAdapterConfig, AdapterConfigEntryAttribute> OutAdapter;
        public static AdapterAgent<IInboundAdapterConfig, AdapterConfigEntryAttribute> InAdapter;
        public static AdapterAgent<IBidirectionalAdapterConfig, AdapterConfigEntryAttribute> BiAdapter;
        public static AdapterServiceCfgMgt ServiceMgt;
        public static AdapterConfigCfgMgt ConfigMgt;
        public static DeviceDirManager DeviceMgt;
        public static Logging Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            // initialize logging
            Log = new Logging(Application.StartupPath + "\\AdapterConfig.log");            
            LoggingHelper.EnableApplicationLogging(Log);
            LoggingHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName, args);

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
                    case AdapterConfigArgument.InIMWizard:
                        {
                            InIMWizard = true;
                            break;
                        }
                }
            }

            // check multi instance
            if (CheckMultiInstance(args))
            {
                goto exit;
            }

            // load DeviceDir
            DeviceMgt = new DeviceDirManager();
            DeviceMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
            if (!DeviceMgt.LoadDeviceDir())
            {
                Log.Write(LogType.Error, "Load DeviceDir failed. " + DeviceMgt.FileName);
                DeviceMgt.DeviceDirInfor = new DeviceDir();
                MessageBox.Show("Cannot load DeviceDir file.");
            }

            // initialize config
            ConfigMgt = new AdapterConfigCfgMgt();
            ConfigMgt.FileName = Application.StartupPath + "\\" + ConfigMgt.FileName;
            if (ConfigMgt.Load())
            {
                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                goto exit;
            }

            // initialize service config
            ServiceMgt = new AdapterServiceCfgMgt();
            ServiceMgt.FileName = Application.StartupPath + "\\" + ServiceMgt.FileName;
            if (ServiceMgt.Load())
            {
                Log.Write("Load serivce config succeeded. " + ServiceMgt.FileName);
            }
            else
            {
                Log.Write(LogType.Error, "Load serivce config failed. " + ServiceMgt.FileName);
                ServiceMgt = null;
            }

            // log config parameters
            string adapterFileName = ConfigMgt.Config.AdapterFileName;
            DirectionType adapterDirection = ConfigMgt.Config.AdapterDirection;

            Log.Write("Adapter filename: " + adapterFileName, false);
            Log.Write("Adapter direction: " + adapterDirection, false);

            if (adapterFileName == null || adapterFileName.Length < 1) goto exit;

            // show window
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            try
            {
                switch (adapterDirection)
                {
                    case DirectionType.INBOUND:
                        {
                            // initialize inbound adapter agent
                            InAdapter = new AdapterAgent<IInboundAdapterConfig, AdapterConfigEntryAttribute>(adapterFileName, Log);
                            InitializeAdapter(InAdapter.Instance);
                            Application.Run(new FormConfigIn());
                            UninitiliazeAdapter(InAdapter.Instance);
                            break;
                        }
                    case DirectionType.OUTBOUND:
                        {
                            // initialize outbound adapter agent
                            OutAdapter = new AdapterAgent<IOutboundAdapterConfig, AdapterConfigEntryAttribute>(adapterFileName, Log);
                            InitializeAdapter(OutAdapter.Instance);
                            Application.Run(new FormConfigOut());
                            UninitiliazeAdapter(OutAdapter.Instance);
                            break;
                        }
                    case DirectionType.BIDIRECTIONAL:
                        {
                            // initialize bidiretional adapter agent
                            BiAdapter = new AdapterAgent<IBidirectionalAdapterConfig, AdapterConfigEntryAttribute>(adapterFileName, Log);
                            InitializeAdapter(BiAdapter.Instance);
                            Application.Run(new FormConfigBi());
                            UninitiliazeAdapter(BiAdapter.Instance);
                            break;
                        }
                }
            }
            catch (Exception err)
            {
                Log.Write(LogType.Error, "Error in loading main window.");
                Log.Write(err);
            }

        exit:
            // exit
            Log.WriteAppExit(AppName);
            return (int)ExitCode;
        }

        static void InitializeAdapter(IAdapterBase a)
        {
            if (a == null)
            {
                Log.Write(LogType.Error, "Load adapter failed. " + ConfigMgt.Config.AdapterFileName);
            }
            else
            {
                if (a.Initialize(new string[] {
                    Program.ConfigMgt.Config.DataDBConnection,
                    Program.ConfigMgt.Config.ConfigDBConnection }))
                {
                    Log.Write("Adapter initialized. " + ConfigMgt.Config.AdapterFileName);
                }
                else
                {
                    Log.Write(LogType.Error, "Initialize adapter failed. " + ConfigMgt.Config.AdapterFileName);
                    Log.Write(a.LastError);
                }
            }
        }
        static void UninitiliazeAdapter(IAdapterBase a)
        {
            if (a != null)
            {
                if (a.Exit(null))
                {
                    Log.Write("Adapter exited. " + ConfigMgt.Config.AdapterFileName);
                }
                else
                {
                    Log.Write(LogType.Error, "Exit adapter failed. " + ConfigMgt.Config.AdapterFileName);
                    Log.Write(a.LastError);
                }
            }
        }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.Write(LogType.Error, "Error within main window.");
            Log.Write(e.Exception);
        }
        static bool CheckMultiInstance(string[] args)
        {
            if (args != null)
            {
                foreach (string a in args)
                {
                    switch (a.ToLower())
                    {
                        case "-m":
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
    }
}