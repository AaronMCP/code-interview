using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Diagnostics;
using System.Net;
using System.ServiceProcess;
using System.Data;
using System.Runtime.InteropServices;
using System.Net.NetworkInformation;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Management.Scripts;

namespace HYS.IM.IPMonitor.Policies.Validations
{
    public class IsRunning : IVerifitable 
    {
        public bool Validation(string ServiceName)
        {
            try
            {
                ServiceController sc = new ServiceController(ServiceName);

                if (sc != null)
                {
                    return sc.Status == ServiceControllerStatus.Running;
                }
                else
                {
                    Program.Log.Write(LogType.Error,string.Format("the service ({0}) does not exist!",ServiceName));
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, ex.Message + ex.StackTrace);           
            }
            return false;
        }
    }
}
