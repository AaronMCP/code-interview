using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using HYS.Adapter.Base;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.License2;
using HYS.Adapter.Service.Controlers;

namespace HYS.Adapter.Service.Services
{
    public partial class AdapterService : ServiceBase
    {
        public AdapterService()
        {
            InitializeComponent();
            LoadAdapter();

            messager = new MessageControler();
            garbagecollector = new GarbageControler();
            licensecontroler = new LicenseControler(this);

            ServiceName = Program.ConfigMgt.Config.ServiceName;
            CanShutdown = true;
            CanStop = true;
        }

        protected IAdapterService adapter;
        protected MessageControler messager;
        protected LicenseControler licensecontroler;
        protected GarbageControler garbagecollector;
        protected AdapterServiceEntryAttribute adapterAttribute;

        protected void LoadAdapter()
        {
            try
            {
                switch (Program.ConfigMgt.Config.AdapterDirection)
                {
                    case DirectionType.INBOUND:
                        if (Program.InAdapter != null)
                        {
                            adapter = Program.InAdapter.Instance;
                            adapterAttribute = Program.InAdapter.Attribute;
                        }
                        break;
                    case DirectionType.OUTBOUND:
                        if (Program.OutAdapter != null)
                        {
                            adapter = Program.OutAdapter.Instance;
                            adapterAttribute = Program.OutAdapter.Attribute;
                        }
                        break;
                    case DirectionType.BIDIRECTIONAL:
                        if (Program.BiAdapter != null)
                        {
                            adapter = Program.BiAdapter.Instance;
                            adapterAttribute = Program.BiAdapter.Attribute;
                        }
                        break;
                }

                if (adapter == null)
                {
                    Program.Log.Write(LogType.Error, "Cannot create adapter instance.");
                    Program.Log.Write(AssemblyHelper.LastError);
                    return;
                }

                Program.Log.Write("\r\nAdapter Loaded", false);
                Program.Log.Write("---------------------", false);
                if (adapterAttribute != null)
                {
                    Program.Log.Write("Name:" + adapterAttribute.Name);
                    Program.Log.Write("Direction:" + adapterAttribute.Direction.ToString());
                    Program.Log.Write("Description:" + adapterAttribute.Description);
                }
                Program.Log.Write("---------------------\r\n", false);

                if (adapter.Initialize(new string[] {
                    Program.ConfigMgt.Config.DataDBConnection,
                    Program.ConfigMgt.Config.ConfigDBConnection }))
                {
                    Program.Log.Write(LogType.Debug, "Adapter initialization succeeded.");
                }
                else
                {
                    Program.Log.Write(LogType.Warning, "Adapter initialization failed.");
                    Program.Log.Write(adapter.LastError);
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Error, "Error in loading adapter.");
                Program.Log.Write(err);
            }
        }
        protected bool CheckAdapter()
        {
            if (adapter != null) return false;
            Program.Log.Write(LogType.Warning, "Adapter has been unloaded.");
            return true;
        }

        protected void StopAdapter()
        {
            if (CheckAdapter()) return;

            if (adapter.Stop(null))
            {
                Program.Log.Write(LogType.Debug, "Adapter stopping succeeded.");
            }
            else
            {
                Program.Log.Write(LogType.Warning, "Adapter stopping failed.");
                Program.Log.Write(adapter.LastError);
            }
        }
        protected void UnloadAdapter()
        {
            if (CheckAdapter()) return;

            if (adapter.Exit(null))
            {
                Program.Log.Write(LogType.Debug, "Adapter finalization succeeded.");
            }
            else
            {
                Program.Log.Write(LogType.Warning, "Adapter finalization failed.");
                Program.Log.Write(adapter.LastError);
            }
        }
        protected bool StartAdapter(DeviceLicense device)
        {
            if (device == null) return false;
            if (CheckAdapter()) return false;

            if (adapter.Start(new string[] { device.FeatureCode.ToString() }))
            {
                Program.Log.Write(LogType.Debug, "Adapter starting succeeded.\r\n");
                return true;
            }
            else
            {
                Program.Log.Write(LogType.Warning, "Adapter starting failed.\r\n");
                Program.Log.Write(adapter.LastError);
                this.Stop();

                return false;
            }
        }

        protected override void OnStart(string[] args)
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

            DeviceLicense device = licensecontroler.Start(args);
            if (device == null) return;

            if (!StartAdapter(device)) return;

            messager.NotifyServiceStart();
            garbagecollector.Start();
        }
        protected override void OnShutdown()
        {
            licensecontroler.Stop();
            UnloadAdapter();
            garbagecollector.Stop();
            messager.NotifyServiceStop();
        }
        protected override void OnStop()
        {
            licensecontroler.Stop();
            StopAdapter();
            garbagecollector.Stop();
            messager.NotifyServiceStop();
        }
    }
}
