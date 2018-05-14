using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketClientConfig : XObject, ICodePageIndicator
    {
        private string _socketClientType = SocketClient.DEVICE_NAME;
        public string SocketClientType
        {
            get { return _socketClientType; }
            set { _socketClientType = value; }
        }

        private string _ipAddress = "127.0.0.1";
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

        // --- do not support this feature currently ---
        //private int _connectionTimeoutSecond = 120; //s : the time to diconnect after the last data receved or data sent, be effective only in long term connection socket client, see the SocketClientWithLongConnection.
        //public int ConnectionTimeoutSecond
        //{
        //    get { return _connectionTimeoutSecond; }
        //    set { _connectionTimeoutSecond = value; }
        //}

        private string _codePageName = "utf-8";
        public string CodePageName
        {
            get { return _codePageName; }
            set { _codePageName = value; }
        }

        private int _codePageCode = -1;
        /// <summary>
        /// As CodePageName only cover a sub-set of code pages which CodePageCode covers, when CodePageCode >= 0, it has higher priority than CodePageName. When CodePageCode==0, it indicats the usage of default code page.
        /// </summary>
        public int CodePageCode
        {
            get { return _codePageCode; }
            set { _codePageCode = value; }
        }

        //private int _receiveRequestBufferSizeKB = 8;
        //public int ReceiveRequestBufferSizeKB
        //{
        //    get { return _receiveRequestBufferSizeKB; }
        //    set { _receiveRequestBufferSizeKB = value; }
        //}

        private int _receiveResponseBufferSizeKB = 1;
        public int ReceiveResponseBufferSizeKB
        {
            get { return _receiveResponseBufferSizeKB; }
            set { _receiveResponseBufferSizeKB = value; }
        }
    }
}
