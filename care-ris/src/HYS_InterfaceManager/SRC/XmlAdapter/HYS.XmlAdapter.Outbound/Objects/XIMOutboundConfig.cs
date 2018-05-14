using System;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.XmlAdapter.Common.Net;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Outbound.Controlers;

namespace HYS.XmlAdapter.Outbound.Objects
{
    public class XIMOutboundConfig : XObject
    {
        public XIMOutboundConfig()
        {
            _socketConfig.ReceiveEndSign = XIMClientHelper.ResponseEndSign;
            _socketConfig.SendEndSign = XIMClientHelper.RequestEndSign;
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

        private int _dataCheckingInterval = 30 * 1000;     //ms
        public int DataCheckingInterval
        {
            get { return _dataCheckingInterval; }
            set { _dataCheckingInterval = value; }
        }

        private bool _outboundToFile = false;
        public bool OutboundToFile
        {
            get { return _outboundToFile; }
            set { _outboundToFile = value; }
        }

        private string _targetPath = "OutputFiles";
        public string TargetPath
        {
            get { return _targetPath; }
            set { _targetPath = value; }
        }

        private string _fileNameSuffix = ".xml";
        public string FileNameSuffix
        {
            get { return _fileNameSuffix; }
            set { _fileNameSuffix = value; }
        }

        private string _fileNamePrefix = "xim_";
        public string FileNamePrefix
        {
            get { return _fileNamePrefix; }
            set { _fileNamePrefix = value; }
        }

        private bool _includeRequestHeader = true;
        public bool IncludeRequestHeader
        {
            get { return _includeRequestHeader; }
            set { _includeRequestHeader = value; }
        }

        private bool _enableDataMerging = true;
        public bool EnableDataMerging
        {
            get { return _enableDataMerging; }
            set { _enableDataMerging = value; }
        }

        private int _dataMergingPKIndex = 1;
        public int DataMergingPKIndex
        {
            get { return _dataMergingPKIndex; }
            set { _dataMergingPKIndex = value; }
        }

        private SocketConfig _socketConfig = new SocketConfig();
        public SocketConfig SocketConfig
        {
            get { return _socketConfig; }
            set { _socketConfig = value; }
        }

        private XCollection<XIMOutboundMessage> _messages = new XCollection<XIMOutboundMessage>();
        public XCollection<XIMOutboundMessage> Messages
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
