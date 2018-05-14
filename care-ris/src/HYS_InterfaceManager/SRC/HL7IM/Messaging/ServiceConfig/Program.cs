using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;
using HYS.IM.Messaging.Management;
using System.IO;
using HYS.IM.Messaging.Management.NTServices;
using System.Reflection;

namespace HYS.IM.Messaging.ServiceConfig
{
    static class Program
    {
        internal static string GetSolutionRoot()
        {
            //string path = Path.Combine(Application.StartupPath, "../");
            string path = Path.Combine(Application.StartupPath, "../../");  //20100419
            return path;
        }

        public const string AppName = "NTServiceHostConfig";

        public static LogControler Log;
        public static ConfigManager<NTServiceHostConfig> ConfigMgt;
        public static DialogResult ConfigResult = DialogResult.Cancel;
        public static bool DisableModifyingEntity = false;
        public static bool AutoRunBatScript = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            //if (args != null && args.Length > 0 && args[0] == "-set") createDefaultConfigSilently = true;

            if (PreLoading())
            {
                AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

                if (!HandleArguments(args))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain());
                }
            }

            BeforeExit();

            return (int)((ConfigResult == DialogResult.OK) ? ServiceConfigExitCode.OK : ServiceConfigExitCode.Cancel);
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

        static bool _silence = false;
        static bool HandleArguments(string[] args)
        {
            if (args == null || args.Length < 1) return false;

            for(int i=0; i<args.Length ; i++)
            {
                string arg = args[i];
                switch (arg)
                {
                    case "-b":
                        {
                            AutoRunBatScript = true;
                            continue;
                        }
                    case "-d":
                        {
                            DisableModifyingEntity = true;
                            continue;
                        }
                    case "-s":
                        {
                            _silence = true;
                            continue;
                        }
                    case "-auto":
                        {
                            Program.Log.Write("Begin setting service as automatic start.");
                            bool res = ServiceMgt.SetServiceStartStyle(Program.ConfigMgt.Config.ServiceName, ServiceMgt.Automatic, Program.Log);
                            Program.Log.Write("Begin setting service as automatic start. result: " + res.ToString());
                            return true;
                        }
                    case "-manual":
                        {
                            Program.Log.Write("Begin setting service as manually start.");
                            bool res = ServiceMgt.SetServiceStartStyle(Program.ConfigMgt.Config.ServiceName, ServiceMgt.Manual, Program.Log);
                            Program.Log.Write("Begin setting service as manually start. result: " + res.ToString());
                            return true;
                        }
                    case "-r":  // register this NT Service Host into the integration solution
                        {
                            Program.Log.Write("Begin registering this NT Service Host into the integration solution.");
                            RegisterNTServiceHost();
                            Program.Log.Write("End registering this NT Service Host into the integration solution.");
                            return true;
                        }
                    case "-u":  // unregister this NT Service Host into the integration solution
                        {
                            Program.Log.Write("Begin unregistering this NT Service Host into the integration solution.");
                            UnregisterNTServiceHost();
                            Program.Log.Write("End unregistering this NT Service Host into the integration solution.");
                            return true;
                        }

                }
            }

            return false;
        }
        static void RegisterNTServiceHost()
        {
            if (ConfigMgt.Config.ServiceName == null ||
                ConfigMgt.Config.ServiceName.Length < 1)
            {
                string msg = "Cannot register an NT Service Host with empty name to the integration solution.";
                Program.Log.Write(LogType.Error, msg);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sFile = Path.Combine(GetSolutionRoot(), SolutionConfig.SolutionDirFileName);
            ConfigManager<SolutionConfig> sMgt = new ConfigManager<SolutionConfig>(sFile);
            if (!sMgt.Load())
            {
                string msg = "Cannot load integration solution configuration file from " + sFile;
                Program.Log.Write(LogType.Error, msg + "\r\n" + sMgt.LastErrorInfor);

                if (MessageBox.Show(msg + "\r\nDo you want to create a default integration solution configuration file?",
                    AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    != DialogResult.Yes)
                    return;

                sMgt.Config = new SolutionConfig();
                if (!sMgt.Save())
                {
                    msg = "Save integration solution configuration file failed.";
                    Program.Log.Write(LogType.Error, msg + "\r\n" + sMgt.LastErrorInfor);
                    MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            bool hasRegistered = false;
            foreach (NTServiceHostInfo hi in sMgt.Config.Hosts)
            {
                if (hi.ServiceName == ConfigMgt.Config.ServiceName)
                {
                    hasRegistered = true;
                    break;
                }
            }

            if (hasRegistered)
            {
                string msg = string.Format(
                    "The NT Service Host (Name:{0}) has already exsited in the integration solution.",
                    ConfigMgt.Config.ServiceName);
                Program.Log.Write(LogType.Error, msg);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NTServiceHostInfo hInfo = new NTServiceHostInfo();
            hInfo.ServiceName = ConfigMgt.Config.ServiceName;
            hInfo.ServiceDescription = ConfigMgt.Config.Description;
            string sPath = ConfigHelper.DismissDotDotInThePath(GetSolutionRoot());
            string hPath = Path.GetDirectoryName(ConfigMgt.FileName);
            hInfo.ServicePath = ConfigHelper.GetRelativePath(sPath, hPath);
            sMgt.Config.Hosts.Add(hInfo);

            if (!sMgt.Save())
            {
                string msg = "Save integration solution configuration file failed.";
                Program.Log.Write(LogType.Error, msg + "\r\n" + sMgt.LastErrorInfor);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string successMsg = string.Format(
                    "Register NT Service Host(Name:{0}) into the integration solution success.",
                    ConfigMgt.Config.ServiceName);
            Program.Log.Write(successMsg);
            
            if(!_silence) MessageBox.Show(successMsg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        static void UnregisterNTServiceHost()
        {
            if (ConfigMgt.Config.ServiceName == null ||
                ConfigMgt.Config.ServiceName.Length < 1)
            {
                string msg = "Cannot register an NT Service Host with empty name to the integration solution.";
                Program.Log.Write(LogType.Error, msg);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sFile = Path.Combine(GetSolutionRoot(), SolutionConfig.SolutionDirFileName);
            ConfigManager<SolutionConfig> sMgt = new ConfigManager<SolutionConfig>(sFile);
            if (!sMgt.Load())
            {
                string msg = "Cannot load integration solution configuration file from " + sFile;
                Program.Log.Write(LogType.Error, msg + "\r\n" + sMgt.LastErrorInfor);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NTServiceHostInfo hInfo = null;
            foreach (NTServiceHostInfo hi in sMgt.Config.Hosts)
            {
                if (hi.ServiceName == ConfigMgt.Config.ServiceName)
                {
                    hInfo = hi;
                    break;
                }
            }

            if (hInfo == null)
            {
                string msg = string.Format(
                    "The NT Service Host (Name:{0}) does not exsited in the integration solution.",
                    ConfigMgt.Config.ServiceName);
                Program.Log.Write(LogType.Error, msg);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sMgt.Config.Hosts.Remove(hInfo);

            if (!sMgt.Save())
            {
                string msg = "Save integration solution configuration file failed.";
                Program.Log.Write(LogType.Error, msg + "\r\n" + sMgt.LastErrorInfor);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            string successMsg = string.Format(
                    "Unregister NT Service Host (Name:{0}) from the integration solution success.",
                    ConfigMgt.Config.ServiceName);
            Program.Log.Write(successMsg);

            if (!_silence) MessageBox.Show(successMsg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static bool PreLoading()
        {
            string path = ConfigHelper.GetFullPath(NTServiceHostConfig.NTServiceHostConfigFileName);
            ConfigMgt = new ConfigManager<NTServiceHostConfig>(path);
            if (ConfigMgt.Load())
            {
                Log = new LogControler(AppName, ConfigMgt.Config.LogConfig);
                LogHelper.EnableApplicationLogging(Log);
                LogHelper.EnableXmlLogging(Log);
                Log.WriteAppStart(AppName);

                Log.Write("Load config succeeded. " + ConfigMgt.FileName);
                return true;
            }
            else
            {
                Log = new LogControler(AppName);
                LogHelper.EnableApplicationLogging(Log);
                LogHelper.EnableXmlLogging(Log);
                Log.WriteAppStart(AppName);

                Log.Write(LogType.Error, "Load config failed. " + ConfigMgt.FileName);
                Log.Write(ConfigMgt.LastError);

                if (//createDefaultConfigSilently ||
                    MessageBox.Show("Cannot load " + AppName + " configuration file. \r\n" +
                    ConfigMgt.FileName + "\r\n\r\nDo you want to create a configuration file with default setting and continue?",
                    AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ConfigMgt.Config = new NTServiceHostConfig();
                    if (ConfigMgt.Save())
                    {
                        Log.Write("Create config file succeeded. " + ConfigMgt.FileName);
                        return true;
                    }
                    else
                    {
                        Log.Write(LogType.Error, "Create config file failed. " + ConfigMgt.FileName);
                        Log.Write(ConfigMgt.LastError);
                        return false;
                    }
                }

                return false;
            }
        }
        internal static void BeforeExit()
        {
            Log.WriteAppExit(AppName);
        }
    }
}