using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.XmlAdapter.Common;
using HYS.XmlAdapter.Common.Net;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Inbound.Objects;
using HYS.XmlAdapter.Inbound.Controlers;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Inbound.Adapters
{
    [AdapterServiceEntry("XIM(HL7) Inbound Adapter Service", DirectionType.INBOUND, "XIM(HL7) inbound adapter for GC Gateway 2.0")]
    public class ServiceMain : IInboundAdapterService
    {
        private XIMServer _server;

        #region IInboundAdapterService Members

        public event DataReceiveEventHandler OnDataReceived;
        internal bool SaveData(IInboundRule rule, DataSet data)
        {
            if (OnDataReceived == null) return false;
            return OnDataReceived(rule, data);
        }

        #endregion

        #region IAdapterService Members

        public bool Start(string[] arguments)
        {
            _server = XIMServer.CreateXIMServer(this);
            if (_server == null) return false;
            return _server.Start();
        }

        public AdapterStatus Status
        {
            get
            {
                if (_server == null) return AdapterStatus.Unknown;
                if (_server.IsListening) return AdapterStatus.Running;
                return AdapterStatus.Stopped;
            }
        }

        public bool Stop(string[] arguments)
        {
            if (_server == null) return false;
            return _server.Stop();
        }

        #endregion

        #region IAdapterBase Members

        public bool Exit(string[] arguments)
        {
            Program.BeforeExit();
            return true;
        }

        public bool Initialize(string[] arguments)
        {
            Program.PreLoading();
            Program.Log.Write(Program.AppName + " is running in Adapter.Service host.");

            if (arguments != null && arguments.Length > 0)
            {
                string str = arguments[0];
                //Program.Log.Write("GWDataDB connection: " + str); //contains db pw
                Program.ConfigMgt.Config.GWDataDBConnection = str;
            }

            XIMMappingHelper.ClearMapping(Program.ConfigMgt.Config.Messages);

            return true;
        }

        public Exception LastError
        {
            get { return null; }
        }

        #endregion
    }
}
