using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.HL7IM.Manager.Config;
using CSH.eHeath.HL7Gateway.Manager;
using System.IO;
using System.Windows.Forms;
using HYS.IM.Messaging.Management.Scripts;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Common.Logging;

namespace HYS.HL7IM.Manager.Controler
{
    public static class HL7GatewayInterfaceHelper
    {
        public static bool InstallInterface(string strServiceName, InterfaceType iType, UpdateProgress updateProgress)
        {
            HL7InterfaceConfig config = CreateHL7InterfaceConfig(strServiceName, iType);
            Program.ConfigMgt.Config.InterfaceList.Add(config);

            try
            {
                string strPrototypePath = Path.Combine(Application.StartupPath, iType == InterfaceType.Receiver ? "..\\Prototype\\Receiver" : "..\\Prototype\\Sender");
                string targetPath = Path.Combine(Application.StartupPath, config.InterfaceFolder);
                CopyDirectory(strPrototypePath, targetPath, updateProgress);

                string strSvcRelativePath = "Bin\\Services\\" + (iType == InterfaceType.Receiver ? "HL7GW_RCV" : "HL7GW_SND");
                string targetServicePath = Path.Combine(targetPath, strSvcRelativePath);
                string svcCfgExe = Path.Combine(targetServicePath, "HYS.IM.Messaging.ServiceConfig.exe");
                ScriptMgt.ExecuteAssembly(svcCfgExe, "-s -u", true, Program.Log);

                HL7ConfigHelper.UpdateInterfaceName(config);

                string installSvcBat = Path.Combine(targetServicePath, "InstallService.bat");
                string uninstallSvcBat = Path.Combine(targetServicePath, "UninstallService.bat");

                ScriptMgt.ExecuteAssembly(installSvcBat, "", true, Program.Log);
                ScriptMgt.ExecuteAssembly(svcCfgExe, "-s -r", true, Program.Log);

                GenerateBat(targetServicePath, strServiceName);
                UpdateUnistallBat(Program.ConfigMgt.Config);

                if (!Program.SaveConfig())
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Error, "Install interface failed.");
                Program.Log.Write(err);
                Program.ConfigMgt.Config.InterfaceList.Remove(config);
                return false;
            }

            return true;
        }

        private static void GenerateBat(string strPath, string strServiceName)
        {
            string startFName = Path.Combine(strPath, "StartService.bat");
            string stopFName = Path.Combine(strPath, "StopService.bat");

            using (StreamWriter sw = File.CreateText(startFName))
            {
                sw.Write("net start " + strServiceName);
            }

            using (StreamWriter sw = File.CreateText(stopFName))
            {
                sw.Write("net stop " + strServiceName);
            }
        }

        public delegate void UpdateProgress(int iMaximum, int current, string strDescription);

        public static void CopyDirectory(string strSourceDir, string strDistDir, UpdateProgress updateProgress)
        {
            DirectoryInfo source = new DirectoryInfo(strSourceDir);
            DirectoryInfo target = new DirectoryInfo(strDistDir);

            if (target.FullName.StartsWith(source.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new Exception("Can not copy parent directory to child directory.");
            }

            if (!source.Exists)
            {
                return;
            }

            if (!target.Exists)
            {
                target.Create();
            }

            FileInfo[] files = source.GetFiles();
            string strPrefix = source.Name + "->" + target.Name;
            int length = files.Length;
            if (null != updateProgress) updateProgress(length, 0, strPrefix);
            for (int i = 0; i < length; i++)
            {
                FileInfo targetFile = new FileInfo(target.FullName + @"\" + files[i].Name);
                if (targetFile.Exists)
                {
                    targetFile.Attributes = FileAttributes.Normal;
                }
                File.Copy(files[i].FullName, target.FullName + @"\" + files[i].Name, true);
                if (null != updateProgress) updateProgress(length, i, strPrefix + ":" + files[i].Name);
            }
            if (null != updateProgress) updateProgress(length, length, strPrefix);

            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                CopyDirectory(dir.FullName, target.FullName + @"\" + dir.Name, updateProgress);
            }
        }

        private static HL7InterfaceConfig CreateHL7InterfaceConfig(string strServiceName, InterfaceType iType)
        {
            HL7InterfaceConfig config = new HL7InterfaceConfig();
            config.InterfaceName = strServiceName;
            config.InterfaceType = iType;
            config.InterfaceFolder = (iType == InterfaceType.Receiver ? "Receiver\\" : "Sender\\") + strServiceName;
            config.InstallDate = DateTime.Now;

            return config;
        }

        internal static bool UninstallInterface(HL7InterfaceConfig config)
        {
            string interfacePath = Path.Combine(Application.StartupPath, config.InterfaceFolder);
            string strSvcRelativePath = "Bin\\Services\\" + (config.InterfaceType == InterfaceType.Receiver ? "HL7GW_RCV" : "HL7GW_SND");
            string interfaceServicePath = Path.Combine(interfacePath, strSvcRelativePath);

            try
            {
                Program.ConfigMgt.Config.InterfaceList.Remove(config);
                if (Program.SaveConfig())
                {
                    string uninstallSvcBat = Path.Combine(interfaceServicePath, "UninstallService.bat");
                    ScriptMgt.ExecuteAssembly(uninstallSvcBat, "", true, Program.Log);

                    UpdateUnistallBat(Program.ConfigMgt.Config);

                    DeleteDirectory(interfacePath, true);
                    return true;
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Error, "Unistall interface failed : " + config.InterfaceName);
                Program.Log.Write(err);
            }

            return false;
        }

        internal static void DeleteDirectory(string strPath, bool revise)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(strPath);
            if (revise)
            {
                DirectoryInfo[] dirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    DeleteDirectory(dir.FullName, revise);
                }
            }

            FileInfo[] files = dirInfo.GetFiles();
            foreach (FileInfo file in files)
            {
                file.Attributes = FileAttributes.Normal;
                file.Delete();
            }

            dirInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            dirInfo.Delete();
        }

        internal static void UpdateUnistallBat(ManagerConfig config)
        {
            string startFName = Path.Combine(Application.StartupPath, "Uninstall.bat");
            using (StreamWriter sw = File.CreateText(startFName))
            {
                foreach (HL7InterfaceConfig iConfig in config.InterfaceList)
                {
                    if (!string.IsNullOrEmpty(iConfig.InterfaceName))
                    {
                        sw.WriteLine("net stop " + iConfig.InterfaceName);
                        sw.WriteLine("sc delete " + iConfig.InterfaceName);
                    }
                }
            }
        }
    }
}
