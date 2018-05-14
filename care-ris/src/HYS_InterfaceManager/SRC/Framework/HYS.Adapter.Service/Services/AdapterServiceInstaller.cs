using System;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Configuration.Install;
using System.Windows.Forms;
using System.ServiceProcess;
using System.ComponentModel;
using System.Management;
using Microsoft.Win32;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.License2;

namespace HYS.Adapter.Service.Services
{
    [RunInstallerAttribute(true)]
    public class AdapterServiceInstaller : Installer
    {
        private string serviceName;

        private ServiceInstaller serviceInstaller1;
        //private ServiceInstaller serviceInstaller2;
        private ServiceProcessInstaller processInstaller;

        public AdapterServiceInstaller()
        {
            // Instantiate installers for process and services.
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller1 = new ServiceInstaller();
            //serviceInstaller2 = new ServiceInstaller();

            // The services run under the system account.
            processInstaller.Account = ServiceAccount.LocalSystem;

            // The services are started manually.
            //serviceInstaller1.StartType = ServiceStartMode.Automatic;
            serviceInstaller1.StartType = ServiceStartMode.Manual;
            //serviceInstaller2.StartType = ServiceStartMode.Manual;

            //serviceInstaller1.ServicesDependedOn = new string[] { GWLicenseAgent.NetbiosServiceName }; //TCP/IP NetBIOS Helper: Enables support for NetBIOS over TCP/IP (NetBT) service and NetBIOS name resolution.

            //Properties.Settings.Default.Reload();
            //Console.Write(">>>>>>>>>>>>>>>" + Program.ServiceName );//Properties.Settings.Default.ServiceName);

            serviceName = GetServiceName2();
            // ServiceName must equal those on ServiceBase derived classes.            
            serviceInstaller1.ServiceName = serviceName;  // Properties.Settings.Default.ServiceName;    // "HelloWorldService";
            serviceInstaller1.DisplayName = serviceName;
            //serviceInstaller2.ServiceName = "Hello-World Service 2";


            // Add installers to collection. Order is not important.
            Installers.Add(serviceInstaller1);
            //Installers.Add(serviceInstaller2);
            Installers.Add(processInstaller);
        }

        public string GetServiceName()
        {
            try
            {
                ConfigXmlDocument doc = new ConfigXmlDocument();
                doc.Load(Application.StartupPath + "\\HYS.Adapter.Service.exe.config");

                System.Xml.XmlNodeList nlist = doc.SelectNodes("/configuration/userSettings/HYS.Adapter.Service.Properties.Settings/setting");
                if (nlist == null || nlist.Count < 1) return "[NULL]";

                foreach (System.Xml.XmlNode n in nlist)
                {
                    System.Xml.XmlNode ann = n.SelectSingleNode("@name");
                    string strKey = ann.InnerText;

                    switch (strKey)
                    {
                        case "ServiceName":
                            XmlNode _ServiceNameNode = n.SelectSingleNode("value");
                            if (_ServiceNameNode != null)
                            {
                                return _ServiceNameNode.InnerText;
                            }
                            else
                            {
                                return "[NULL]";
                            }
                    }
                }

                return "[NULL]";
            }
            catch (Exception err)
            {
                Console.Write(err);
                return "[NULL]";
            }
        }
        public string GetServiceName2()
        {
            AdapterServiceCfgMgt configMgt = new AdapterServiceCfgMgt();
            configMgt.FileName = Application.StartupPath + "\\" + configMgt.FileName;

            if (configMgt.Load())
            {
                Console.WriteLine("Load config file succeeded. " + configMgt.FileName);
                string serviceName = configMgt.Config.ServiceName;
                Console.WriteLine("Service name: " + serviceName);
                return serviceName;
            }
            else
            {
                Console.WriteLine("Load config file failed. " + configMgt.FileName);
                Console.WriteLine(configMgt.LastErrorInfor);
                return "[NULL]";
            }
        }

        public void SetServicePermission(string serviceName)
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

            SetServicePermission(serviceName);
        }
    }
}
