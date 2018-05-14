using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.XmlAdapter.Common;
using HYS.XmlAdapter.Common.Net;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Outbound.Objects;
using HYS.XmlAdapter.Outbound.Controlers;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Outbound.Adapters
{
    [AdapterServiceEntry("XIM(HL7) Outbound Adapter Service", DirectionType.OUTBOUND, "XIM(HL7) outbound adapter for GC Gateway 2.0")]
    public class ServiceMain : IOutboundAdapterService
    {
        private XIMClient _client;

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

        #region IAdapterService Members

        public bool Start(string[] arguments)
        {
            _client = new XIMClient(this);
            return _client.Start();
        }

        public AdapterStatus Status
        {
            get
            {
                if (_client == null) return AdapterStatus.Unknown;
                if (_client.IsRunning) return AdapterStatus.Running;
                return AdapterStatus.Stopped;
            }
        }

        public bool Stop(string[] arguments)
        {
            if (_client == null) return false;
            return _client.Stop();
        }

        #endregion

        #region IOutboundAdapterService Members

        public event DataDischargeEventHanlder OnDataDischarge;
        internal bool DischargeData(string[] guidList)
        {
            if (OnDataDischarge == null) return false;
            return OnDataDischarge(guidList);
        }

        public event DataRequestEventHanlder OnDataRequest;
        internal DataSet RequestData(IOutboundRule rule, DataSet criteria)
        {
            if (OnDataRequest == null) return null;
            return OnDataRequest(rule, criteria);
        }

        #endregion
    }
}
