using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter;

namespace HYS.SocketAdapter.Common
{
    /// <summary>
    /// Delegate used to notify main program that some message have received
    /// </summary>
    public delegate void DataReceivedEvent(string msg, ref string result);


    /// <summary>
    /// Socket Adapter Server side connection, responsible for listen and accecpt 
    /// remote connecting
    /// </summary>
    public class ServerConnection
    {
        string _IP = "";
        int _Port = -1;
        static Logging _Log;

        static bool _IsListening = false;
        static TcpListener _Listener;

        #region Contructor      

        public ServerConnection(string IP, int Port)
        {
            this.BuildServerConnection(IP, Port);
        }

        public ServerConnection(string IP, int Port, Logging Log)
        {
            _Log = Log;
            BuildServerConnection(IP, Port);
        }

        private void BuildServerConnection(string IP, int Port)
        {
            try
            {
                _IP = IP;
                _Port = Port;
                IPAddress ipAddress = Dns.GetHostAddresses(IP)[0];

                _Listener = new TcpListener(ipAddress, Port);
            }
            catch (Exception Ex)
            {
                //try
                //{
                    if (_Log != null)
                        _Log.Write(Ex);
                //}
                //catch (Exception e)
                //{ }

            }
        }

        #endregion

        public bool Start()
        {
            try
            {                
                _Listener.Start();
                _IsListening = true;
                _watchdog.Start();
                return true;
            }
            catch (Exception Ex)
            {
                if (_Log != null)
                    _Log.Write(Ex);
                return false;
            }
        }

        public void Stop()
        {
            _Listener.Stop();
            _IsListening = false;
        }

        static public DataReceivedEvent OnDataReceived;

        Thread _watchdog = new Thread( ThreadFun );

        static private void  ThreadFun(object data)
        {
            if (_Log != null)
                _Log.Write(LogType.Warning, "===================watch dog start...======================");
            while (_IsListening)
            {
                try
                {
                    //TODO: ADD MORE CONTROL CODE
                    Socket sock = _Listener.AcceptSocket();
                    byte[] buf = new byte[1024];
                    int size = sock.Receive(buf);
                    if (size > 0)
                    {
                        
                        string msg = System.Text.Encoding.Default.GetString(buf, 0, size);
                        _Log.Write(LogType.Debug, DateTime.Now.ToShortTimeString()+"  Server Rec:"+msg);
                        string result = "";
                        if (OnDataReceived != null)
                            OnDataReceived(msg,ref result);
                        if (result.Length > 0)
                        {
                            
                            if(sock.Send(System.Text.Encoding.Default.GetBytes(result))>0)
                                _Log.Write(LogType.Debug, DateTime.Now.ToShortTimeString() + " [Success]  Server Send:" + result);
                            else
                                _Log.Write(LogType.Debug, DateTime.Now.ToShortTimeString() + " [Faile] Server Send:" + result);
                        }

                    }
                    sock.Close();
                }
                catch (Exception Ex)
                {
                    if (_Log != null)
                        _Log.Write(Ex);
                }
            }


            if (_Log != null)
                _Log.Write(LogType.Warning, "=================== watch dog Exit ========================");
        }

    }


   
}
