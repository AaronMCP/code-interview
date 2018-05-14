#region

using System;
using System.IO;
using System.Reflection;
using System.ServiceModel.Web;
using System.Windows;
using Hys.CareAgent.WcfService;
using Hys.CareAgent.WcfService.Contract;
using log4net;
using log4net.Config;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Windows.Threading;
using Hys.CareAgent.Common;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using AutoMapper;
using Hys.CareAgent.DAP;
using Application = System.Windows.Application;
using Timer = System.Threading.Timer;
using Hys.CareAgent.DICOMReceiver;
using System.Net;
using CefSharp;

#endregion

namespace Hys.CareAgent.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILog _logger = LogManager.GetLogger("App");

        private WebServiceHost _risProTaskHost;
        private Timer registerTimer;
        private Timer uploadTimer;
        private Timer confExitTimer;
        private int uploadInterval = 0;
        private DICOMReceiverService dicomReceiverService = null;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            DAP.Startup.AutoMapperStart();
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DAPContext, Hys.CareAgent.Main.Migrations.Configuration>());

            string proc = Process.GetCurrentProcess().ProcessName;
            Process[] processes = Process.GetProcessesByName(proc);
            if (processes.Length > 1)
            {
                System.Windows.MessageBox.Show("Program is already running");
                Shutdown();
                return;
            }
            WindowsFormsHost.EnableWindowsFormsInterop();
            // Catch all unhandled exceptions in all threads.
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
            Dispatcher.UnhandledException += UnhandledException;
            System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Catch all handled exceptions in managed code, before the runtime searches the Call Stack 
            //AppDomain.CurrentDomain.FirstChanceException += FirstChanceException;

            // Catch all unobserved task exceptions.
            TaskScheduler.UnobservedTaskException += UnobservedTaskException;

            // Catch all unhandled exceptions.
            System.Windows.Forms.Application.ThreadException += ThreadException;

            // Catch all WPF unhandled exceptions.
            Dispatcher.CurrentDispatcher.UnhandledException += CurrentDispatcher_UnhandledException;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");

            try
            {
                uploadInterval = Convert.ToInt32(ConfigurationManager.AppSettings["UploadInterval"]) * 1000;

                _risProTaskHost = new WebServiceHost(typeof(RisProTaskService));
                var fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "log4net.config");
                if (fileName.StartsWith(@"file:\"))
                {
                    fileName = fileName.Substring(@"file:\".Length);
                }
                XmlConfigurator.ConfigureAndWatch(new FileInfo(fileName));
                _logger.Info("Agent start...");

                _risProTaskHost.Open();
                StartDICOMReceiver();

                #region init CEF
                Cef.EnableHighDPISupport();
                var settings = new CefSettings()
                {
                    //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                    CachePath = Path.Combine(Directory.GetCurrentDirectory(), "CefSharp\\Cache"),
                    Locale = "zh-CN"
                };

                if (!Cef.IsInitialized)
                {
                    //Perform dependency check to make sure all relevant resources are in our output directory.
                    Cef.Initialize(settings, true, true);
                }
                #endregion

                setDefaultConfig();
                //reg
                registerTimer = new Timer(_ => registerTimer_Tick(), null, uploadInterval, Timeout.Infinite);

                //upload
                uploadTimer = new Timer(_ => uploadTimer_Tick(), null, uploadInterval, Timeout.Infinite);

                //upload
                confExitTimer = new Timer(_ => confExitTimer_Tick(), null, uploadInterval, Timeout.Infinite);

            }
            catch (Exception exception)
            {

                _logger.Error("App_OnStartup", exception);
            }

        }

        private void setDefaultConfig()
        {
            var currDir = Directory.GetCurrentDirectory();
            var userConfigDir = currDir + "\\userconfig";

            if (!Directory.Exists(userConfigDir))
            {
                Directory.CreateDirectory(userConfigDir);
            }

            var defaltPacs = "pacs.json";
            var pacsConfig = userConfigDir + "\\" + defaltPacs;

            if (!File.Exists(pacsConfig))
            {
                File.Copy(defaltPacs, pacsConfig);
            }
        }

        private void CurrentDispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Error("CurrentDispatcher_UnhandledException : " + e.Exception);
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            _logger.Error("ThreadException : " + e.Exception);
        }

        private void UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _logger.Error("UnobservedTaskException : " + e.Exception);
        }

        private void FirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            _logger.Error("FirstChanceException : " + e.Exception);
        }

        private void UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            _logger.Error("DispatcherUnhandledException : " + e.Exception);
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Error("UnhandledException : " + e.ExceptionObject);
        }

        private void registerTimer_Tick()
        {
            try
            {
                registerTimer.Dispose();
                //add timer for register folder or dcm file
                RegisterFileTask.RegisterERMFile();
            }
            catch (Exception exception)
            {
                _logger.Error("UploadERMFile", exception);
            }
            finally
            {
                registerTimer = new Timer(_ => registerTimer_Tick(), null, uploadInterval, Timeout.Infinite);
            }
        }

        private void uploadTimer_Tick()
        {

            try
            {
                uploadTimer.Dispose();
                UploadFileTask.UploadERMFile();
            }
            catch (Exception exception)
            {
                _logger.Error("UploadERMFile", exception);
            }
            finally
            {
                uploadTimer = new Timer(_ => uploadTimer_Tick(), null, uploadInterval, Timeout.Infinite);
            }

        }

        private void confExitTimer_Tick()
        {

            try
            {
                confExitTimer.Dispose();
                Process[] processes = Process.GetProcessesByName(Utilities.ConfProcessName);

                foreach (Process p in processes)
                {
                    if (p.MainWindowHandle == IntPtr.Zero)
                    {
                        p.Kill();
                        return;
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.Error("confExitTimer_Tick ", exception);
            }
            finally
            {
                confExitTimer = new Timer(_ => confExitTimer_Tick(), null, uploadInterval, Timeout.Infinite);
            }
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            try
            {
                _risProTaskHost.Close();

                Cef.Shutdown();

                _logger.Info("Agent end...");
            }
            catch (Exception exception)
            {
                _logger.Error("App_OnStartup", exception);
            }

        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Process unhandled exception
            _logger.Error("app error", e.Exception);
            // Prevent default unhandled exception processing
            e.Handled = true;
        }

        private void StartDICOMReceiver()
        {
            String acqFolder = ConfigurationManager.AppSettings["DICOMFilePath"];
            String aeTitle = ConfigurationManager.AppSettings["DICOMLocalAE"];
            Int32 LocalPort = 105;
            Int32 timeout = 10;
            try
            {
                LocalPort = Convert.ToInt32(ConfigurationManager.AppSettings["DICOMLocalPort"]);
            }
            catch (Exception exception)
            {
                _logger.Error("StartDICOMReceiver ", exception);
            }
            try
            {
                timeout = Convert.ToInt32(ConfigurationManager.AppSettings["DICOMTimeOut"]);
            }
            catch (Exception exception)
            {
                _logger.Error("StartDICOMReceiver ", exception);
            }

            dicomReceiverService = new DICOMReceiverService(aeTitle, (ushort)LocalPort, acqFolder, timeout);
            dicomReceiverService.Start();
        }
    }
}