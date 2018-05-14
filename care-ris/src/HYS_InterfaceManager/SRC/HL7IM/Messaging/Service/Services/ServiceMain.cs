using System;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.ServiceProcess;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Base.Controler;
using HYS.IM.Messaging.Service.Controlers;
using HYS.Common.Objects.License2;

namespace HYS.IM.Messaging.Service.Services
{
    public partial class ServiceMain : ServiceBase
    {
        private EntityContainer _container;
        private LicenseControler licensecontroler;

        public ServiceMain()
        {
            InitializeComponent();

            //Program.Log.Write("initialize service...");
            //Program.Log.Write(Program.ConfigMgt.Config.Entities.Count.ToString());

            _container = new EntityContainer(Program.ConfigMgt.Config, Program.Log);
            _container.Initialize(Program.ConfigMgt.Config.LogConfig, "NT Service Host");

            foreach (EntityAgent item in _container.EntityList)
            {
                item.EntityInstance.BaseServiceStop += new EventHandler(EntityInstance_BaseServiceStop);
            }

            licensecontroler = new LicenseControler(this);
            ServiceName = Program.ConfigMgt.Config.ServiceName;
            CanShutdown = true;
            CanStop = true;
        }

        protected void EntityInstance_BaseServiceStop(object sender, EventArgs e)
        {
            this.Stop();
        }

        protected override void OnStart(string[] args)
        {
            DumpArgument(args);

            if (Program.License.Config.Enabled)
            {
                DeviceLicense device = licensecontroler.Start();
                if (device == null) return;
            }

            _container.Start();

            NotifyServiceStatusChange(0);
        }

        private void NotifyServiceStatusChange(int status)
        {
            try
            {
                NotifyAdapterServerClient notifyClient = new NotifyAdapterServerClient("HL7Service");
                notifyClient.NotifyStatusChanged(ServiceName, status);
                Program.Log.Write("Notify service status change success.");
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Warning, "Notify service status change failed.");
                Program.Log.Write(LogType.Warning,ex.StackTrace);
            }
        }

        protected override void OnShutdown()
        {
            _container.Stop();
            if (Program.License.Config.Enabled)
            {
                licensecontroler.Stop();
            }
            _container.Uninitalize();
            NotifyServiceStatusChange(1);
        }

        protected override void OnStop()
        {
            _container.Stop();
            if (Program.License.Config.Enabled)
            {
                licensecontroler.Stop();
            }
            _container.Uninitalize();
            NotifyServiceStatusChange(1);
        }

        private void DumpArgument(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            if (args != null)
            {
                foreach (string a in args)
                {
                    sb.Append(a).Append(" ");
                }
            }
            Program.Log.Write(LogType.Debug, "Arguments: " + sb.ToString());
        }
    }
}
