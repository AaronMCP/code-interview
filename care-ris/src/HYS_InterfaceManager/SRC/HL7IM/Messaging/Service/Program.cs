using System.Text;
using System.ServiceProcess;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;
using HYS.IM.Messaging.Service.Services;
using System;
using System.IO;
using System.Reflection;
using HYS.Common.Objects.License2;
using HYS.Common.Objects.Device;

namespace HYS.IM.Messaging.Service
{
    static class Program
    {
        public const string AppName = "NTServiceHost";
        public const string AppConfigFileName = NTServiceHostConfig.NTServiceHostConfigFileName;    // "NTServiceHost.xml";;
        public const string AppLicenseFileName = LicenseConfig.LicenseConfigFileName;

        public static LogControler Log;
        public static ConfigManager<NTServiceHostConfig> ConfigMgt;
        public static LicenseManager<LicenseConfig> License;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (PreLoading())
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                ServiceBase[] ServicesToRun = new ServiceBase[] { new ServiceMain() };
                ServiceBase.Run(ServicesToRun);
            }

            BeforeExit();
        }

        /// <summary>
        /// The NT Service should maintain the entity folder in a list 
        /// and search through the list when loading dll files. 
        /// This is a simple solution. 
        /// More complex and accurate solution is 
        /// identifying the dll requesting entity by using the StakeTrace class. 
        /// 
        /// Currently, we fixed this bug by using the simple solution, 
        /// which depend on the assumption of dll files in different entity folders 
        /// with the same name should be also the same version.
        ///
        /// In future version, as a replacement of EntityLoader.LoadingAssemblyFileNames,
        /// use StackFrame or StackTrace to get current calling context,
        /// and load additional assembly from the exact folder,
        /// so as to support that some message entity may
        /// load additional assembly at runtime.
        /// 
        /// http://www.itwis.com/html/net/c/20090304/3505.html
        /// http://www.kuqin.com/dotnet/20080420/7078.html
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            try
            {
                Log.Write(LogType.Warning, "Failed to resolve: " + args.Name);

                string fn = "";
                int index = args.Name.IndexOf(',');
                if (index < 0) fn = args.Name;
                else fn = args.Name.Substring(0, index);

                //string fname = fn + ".dll";
                //string fpath = ConfigHelper.DismissDotDotInThePath(EntityLoader.LoadingAssemblyFileName);
                //fpath = Path.GetDirectoryName(fpath);
                //fname = Path.Combine(fpath, fname);
                //Log.Write(LogType.Warning, "Try to resolve: " + fname);

                //return Assembly.LoadFile(fname);

                string filename = null;
                for (int i = EntityLoader.LoadingAssemblyFileNames.Count - 1; i >= 0; i--)
                {
                    string fname = fn + ".dll";
                    string fpath = ConfigHelper.DismissDotDotInThePath(EntityLoader.LoadingAssemblyFileNames[i]);
                    fpath = Path.GetDirectoryName(fpath);
                    fname = Path.Combine(fpath, fname);
                    if (File.Exists(fname))
                    {
                        filename = fname;
                        break;
                    }
                }

                Log.Write(LogType.Warning, "Try to resolve: " + filename);
                return Assembly.LoadFile(filename);
            }
            catch (Exception err)
            {
                Log.Write(err);
                return null;
            }
        }

        internal static bool PreLoading()
        {
            //Log = new LogControler(AppName);
            //LogHelper.EnableApplicationLogging(Log);
            //LogHelper.EnableXmlLogging(Log);
            //Log.WriteAppStart(AppName);

            ConfigMgt = new ConfigManager<NTServiceHostConfig>(AppConfigFileName);
            if (ConfigMgt.Load())
            {
                Log = new LogControler(AppName, ConfigMgt.Config.LogConfig);
                LogHelper.EnableApplicationLogging(Log);
                LogHelper.EnableXmlLogging(Log);
                Log.WriteAppStart(AppName);

                Log.Write("Load config succeeded. " + ConfigMgt.FileName);

                License = new LicenseManager<LicenseConfig>(AppLicenseFileName);
                License.EnabledCrypto = true;

                if (License.Load())
                {
                    Log.Write("Load license succeeded. " + License.FileName);
                    return true;
                }
                else
                {
                    //CreateReceiverDefaultLicense();
                    Log.Write(LogType.Error, "Load license failed. " + License.FileName);
                    Log.Write(License.LastError);
                    return false;
                }

            }
            else
            {
                Log = new LogControler(AppName);
                LogHelper.EnableApplicationLogging(Log);
                LogHelper.EnableXmlLogging(Log);
                Log.WriteAppStart(AppName);

                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                Log.Write(ConfigMgt.LastError);
                return false;
            }
        }

        private static void CreateReceiverDefaultLicense()
        {
            LicenseConfig config = new LicenseConfig();
            config.DeviceName = DeviceName.HL7_RECEIVER.ToString();
            config.Type = DeviceType.HL7.ToString();
            config.Direction = DirectionType.INBOUND.ToString();
            config.Enabled = true;

            License.Config = config;
            if (License.Save())
            {
                Log.Write("License Created.");
            }
            else
            {
                Log.Write("Create License failed.");
            }
        }

        private static void CreateSenderDefaultLicense()
        {
            LicenseConfig config = new LicenseConfig();
            config.DeviceName = DeviceName.HL7_SENDER.ToString();
            config.Type = DeviceType.HL7.ToString();
            config.Direction = DirectionType.OUTBOUND.ToString();
            config.Enabled = true;

            License.Config = config;
            if (License.Save())
            {
                Log.Write("License Created.");
            }
            else
            {
                Log.Write("Create License failed.");
            }
        }

        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}