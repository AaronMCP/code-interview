using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.XmlAdapter.Common.Net;
using HYS.XmlAdapter.Common.Files;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Inbound.Controlers;

namespace HYS.XmlAdapter.Inbound.Objects
{
    public class XIMInboundConfig : XObject
    {
        public XIMInboundConfig()
        {
            _socketConfig.ReceiveEndSign = XIMServerHelper.RequestEndSign;
            _socketConfig.SendEndSign = XIMServerHelper.ResponseEndSign;
        }

        private bool _dumpSocketData = true;
        public bool DumpSocketData
        {
            get { return _dumpSocketData; }
            set { _dumpSocketData = value; }
        }

        private string _gwDataDBConnection = "";
        public string GWDataDBConnection
        {
            get { return _gwDataDBConnection; }
            set { _gwDataDBConnection = value; }
        }

        private bool _inboundFromFile = false;
        public bool InboundFromFile
        {
            get { return _inboundFromFile; }
            set { _inboundFromFile = value; }
        }

        private DirectoryMonitorConfig _directoryConfig = new DirectoryMonitorConfig();
        public DirectoryMonitorConfig DirectoryConfig
        {
            get { return _directoryConfig; }
            set { _directoryConfig = value; }
        }

        private bool _includeResponseHeader = true;
        public bool IncludeResponseHeader
        {
            get { return _includeResponseHeader; }
            set { _includeResponseHeader = value; }
        }

        private SocketConfig _socketConfig = new SocketConfig();
        public SocketConfig SocketConfig
        {
            get { return _socketConfig; }
            set { _socketConfig = value; }
        }

        private XCollection<XIMInboundMessage> _messages = new XCollection<XIMInboundMessage>();
        public XCollection<XIMInboundMessage> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }

        private bool _warnBeforeDeleteChannel = true;
        public bool WarnBeforeDeleteChannel
        {
            get { return _warnBeforeDeleteChannel; }
            set { _warnBeforeDeleteChannel = value; }
        }
    }
}
