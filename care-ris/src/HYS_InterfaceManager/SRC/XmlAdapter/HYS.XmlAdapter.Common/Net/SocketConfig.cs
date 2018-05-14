using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Common.Net
{
    public class SocketConfig : XObject
    {
        private string _ipAddress = "";
        public string IPAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        private int _port = 2345;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private int _sendTimeout = 1000 * 60;  //ms
        public int SendTimeout
        {
            get { return _sendTimeout; }
            set { _sendTimeout = value; }
        }

        private int _receiveTimeout = 1000 * 60;  //ms
        public int ReceiveTimeout
        {
            get { return _receiveTimeout; }
            set { _receiveTimeout = value; }
        }

        private int _connectionTimeoutSecond = 120; //s
        public int ConnectionTimeoutSecond
        {
            get { return _connectionTimeoutSecond; }
            set { _connectionTimeoutSecond = value; }
        }

        private int _connectionCollectionInterval = 1000 * 60;  //ms
        public int ConnectionCollectionInterval
        {
            get { return _connectionCollectionInterval; }
            set { _connectionCollectionInterval = value; }
        }

        private bool _enableConnectionCollecting = false;
        public bool EnableConnectionCollecting
        {
            get { return _enableConnectionCollecting; }
            set { _enableConnectionCollecting = value; }
        }

        private bool _includeHeader = true;
        public bool IncludeHeader
        {
            get { return _includeHeader; }
            set { _includeHeader = value; }
        }

        private int _backLog = 100; //The maximum length of the pending connections queue. 
        public int BackLog
        {
            get { return _backLog; }
            set { _backLog = value; }
        }

        private string _codePageName = "utf-8";
        public string CodePageName
        {
            get { return _codePageName; }
            set { _codePageName = value; }
        }

        private string _receiveEndSign = "\0";  //"\r\n";   //'\0' is invalid XML charactor
        [XCData(true)]
        public string ReceiveEndSign
        {
            get { return _receiveEndSign; }
            set { _receiveEndSign = value; }
        }

        private string _sendEndSign = "\0";  //"\r\n";   //'\0' is invalid XML charactor
        [XCData(true)]
        public string SendEndSign
        {
            get { return _sendEndSign; }
            set { _sendEndSign = value; }
        }

        private string _sourceDeviceName = "XISClient";
        [XCData(true)]
        public string SourceDeviceName
        {
            get { return _sourceDeviceName; }
            set { _sourceDeviceName = value; }
        }

        private string _targetDeviceName = "HIS-RIS-Device";
        [XCData(true)]
        public string TargetDeviceName
        {
            get { return _targetDeviceName; }
            set { _targetDeviceName = value; }
        }
    }
}
