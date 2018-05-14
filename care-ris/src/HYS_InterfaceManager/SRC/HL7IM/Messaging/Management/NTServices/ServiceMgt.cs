using System;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Management.Config;

namespace HYS.IM.Messaging.Management.NTServices
{
    public class ServiceMgt
    {
        private static object GetStatus(string serviceName, ILog log)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                return sc.Status;
            }
            catch (Exception err)
            {
                if (log != null) log.Write(err);
                return null;
            }
        }
        public static ServiceStatus GetServiceStatus(string serviceName, ILog log)
        {
            object o = GetStatus(serviceName, log);
            if (o == null) return ServiceStatus.Unknown;

            ServiceControllerStatus s = (ServiceControllerStatus)o;

            switch (s)
            {
                case ServiceControllerStatus.Running: return ServiceStatus.Running;
                case ServiceControllerStatus.Stopped: return ServiceStatus.Stopped;
                default: return ServiceStatus.Other;
            }
        }

        public static int TimeOut = 20000;
        public static bool SetServiceStatus(string serviceName, ServiceStatus status, ILog log)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);

                switch (status)
                {
                    case ServiceStatus.Running:
                        sc.Start(new string[] { ServiceArgument.InISM });
                        sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 0, 0, TimeOut));
                        break;
                    case ServiceStatus.Stopped:
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 0, 0, TimeOut));
                        break;
                }

                return true;
            }
            catch (Exception err)
            {
                if (log != null) log.Write(err);
                return false;
            }
        }

        public const int Manual = 3;
        public const int Automatic = 2;
        public static bool SetServiceStartStyle(string serviceName, int code, ILog log)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey
                    ("SYSTEM\\CurrentControlSet\\Services\\" + serviceName, true))
                {
                    key.SetValue("Start", code, RegistryValueKind.DWord);
                }
                return true;
            }
            catch (Exception err)
            {
                if (log != null) log.Write(err);
                return false;
            }
        }

        public static bool SetServiceStatusAndStartStyleRemote(string mgtSvcConfigFileName, string serviceName, ServiceStatus status, ILog log)
        {
            if (serviceName == null || serviceName.Length < 1) return false;

            string str = "";

            ConfigManager<ServiceConfig> mgt = new ConfigManager<ServiceConfig>(mgtSvcConfigFileName);
            if (!mgt.Load())
            {
                str = "Load file failed: " + mgt.FileName;
                if (log != null)
                {
                    log.Write(LogType.Error, str);
                    log.Write(mgt.LastError);
                }
                return false;
            }

            try
            {
                bool hasChannel = false;
                foreach (IChannel chn in ChannelServices.RegisteredChannels)
                {
                    if (chn is HttpChannel)
                    {
                        hasChannel = true;
                        break;
                    }
                }

                if (!hasChannel)
                {
                    HttpChannel chn = new HttpChannel();
                    ChannelServices.RegisterChannel(chn, false);
                }

                IManagementService helper = Activator.GetObject(typeof(IManagementService), mgt.Config.RemotingUrl) as IManagementService;
                if (helper == null)
                {
                    str = "Create remote object failed: " + mgt.Config.RemotingUrl;
                    if (log != null) log.Write(LogType.Error, str);
                    return false;
                }

                return helper.SetServiceStatusAndStartStyle(serviceName, status);
            }
            catch (Exception err)
            {
                if (log != null)
                {
                    str = "Call remting service failed: " + mgt.Config.RemotingUrl;
                    log.Write(LogType.Error, str);
                    log.Write(err);
                }
                return false;
            }
        }
    }
}
