using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterObjects
{
    public class SQLInAdapterConfig : XObject
    {
        private ConnectionConfig _thirdPartyInteractConfig = new ConnectionConfig();
        public ConnectionConfig ThirdPartyInteractConfig
        {
            get { return _thirdPartyInteractConfig; }
            set { _thirdPartyInteractConfig = value; }
        }

        private XCollection<SQLInboundChanel> _inboundChanels = new XCollection<SQLInboundChanel>();
        public XCollection<SQLInboundChanel> InboundChanels
        {
            get { return _inboundChanels; }
            set { _inboundChanels = value; }
        }

        private XCollection<SQLInboundChanel> _inboundPassiveChanels = new XCollection<SQLInboundChanel>();
        public XCollection<SQLInboundChanel> InboundPassiveChanels
        {
            get { return _inboundPassiveChanels; }
            set { _inboundPassiveChanels = value; }
        }

        private XCollection<ThrPartyDBParamter> _thrPartyDBParamter = new XCollection<ThrPartyDBParamter>();
        public XCollection<ThrPartyDBParamter> ThrPartyDBParamters
        {
            get { return _thrPartyDBParamter; }
            set { _thrPartyDBParamter = value; }
        }

        private ThrPartyAppConfig _thrPartyAppConfig = new ThrPartyAppConfig();
        public ThrPartyAppConfig ThrPartyAppConfig
        {
            get { return _thrPartyAppConfig; }
            set { _thrPartyAppConfig = value; }
        }
    }
}
