using System;
using System.Collections;
using System.ServiceProcess;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Config;
using Microsoft.Win32;

namespace HYS.IM.BusinessControl.SystemControl
{
    public class ServiceControl
    {
        private static object GetStatus(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                return sc.Status;
            }
            catch (Exception err)
            {
                GCError.SetLastError("Get service status failed. service name: " + serviceName);
                GCError.SetLastError(err);
                return null;
            }
        }

        public static AdapterStatus GetServiceStatus(string serviceName)
        {
            object o = GetStatus(serviceName);
            if (o == null) return AdapterStatus.Unknown;

            ServiceControllerStatus s = (ServiceControllerStatus)o;

            switch (s)
            {
                case ServiceControllerStatus.Running: return AdapterStatus.Running;
                case ServiceControllerStatus.Stopped: return AdapterStatus.Stopped;
                default: return AdapterStatus.Other;
            }
        }

        public static Hashtable GetServiceStatus(string[] serviceList)
        {
            if (serviceList == null) return null;

            Hashtable table = new Hashtable();
            foreach (string s in serviceList)
            {
                table.Add(s, GetServiceStatus(s));
            }

            return table;
        }

        public static bool SetServiceStatus(string serviceName, AdapterStatus status)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);

                switch (status)
                {
                    case AdapterStatus.Running :
                        sc.Start(new string[] { AdapterConfigArgument.InIM });
                        sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0,0,0,0,TimeOut));
                        break;
                    case AdapterStatus.Stopped :
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0,0,0,0,TimeOut));
                        break;
                }

                return true;
            }
            catch (Exception err)
            {
                GCError.SetLastError("Set service status failed. service name: " + serviceName + " status: " + status.ToString());
                GCError.SetLastError(err);
                return false;
            }
        }

        public static int TimeOut = 20000;

        public static bool SetServiceStartStyle(string serviceName, int code)
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
                GCError.SetLastError("Set service start style failed. service name: " + serviceName);
                GCError.SetLastError(err);
                return false;
            }
        }
        public const int Automatic = 2;
        public const int Manual = 3;
    }
}
