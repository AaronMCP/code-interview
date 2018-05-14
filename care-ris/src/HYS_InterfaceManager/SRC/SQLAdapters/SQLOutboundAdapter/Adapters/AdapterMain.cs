using System;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using SQLOutboundAdapter.Forms;

namespace SQLOutboundAdapter.Adapters
{
    [AdapterServiceEntry("SQL_OUT_Adapter", DirectionType.OUTBOUND, "SQL Outbound Adapter for GC Gateway")]
    public class AdapterMain : IOutboundAdapterService
    {
        private DeviceDir directory;
        private DataAccessControler controler;


        #region IAdapter Members

        public bool Initialize(string[] arguments)
        {
            Program.Preloading();
            
            controler = new DataAccessControler();
            controler.OnDataRequest += new DataRequestEventHanlder(controler_OnDataRequest);
            controler.OnDataDischarge += new DataDischargeEventHanlder(controler_OnDataDischarge);
            return true;
        }

        public bool Exit(string[] arguments)
        {
            if (controler != null)
            {
                if (controler.IsRunning) controler.Stop();
                controler.OnDataRequest -= new DataRequestEventHanlder(controler_OnDataRequest);
                controler.OnDataDischarge -= new DataDischargeEventHanlder(controler_OnDataDischarge);
            }

            Program.BeforeExit();
            return true;
        }

        public event DataRequestEventHanlder OnDataRequest=null;
        public event DataDischargeEventHanlder OnDataDischarge=null;

        private DataSet controler_OnDataRequest(IOutboundRule rule, DataSet criteria)
        {
            if (OnDataRequest != null)
                return OnDataRequest(rule, criteria);
            else
                return null;
        }

        private bool controler_OnDataDischarge(string[] guidlist)
        {
            if (OnDataDischarge != null)
                return OnDataDischarge(guidlist);
            else
                return false;
        }

        public bool Start(string[] arguments)
        {
            if (controler == null) return false;
            controler.Start();
            return true;
        }

        
       
        public bool Stop(string[] arguments)
        {
            if (controler == null) return false;
            controler.Stop();
            return true;
        }

        public AdapterStatus Status
        {
            get
            {
                if (controler == null) return AdapterStatus.Unknown;
                if (controler.IsRunning) return AdapterStatus.Running;
                return AdapterStatus.Stopped;
            }
        }

        public AdapterOption Option
        {
            get
            {
                return new AdapterOption(DirectionType.OUTBOUND );
            }                                          
        }

        public IConfigUI[] GetConfigUI()
        {
            return new IConfigUI[] { new FormConfigAgent() };
        }

        public DeviceDir GetInfor()
        {
            if (directory == null)
            {
                DeviceDirManager dirMgt = new DeviceDirManager();
                dirMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
                if (dirMgt.LoadDeviceDir()) directory = dirMgt.DeviceDirInfor;
            }

            return directory;
        }

        public Exception LastError
        {
            get { return null; }
        }

        #endregion
    }
}
