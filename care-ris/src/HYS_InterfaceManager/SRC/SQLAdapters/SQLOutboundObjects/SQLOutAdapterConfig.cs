using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLOutboundAdapterObjects
{
    public class SQLOutAdapterConfig : XObject
    {
        private ConnectionConfig _thirdPartyInteractConfig = new ConnectionConfig();
        public ConnectionConfig ThirdPartyInteractConfig
        {
            get { return _thirdPartyInteractConfig; }
            set { _thirdPartyInteractConfig = value; }
        }

        private XCollection<SQLOutboundChanel> _outboundChanels = new XCollection<SQLOutboundChanel>();
        public XCollection<SQLOutboundChanel> OutboundChanels
        {
            get { return _outboundChanels; }
            set { _outboundChanels = value; }
        }

        private XCollection<SQLOutboundChanel> _outboundPassiveChanels = new XCollection<SQLOutboundChanel>();
        public XCollection<SQLOutboundChanel> OutboundPassiveChanels
        {
            get { return _outboundPassiveChanels; }
            set { _outboundPassiveChanels = value; }
        }

        private XCollection<ThrPartyDBParamter> _thrPartyDBParamter = new XCollection<ThrPartyDBParamter>();
        public XCollection<ThrPartyDBParamter> ThrPartyDBParamters
        {
            get { return _thrPartyDBParamter; }
            set { _thrPartyDBParamter = value; }
        }

        //private bool _replaceUnixLineEndingWithWindowsLineEnding = true;
        //public bool ReplaceUnixLineEndingWithWindowsLineEnding
        //{
        //    get { return _replaceUnixLineEndingWithWindowsLineEnding; }
        //    set { _replaceUnixLineEndingWithWindowsLineEnding = value; }
        //}
    }
}
