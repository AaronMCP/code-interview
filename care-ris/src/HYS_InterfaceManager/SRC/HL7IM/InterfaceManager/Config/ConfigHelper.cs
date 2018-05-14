using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using HYS.IM.Messaging.Management.Config;
using System.IO;
using System.Windows.Forms;
using HYS.IM.Messaging.Base.Config;
using CSH.eHeath.HL7Gateway.Manager;
using HYS.IM.Common.Logging;

namespace HYS.HL7IM.Manager.Config
{
    internal static class HL7ConfigHelper
    {
        public static HL7InterfaceConfig LoadConfigInfo(HL7InterfaceConfig config)
        {
            config.InterfaceName = GetInterfaceName(config);
            config.InterfaceStatus = GetServiceStatus(config.InterfaceName);

            return config;
        }

        public static string GetInterfaceName(HL7InterfaceConfig config)
        {
            if (string.IsNullOrEmpty(config.InterfaceFolder))
            {
                return null;
            }

            string strType = config.InterfaceType == InterfaceType.Receiver ? "HL7GW_RCV" : "HL7GW_SND";
            string fileName = Path.Combine(Application.StartupPath, config.InterfaceFolder + "\\Bin\\Services\\" + strType + "\\NTServiceHost.xml");
            ConfigManager<NTServiceHostConfig> ConfigMgt = new ConfigManager<NTServiceHostConfig>(fileName);
            if (ConfigMgt.Load())
            {
                return ConfigMgt.Config.ServiceName;
            }
            else
            {
                Program.Log.Write(LogType.Error, "Load NT Service Host Config failed :" + fileName);
            }

            return null;

        }

        public static void UpdateInterfaceName(HL7InterfaceConfig config)
        {
            if (string.IsNullOrEmpty(config.InterfaceFolder))
            {
                return;
            }

            string strType = config.InterfaceType == InterfaceType.Receiver ? "HL7GW_RCV" : "HL7GW_SND";
            string fileName = Path.Combine(Application.StartupPath, config.InterfaceFolder + "\\Bin\\Services\\" + strType + "\\NTServiceHost.xml");
            ConfigManager<NTServiceHostConfig> ConfigMgt = new ConfigManager<NTServiceHostConfig>(fileName);
            if (ConfigMgt.Load())
            {
                ConfigMgt.Config.ServiceName = config.InterfaceName;
                if (ConfigMgt.Save())
                {
                    Program.Log.Write("Update NT Service Host Config success :" + config.InterfaceName);
                }
                else
                {
                    Program.Log.Write(LogType.Error, "Update NT Service Host Config failed :" + fileName);
                }
            }
            else
            {
                Program.Log.Write(LogType.Error, "Load NT Service Host Config failed :" + fileName);
            }
        }

        public static InterfaceStatus GetServiceStatus(string interfaceName)
        {
            if (string.IsNullOrEmpty(interfaceName))
            {
                return InterfaceStatus.Unkown;
            }

            ServiceController[] scList = ServiceController.GetServices();
            if (scList != null)
            {
                foreach (ServiceController sc in scList)
                {
                    if (sc.ServiceName == interfaceName)
                    {
                        return GetServiceStatus(sc);
                    }
                }
            }

            return InterfaceStatus.Unkown;

        }

        private static InterfaceStatus GetServiceStatus(ServiceController sc)
        {
            switch (sc.Status)
            {
                case ServiceControllerStatus.ContinuePending:
                    return InterfaceStatus.Stopped;
                case ServiceControllerStatus.PausePending:
                    return InterfaceStatus.Stopped;
                case ServiceControllerStatus.Paused:
                    return InterfaceStatus.Stopped;
                case ServiceControllerStatus.Running:
                    return InterfaceStatus.Running;
                case ServiceControllerStatus.StartPending:
                    return InterfaceStatus.Stopped;
                case ServiceControllerStatus.StopPending:
                    return InterfaceStatus.Stopped;
                case ServiceControllerStatus.Stopped:
                    return InterfaceStatus.Stopped;
                default:
                    return InterfaceStatus.Unkown;
            }
        }
    }
}
