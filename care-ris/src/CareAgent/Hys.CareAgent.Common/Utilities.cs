using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;

namespace Hys.CareAgent.Common
{
    public static class Utilities
    {
        public const string RootPath = "c:\\Haosyisheng\\CareAgent\\Temp\\";
        public const string ConfPath = "/conf/ConfDemo.exe";
        public const string ConfProcessName = "ConfDemo";
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger("App");
        public  enum StatusCode
        {
            Success = 0,
            Failed = -1,
            AccessDenied = -2
        }
        public static string GenerateTempSaveFolder()
        {
            string savePath = RootPath + System.DateTime.Now.ToString("yyyy-MM-dd") + "\\";

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            return savePath;
        }

        public static void DeleteOutdatedFolder()
        {
            try
            {
                string checkFolder = System.DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(RootPath);

                DirectoryInfo[] subs = dir.GetDirectories();

                for (int i = subs.Length - 1; i >= 0; --i)
                {
                    try
                    {
                        if (string.Compare(subs[i].Name, checkFolder) < 0)
                            subs[i].Delete(true);
                    }
                    catch (System.Exception ex)
                    {
                        _logger.Error("DeleteOutdatedFolder error:" + ex.ToString());
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.Error("DeleteOutdatedFolder error:" + ex.ToString());
            }
        }

        public static string GetIPAddress()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"])
                    {
                        string[] addresses = (string[])mo["IPAddress"];
                        foreach (string ipaddress in addresses)
                        {
                            return ipaddress;
                        }
                    }

                }
            }
            catch { return "N/A"; }
            return "N/A";
        }

        public static string GetProcessID()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                using (System.Management.ManagementClass managementClass = new System.Management.ManagementClass("Win32_Processor"))
                {
                    using (System.Management.ManagementObjectCollection managementObject = managementClass.GetInstances())
                    {
                        foreach (System.Management.ManagementObject currentResult in managementObject)
                        {
                            sb.Append(currentResult["ProcessorID"].ToString());
                        }
                    }
                }
                string ip = GetIPAddress();
                string processID = sb.ToString() + ip;
                if (processID.Length > 255)
                {
                    return processID.Substring(0, 255);
                }
                else
                {
                    return processID;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetProcessID error:" + ex.ToString());
                return "";
            }
        }
    }
}
