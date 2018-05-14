using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;


namespace HYS.RdetAdapter.Configuration
{
    public class ServerRdetParams :XObject
    {
        public ServerRdetParams()
        {
        }

        public ServerRdetParams(string sListenIP, int iListenPort)
        {
            _ListenIP = sListenIP;
            _ListenPort = iListenPort;
        }

        string _ListenIP = "";
        public string ListenIP
        {
            get { return _ListenIP; }
            set { _ListenIP = value; }
        }

        int _ListenPort = -1;
        public int ListenPort
        {
            get { return _ListenPort; }
            set { _ListenPort = value; }
        }

        int _ConnectTimeout = 1000 * 60; //one minutes
        public int ConnectTimeout
        {
            get { return _ConnectTimeout; }
            set { _ConnectTimeout = value; }
        }

        int _ConnectTryCount = 5;
        public int ConnectTryCount
        {
            get { return _ConnectTryCount; }
            set { _ConnectTryCount = value; }
        }

        int _SendTimeout = 1000 * 60;
        public int SendTimeout
        {
            get { return _SendTimeout; }
            set { _SendTimeout = value; }
        }

        int _RecTimeout = 1000 * 120;
        public int RecTimeout
        {
            get { return _RecTimeout; }
            set { _RecTimeout = value; }
        }


    }

    public class ClientRdetParams : XObject
    {
        public ClientRdetParams()
        {
        }

        public ClientRdetParams(string sServerIP, int iServerPort)
        {
            _ServerIP = sServerIP;
            _ServerPort = iServerPort;
        }

        public ClientRdetParams(string sServerIP, int iServerPort, string sLocalIP, int iLocalPort)
        {
            _ServerIP = sServerIP;
            _ServerPort = iServerPort;

            _LocalIP = sLocalIP;
            _LocalPort = iLocalPort;
        }

        string _LocalIP = "";
        public string LocalIP
        {
            get { return _LocalIP; }
            set { _LocalIP = value; }
        }

        int _LocalPort = -1;
        public int LocalPort
        {
            get { return _LocalPort; }
            set { _LocalPort = value; }
        }


        string _ServerIP = "";
        public string ServerIP
        {
            get { return _ServerIP; }
            set { _ServerIP = value; }
        }

        int _ServerPort = -1;
        public int ServerPort
        {
            get { return _ServerPort; }
            set { _ServerPort = value; }
        }

        int _ConnectTimeout = 1000 * 60; //one minutes
        public int ConnectTimeout
        {
            get { return _ConnectTimeout; }
            set { _ConnectTimeout = value; }
        }

        int _ConnectTryCount = 5;
        public int ConnectTryCount
        {
            get { return _ConnectTryCount; }
            set { _ConnectTryCount = value; }
        }

        int _SendTimeout = 1000 * 60;
        public int SendTimeout
        {
            get { return _SendTimeout; }
            set { _SendTimeout = value; }
        }

        int _RecTimeout = 1000 * 60;
        public int RecTimeout
        {
            get { return _RecTimeout; }
            set { _RecTimeout = value; }
        }

    }
}
