using System;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Adapter.Composer.Forms;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Config;
using HYS.Common.Xml;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Composer
{
    static class Program
    {
        public const string AppName = "Adapter Composer";
        public static AdapterComposerCfgMgt ConfigMgt;
        public static Logging Log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // initialize logging
            Log = new Logging(Application.StartupPath + "\\AdapterComposer.log");
            LoggingHelper.EnableApplicationLogging(Log);
            LoggingHelper.EnableXmlLogging(Log);
            Log.WriteAppStart(AppName);

            // initialize config
            ConfigMgt = new AdapterComposerCfgMgt();
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
                return;
            }

            // log config parameters
            string deviceFileName = ConfigMgt.Config.DeviceDirFileName;
            string configFileName = ConfigMgt.Config.ConfigConfigFileName;
            string serviceFileName = ConfigMgt.Config.ServiceConfigFileName;

            Log.Write("Device index filename: " + deviceFileName, false);
            Log.Write("Config config filename: " + configFileName, false);
            Log.Write("Service config filename: " + serviceFileName, false);

            // process arguments
            string arg1 = null;
            string arg2 = null;

            if (args.Length > 0) arg1 = args[0];
            if (args.Length > 1) arg2 = args[1];

            Log.Write("Argument 1: " + arg1, false);
            Log.Write("Argument 2: " + arg2, false);

            if (arg1 == null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            else
            {
                HandleArgument(arg1, arg2);
            }

            // exit
            Log.WriteAppExit(AppName);
        }
        static void HandleArgument(string arg1, string arg2)
        {
            switch (arg1)
            {
                case "-config":
                    {
                        if (arg2 == null)
                        {
                            Application.Run(new FormConfig());
                        }
                        else
                        {
                            SetConfigValue2(new AdapterConfigCfgMgt
                                (ConfigMgt.Config.ConfigConfigFileName), arg2);
                        }
                        break;
                    }
                case "-service":
                    {
                        if (arg2 == null)
                        {
                            Application.Run(new FormService());
                        }
                        else
                        {
                            SetConfigValue2(new AdapterServiceCfgMgt
                                (ConfigMgt.Config.ServiceConfigFileName), arg2);
                        }
                        break;
                    }
                case "-device":
                    {
                        if (arg2 == null)
                        {
                            Application.Run(new FormDevice());
                        }
                        else
                        {
                            SetDeviceValue(arg2);
                        }
                        break;
                    }
                case "-iem":
                    {
                        if (arg2 != null)
                        {
                            SetConfigValue2(new IMCfgMgt
                                (ConfigHelper.IMDefaultFileName), arg2);
                        }
                        break;
                    }
            }
        }
        //static void SetConfigValue(ControlerBase cb, string arg)
        //{
        //    if (cb == null) return;

        //    Tag t = GetTag(arg);
        //    if (t == null) return;

        //    if (!cb.Load())
        //    {
        //        Log.Write(LogType.Error,"Load config file failed. " + cb.GetFileName());
        //        Log.Write(cb.LastError);
        //        return;
        //    }

        //    if (!cb.SetValue(t.Key, t.Value))
        //    {
        //        Log.Write(LogType.Error, "Set value failed.");
        //        Log.Write(cb.LastError);
        //        return;
        //    }

        //    if (!cb.Save())
        //    {
        //        Log.Write(LogType.Error, "Save config file failed. " + cb.GetFileName());
        //        Log.Write(cb.LastError);
        //        return;
        //    }

        //    Log.Write("Update config file succeeded.");
        //}
        static void SetConfigValue2(ConfigMgtBase cb, string arg)
        {
            if (cb == null) return;

            Tag t = GetTag(arg);
            if (t == null) return;

            if (!cb.Load())
            {
                Log.Write(LogType.Error, "Load config file failed. " + cb.FileName);
                Log.Write(cb.LastError);
                return;
            }

            if (!cb.SetValue(t.Key, t.Value))
            {
                Log.Write(LogType.Error, "Set value failed.");
                Log.Write(cb.LastError);
                return;
            }

            if (!cb.Save())
            {
                Log.Write(LogType.Error, "Save config file failed. " + cb.FileName);
                Log.Write(cb.LastError);
                return;
            }

            Log.Write("Update config file succeeded.");
        }
        static void SetDeviceValue(string arg)
        {
            Tag t = GetTag(arg);
            if (t == null) return;

            DeviceDirManager dir = new DeviceDirManager();
            dir.FileName = DeviceDirManager.IndexFileName;

            if (!dir.LoadDeviceDir())
            {
                Log.Write(LogType.Error, "Load DeviceDir file failed. " + dir.FileName);
                Log.Write(dir.LastError);
            }

            try
            {
                object res = dir.DeviceDirInfor.Header.GetType().InvokeMember(t.Key,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                    null, dir.DeviceDirInfor.Header, new object[] { t.Value });
            }
            catch (Exception err)
            {
                Log.Write(LogType.Error, "Set value failed.");
                Log.Write(err);
            }

            if (!dir.SaveDeviceDir())
            {
                Log.Write(LogType.Error, "Save DeviceDir file failed. " + dir.FileName);
                Log.Write(dir.LastError);
            }

            Log.Write("Update DeviceDir file succeeded.");
        }
        static Tag GetTag(string arg)
        {
            if (arg == null||arg.Length < 1) return null;

            int index = arg.IndexOf('=');
            string key = arg.Substring(0, index).Trim();

            index++;
            string value = "";
            if (index < arg.Length)
            {
                value = arg.Substring(index, arg.Length - index);
            }

            Tag t = new Tag();
            t.Value = value;
            t.Key = key;

            return t;
        }

        class Tag
        {
            public string Key;
            public string Value;
        }
    }
}