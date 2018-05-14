using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Device;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;

namespace HYS.SocketAdapter.SocketInboundAdapter
{
    [AdapterServiceEntry("Socket_IN_Adapter", DirectionType.INBOUND, "Socket Inbound Adapter for GC Gateway")]
    public class AdapterMain : IInboundAdapterService 
    {
        private DataAccessControler controler;

        #region IInBoundAdapter Members

        public event DataReceiveEventHandler OnDataReceived;

        private bool controler_OnDataReceived(IInboundRule rule, DataSet data)
        {
            if (OnDataReceived != null)
                return OnDataReceived(rule, data);
            else
                return false;
        }

        #endregion

        #region IAdapter Members

        public bool Initialize(string[] arguments)
        {
            try
            {

                Program.Preloading();
                
                Program.Log.Write("After preloading");

                controler = new DataAccessControler();
                Program.Log.Write("After DataAccessControler");

                controler.OnDataReceived += new DataReceiveEventHandler(controler_OnDataReceived);

                
            }
            catch (Exception ex)
            {
                Program.Log.Write(ex);
            }

            return true;
        }

        public bool Exit(string[] arguments)
        {
            if (controler != null)
            {
                if (controler.IsRunning) controler.Stop();
                controler.OnDataReceived -= new DataReceiveEventHandler(controler_OnDataReceived);
            }

            Program.BeforeExit();
            return true;
        }

        public bool Start(string[] arguments)
        {
            if (controler == null) return false;
            return controler.Start();
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

        //public AdapterOption Option
        //{
        //    get
        //    {
        //        return new AdapterOption(DirectionType.Inbound);
        //    }
        //}

        public IConfigUI[] GetConfigUI()
        {
            //return new IConfigUI[] { new FormConfigAgent() };
            return null;//TODO: Implemented IConfigUI[]
        }

        //public DeviceDir GetInfor()
        //{
        //    if (directory == null)
        //    {
        //        DeviceDirManager dirMgt = new DeviceDirManager();
        //        dirMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
        //        if (dirMgt.LoadDeviceDir()) directory = dirMgt.DeviceDirInfor;
        //    }

        //    return directory;
        //}

        public Exception LastError
        {
            get { return null; }
        }

        #endregion
    }
}
