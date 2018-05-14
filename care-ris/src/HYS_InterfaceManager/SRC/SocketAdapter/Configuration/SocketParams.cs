using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;


namespace HYS.SocketAdapter.Configuration
{
    public class ServerSocketParams :XObject
    {
        public ServerSocketParams()
        {
        }

        public ServerSocketParams(string sListenIP, int iListenPort)
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

        int _RecTimeout = 1000 * 60;
        public int RecTimeout
        {
            get { return _RecTimeout; }
            set { _RecTimeout = value; }
        }

        string _CodePageName = "utf-8";
        public string CodePageName 
        {
            get{return _CodePageName;}
            set{_CodePageName = value;}
        }

        
    }

    public class ClientSocketParams : XObject
    {
        public ClientSocketParams()
        {
        }

        public ClientSocketParams(string sServerIP, int iServerPort)
        {
            _ServerIP = sServerIP;
            _ServerPort = iServerPort;
        }

        //public ClientSocketParams(string sServerIP, int iServerPort, string sLocalIP, int iLocalPort)
        //{
        //    _ServerIP = sServerIP;
        //    _ServerPort = iServerPort;

        //    _LocalIP = sLocalIP;
        //    _LocalPort = iLocalPort;
        //}

        string _CallbackIP = "";
        public string CallbackIP
        {
            get { return _CallbackIP; }
            set { _CallbackIP = value; }
        }

        int _CallbackPort = 7000;
        public int CallbackPort
        {
            get { return _CallbackPort; }
            set { _CallbackPort = value; }

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

        int _ServerPort = 6000;
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

        string _CodePageName = "utf-8";
        public string CodePageName
        {
            get { return _CodePageName; }
            set { _CodePageName = value; }
        }


    }
}
