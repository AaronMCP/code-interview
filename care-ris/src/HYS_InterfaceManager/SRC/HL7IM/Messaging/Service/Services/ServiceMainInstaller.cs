using System;
using System.Management;
using System.Collections;
using System.ComponentModel;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Configuration.Install;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Service.Services
{
    [RunInstaller(true)]
    public partial class ServiceMainInstaller : Installer
    {
        private bool interactWithDesktop;
        private string serviceName;

        public ServiceMainInstaller()
        {
            InitializeComponent();

            NTServiceHostConfig cfg;
            GetServiceName(out cfg);

            serviceName = cfg.ServiceName;
            interactWithDesktop = cfg.InteractWithDesktop;

            // Instantiate installers for process and services.
            ServiceProcessInstaller processInstaller = new ServiceProcessInstaller();
            processInstaller.Account = cfg.AccountType;

            if (cfg.AccountType == ServiceAccount.User)
            {
                processInstaller.Username = cfg.UserName;
                processInstaller.Password = cfg.Password;
            }

            ServiceInstaller serviceInstaller = new ServiceInstaller();
            serviceInstaller.StartType = cfg.StartType;
            serviceInstaller.ServiceName = cfg.ServiceName;
            serviceInstaller.DisplayName = NTServiceHostConfig.NTServiceDisplayNamePrefix + cfg.ServiceName;
            serviceInstaller.Description = cfg.Description;

            string strDepends = cfg.DependOnServiceNameList;
            if (strDepends != null)
            {
                string[] dlist = strDepends.Split(NTServiceHostConfig.DependOnServiceNameListSpliter);
                List<string> dl = new List<string>();
                foreach (string d in dlist)
                {
                    string str = d.Trim();
                    if (str.Length < 1) continue;
                    dl.Add(str);
                }
                if (dl.Count > 0) serviceInstaller.ServicesDependedOn = dl.ToArray();
            }

            // Add installers to collection. Order is not important.
            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }

        public static void GetServiceName(out NTServiceHostConfig cfg)
        {
            ConfigManager<NTServiceHostConfig> configMgt = new ConfigManager<NTServiceHostConfig>(Program.AppConfigFileName);

            if (configMgt.Load() && configMgt.Config != null)
            {
                Console.WriteLine("Load config file succeeded. " + configMgt.FileName);

                cfg = configMgt.Config;
                Console.WriteLine("Service name: " + cfg.ServiceName);
            }
            else
            {
                Console.WriteLine("Load config file failed. " + configMgt.FileName);
                Console.WriteLine(configMgt.LastErrorInfor);

                cfg = new NTServiceHostConfig();
                cfg.ServiceName = "[NULL]";
                cfg.Description = "[Cannot read configuration file of XDS Gateway Application Host]";
            }
        }

        public static void SetServicePermission(string serviceName)
        {
            Console.WriteLine("--- Set Service Permission ---");

            try
            {
                ConnectionOptions coOptions = new ConnectionOptions();
                coOptions.Impersonation = ImpersonationLevel.Impersonate;

                ManagementScope mgmtScope = new System.Management.ManagementScope(@"root\CIMV2", coOptions);
                mgmtScope.Connect();

                ManagementObject wmiService = new ManagementObject("Win32_Service.Name='" + serviceName + "'");
                ManagementBaseObject InParam = wmiService.GetMethodParameters("Change");
                InParam["DesktopInteract"] = true;

                ManagementBaseObject OutParam = wmiService.InvokeMethod("Change", InParam, null);

                Console.WriteLine("Set DesktopInteract Permission succeeded");
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }

            Console.WriteLine("------------------------------");
        }

        protected override void OnCommitted(IDictionary savedState)
        {
            base.OnCommitted(savedState);

            if (interactWithDesktop) SetServicePermission(serviceName);
        }
    }
}