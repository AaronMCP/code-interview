using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Forms;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;
using HYS.IM.Messaging.Management.Config;

namespace HYS.IM.Messaging.Composer
{
    static class Program
    {
        /// <summary>
        /// 20091217 to support deploying composer in a folder like SolutionFolder/Tools/Composer
        /// 20100419 to support deploying multiple integration solution in the integration platform
        /// </summary>
        /// <returns></returns>
        internal static string GetSolutionRoot()
        {
            //string path = Path.Combine(Application.StartupPath, "../");
            string path = Path.Combine(Application.StartupPath, "../../");  //20100419
            return path;
        }
        /// <summary>
        /// 20100419 to support deploying multiple integration solution in the integration platform
        /// </summary>
        /// <returns></returns>
        internal static string GetPlatformRoot()
        {
            string path = Path.Combine(Application.StartupPath, "../../../../Platform/");
            return path;
        }

        public const string AppName = "IntegrationSolutionComposer";
        public const string AppConfigFileName = SolutionConfig.SolutionDirFileName;

        public static LogControler Log;
        public static ConfigManager<SolutionConfig> ConfigMgt;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (PreLoading())
            {
                if (!HandleArguments(args))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain());
                }
            }

            BeforeExit();
        }

        internal static bool PreLoading()
        {
            //Log = new LogControler(AppName);
            //LogHelper.EnableApplicationLogging(Log);
            //LogHelper.EnableXmlLogging(Log);
            //Log.WriteAppStart(AppName);

            string filePath = Path.Combine(GetSolutionRoot(), AppConfigFileName);   //20091217
            ConfigMgt = new ConfigManager<SolutionConfig>(filePath);
            //ConfigMgt = new ConfigManager<SolutionConfig>(AppConfigFileName);
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

                if (MessageBox.Show("Cannot load " + AppName + " configuration file. \r\n" +
                    ConfigMgt.FileName + "\r\n\r\nDo you want to create a configuration file with default setting and continue?",
                    AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ConfigMgt.Config = SolutionConfig.GetDefault();// new SolutionConfig();
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

        static bool HandleArguments(string[] args)
        {
            if (args == null || args.Length < 1) return false;

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                switch (arg)
                {
                    case "-x":  // modify xml file
                        {
                            Program.Log.Write("Begin modifying xml file.");

                            string file = null;
                            string xpath = null;
                            string value = null;

                            i++;
                            if (i < args.Length) file = args[i];
                            i++;
                            if (i < args.Length) xpath = args[i];
                            i++;
                            if (i < args.Length) value = args[i];

                            ModifyXMLFile(file, xpath, value);

                            Program.Log.Write("End modifying xml file.");
                            return true;
                        }
                    case "-r":  // replace text in a text file
                        {
                            Program.Log.Write("Begin replacing text in a text file.");

                            string file = null;
                            string oldtext = null;
                            string newtext = null;

                            i++;
                            if (i < args.Length) file = args[i];
                            i++;
                            if (i < args.Length) oldtext = args[i];
                            i++;
                            if (i < args.Length) newtext = args[i];

                            ReplaceText(file, oldtext, newtext);

                            Program.Log.Write("End replacing text in a text file.");
                            return true;
                        }
                    case "-g": // generate script files 20090625
                        {
                            Program.Log.Write("Begin generating script files.");

                            ScriptGenerator.WriteCreateDBBatFile();
                            ScriptGenerator.WriteDropDBBatFile();
                            ScriptGenerator.WriteCreateVirtualPathBatFile();
                            ScriptGenerator.WriteDropVirtualPathBatFile();
                            ScriptGenerator.WriteCreateVirtualPathBatFile_iis6();
                            ScriptGenerator.WriteDropVirtualPathBatFile_iis6();
                            ScriptGenerator.WriteWebPortalShortCut();
                            ScriptGenerator.WriteApplyWebConfigShortCut();

                            Program.Log.Write("End generating script files.");
                            return true;
                        }
                    case "-a":  // register this integration solution into the platform
                        {
                            Program.Log.Write("Begin registering this integration solution into the platform.");

                            RegisterIntegrationSolution();

                            Program.Log.Write("End registering this integration solution into the platform.");
                            return true;
                        }
                    case "-d":  // unregister this integration solution into the platform
                        {
                            Program.Log.Write("Begin unregistering this integration solution into the platform.");

                            UnregisterIntegrationSolution();

                            Program.Log.Write("End unregistering this integration solution into the platform.");
                            return true;
                        }
                }
            }

            return false;
        }
        static void RegisterIntegrationSolution()
        {
            if (ConfigMgt.Config.Name == null ||
                ConfigMgt.Config.Name.Length < 1 ||
                ConfigMgt.Config.SolutionID == Guid.Empty)
            {
                string msg = "Cannot register an integration solutioin with empty name or empty ID to the platform.";
                Program.Log.Write(LogType.Error, msg);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pFile = Path.Combine(GetPlatformRoot(), PlatformConfig.PlatformDirFileName);
            ConfigManager<PlatformConfig> pMgt = new ConfigManager<PlatformConfig>(pFile);
            if (!pMgt.Load())
            {
                string msg = "Cannot load platform configuration file from " + pFile;
                Program.Log.Write(LogType.Error, msg + "\r\n" + pMgt.LastErrorInfor);

                if (MessageBox.Show(msg + "\r\nDo you want to create a default platform configuration file?",
                    AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                    != DialogResult.Yes)
                    return;

                pMgt.Config = new PlatformConfig();
                if (!pMgt.Save())
                {
                    msg = "Save integration platform configuration file failed.";
                    Program.Log.Write(LogType.Error, msg + "\r\n" + pMgt.LastErrorInfor);
                    MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            bool hasRegistered = false;
            foreach (SolutionInfo si in pMgt.Config.Solutions)
            {
                if (si.SolutionID == ConfigMgt.Config.SolutionID)
                {
                    hasRegistered = true;
                    break;
                }
            }

            if (hasRegistered)
            {
                string msg = string.Format(
                    "The integration solution (ID:{0},Name:{1}) has already exsited in the platform.",
                    ConfigMgt.Config.SolutionID.ToString(), ConfigMgt.Config.Name);
                Program.Log.Write(LogType.Error, msg);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SolutionInfo sInfo = new SolutionInfo();
            sInfo.SolutionID = ConfigMgt.Config.SolutionID;
            sInfo.Name = ConfigMgt.Config.Name;
            sInfo.Description = ConfigMgt.Config.Description;
            sInfo.SolutionVersion = ConfigMgt.Config.SolutionVersion;
            sInfo.SolutionUpdateDateTime = ConfigMgt.Config.SolutionUpdateDateTime;
            string sPath = ConfigHelper.DismissDotDotInThePath(GetSolutionRoot());
            string pPath = ConfigHelper.DismissDotDotInThePath(GetPlatformRoot());
            sInfo.SolutionPath = ConfigHelper.GetRelativePath(pPath, sPath);
            sInfo.WebSetting = ConfigMgt.Config.WebSetting;
            pMgt.Config.Solutions.Add(sInfo);

            if (!pMgt.Save())
            {
                string msg = "Save integration platform configuration file failed.";
                Program.Log.Write(LogType.Error, msg + "\r\n" + pMgt.LastErrorInfor);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string successMsg = string.Format(
                    "Register integration solution (ID:{0},Name:{1}) into the platform success.",
                    ConfigMgt.Config.SolutionID.ToString(), ConfigMgt.Config.Name);
            Program.Log.Write(successMsg);
            MessageBox.Show(successMsg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        static void UnregisterIntegrationSolution()
        {
            if (ConfigMgt.Config.Name == null ||
                ConfigMgt.Config.Name.Length < 1 ||
                ConfigMgt.Config.SolutionID == Guid.Empty)
            {
                string msg = "Cannot register an integration solutioin with empty name or empty ID to the platform.";
                Program.Log.Write(LogType.Error, msg);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string pFile = Path.Combine(GetPlatformRoot(), PlatformConfig.PlatformDirFileName);
            ConfigManager<PlatformConfig> pMgt = new ConfigManager<PlatformConfig>(pFile);
            if (!pMgt.Load())
            {
                string msg = "Cannot load platform configuration file from " + pFile;
                Program.Log.Write(LogType.Error, msg + "\r\n" + pMgt.LastErrorInfor);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SolutionInfo sInfo = null;
            foreach (SolutionInfo si in pMgt.Config.Solutions)
            {
                if (si.SolutionID == ConfigMgt.Config.SolutionID)
                {
                    sInfo = si;
                    break;
                }
            }

            if (sInfo == null)
            {
                string msg = string.Format(
                    "The integration solution (ID:{0},Name:{1}) does not exsited in the platform.",
                    ConfigMgt.Config.SolutionID.ToString(), ConfigMgt.Config.Name);
                Program.Log.Write(LogType.Error, msg);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            pMgt.Config.Solutions.Remove(sInfo);

            if (!pMgt.Save())
            {
                string msg = "Save integration platform configuration file failed.";
                Program.Log.Write(LogType.Error, msg + "\r\n" + pMgt.LastErrorInfor);
                MessageBox.Show(msg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string successMsg = string.Format(
                    "Unregister integration solution (ID:{0},Name:{1}) from the platform success.",
                    ConfigMgt.Config.SolutionID.ToString(), ConfigMgt.Config.Name);
            Program.Log.Write(successMsg);
            MessageBox.Show(successMsg, AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        static void ModifyXMLFile(string file, string xpath, string value)
        {
            if (file == null || file.Length < 1)
            {
                Program.Log.Write(LogType.Error, "XML file name argument is empty.");
                return;
            }

            if (xpath == null || xpath.Length < 1)
            {
                Program.Log.Write(LogType.Error, "XPath argument is empty.");
                return;
            }

            if (value == null || value.Length < 1)
            {
                Program.Log.Write(LogType.Error, "Value argument is empty.");
                return;
            }

            try
            {
                if (!Path.IsPathRooted(file)) file = Path.Combine(Application.StartupPath, file);

                XmlDocument doc = new XmlDocument();
                doc.Load(file);

                XmlNode node = doc.SelectSingleNode(xpath);
                if (node == null)
                {
                    Program.Log.Write(LogType.Error, "Cannot find XML node with XPath: " + xpath);
                }
                else
                {
                    node.InnerXml = value;
                }

                doc.Save(file);
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
            }
        }
        static void ReplaceText(string file, string oldtext, string newtext)
        {
            if (!Path.IsPathRooted(file)) file = Path.Combine(Application.StartupPath, file);

            Program.Log.Write("Reading file: " + file + "\r\n");

            string str;
            using (StreamReader sr = File.OpenText(file))
            {
                str = sr.ReadToEnd();
            }

            Program.Log.Write("Modifying from: " + oldtext + " to: " + newtext + "\r\n");

            str = str.Replace(oldtext, newtext);

            Program.Log.Write("Saving file: " + file + "\r\n");

            using (StreamWriter sw = File.CreateText(file))
            {
                sw.Write(str);
            }
        }
    }
}