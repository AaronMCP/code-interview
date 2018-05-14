using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketServerConfig : XObject, ICodePageIndicator
    {
        private string _socketWorkerType = SocketWorker.DEVICE_NAME;
        public string SocketWorkerType
        {
            get { return _socketWorkerType; }
            set { _socketWorkerType = value; }
        }

        private string _ipAddress = "127.0.0.1";
        public string IPAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        private int _port = 1234;
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

        private int _connectionTimeoutSecond = 120; //s : the time to diconnect after the last data receved or data sent, be effective mainly in long term connection socket worker, see the AllowMultipleSessionPerConnection setting.
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

        private bool _enableConnectionCollecting = false;   // if socket worker knows exactly when the client disconnect (see SocketWorker.OnDataReceived() for details), we do not need to enable this "connection collecting".
        public bool EnableConnectionCollecting
        {
            get { return _enableConnectionCollecting; }
            set { _enableConnectionCollecting = value; }
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

        private int _codePageCode = -1;
        /// <summary>
        /// As CodePageName only cover a sub-set of code pages which CodePageCode covers, when CodePageCode >= 0, it has higher priority than CodePageName. When CodePageCode==0, it indicats the usage of default code page.
        /// </summary>
        public int CodePageCode
        {
            get { return _codePageCode; }
            set { _codePageCode = value; }
        }

        // to control the risk of different socket worker's logics,
        // we seperate different logics in different classes created by SocketWorkerFactory,
        // so we do not use this setting to control the worker's logics currently.

        //// if AllowMultipleSessionPerConnection is true, client send all messages in one connection, server wait for client to close connection till connection timeout.
        //private bool _allowMultipleSessionPerConnection = false;   // note: One session = one requesting message + one acknowledgement message
        //public bool AllowMultipleSessionPerConnection
        //{
        //    get { return _allowMultipleSessionPerConnection; }
        //    set { _allowMultipleSessionPerConnection = value; }
        //}

        private int _receiveRequestBufferSizeKB = 8;
        public int ReceiveRequestBufferSizeKB
        {
            get { return _receiveRequestBufferSizeKB; }
            set { _receiveRequestBufferSizeKB = value; }
        }

        //private int _receiveResponseBufferSizeKB = 1;
        //public int ReceiveResponseBufferSizeKB
        //{
        //    get { return _receiveResponseBufferSizeKB; }
        //    set { _receiveResponseBufferSizeKB = value; }
        //}

        private bool _keepAlive = false;
        public bool KeepAlive
        {
            get { return _keepAlive; }
            set { _keepAlive = value; }
        }

        private uint _keepAliveTime = 36000000;
        public uint KeepAliveTime
        {
            get { return _keepAliveTime; }
            set { _keepAliveTime = value; }
        }

        private uint _keepAliveInterval = 1000;
        public uint KeepAliveInterval
        {
            get { return _keepAliveInterval; }
            set { _keepAliveInterval = value; }
        }
    }
}
