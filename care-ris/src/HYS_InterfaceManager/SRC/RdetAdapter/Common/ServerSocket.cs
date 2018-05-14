using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.RdetAdapter.Common
{

    /// <summary>
    /// Called by main programs
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="result"></param>
    public delegate void ClientDataReceivedStrEvent(string msg, ref string result);
    
    public delegate void ClientDataReceivedByRequest(CmdReqBase request, CmdRespBase resp);

    

    /// <summary>
    /// Asynchronous Socket
    /// </summary>
    public class ServerSocket 
    {
        Socket _Listener;

        #region Property

        string _LocalIP = "";
        public string LocalIP
        {
            get { return _LocalIP; }
            //set { _LocalIP = value; }
        }

        int _ListenPort = -1;
        public int ListenPort
        {
            get { return _ListenPort; }
            //set { _ListenPort = value; }
        }

        Logging _Log;
        public Logging Log
        {
            get { return _Log; }
            //set { _Log = value; }
        }

        
        int _SendTimeout = 1000 * 60;
        public int SendTimeout
        {
            get { return _SendTimeout; }
            set { _SendTimeout = value;}
        }

        int _ReceiveTimeout = 1000 * 60;
        public int ReceiveTimeout
        {
            get { return _ReceiveTimeout; }
            set { _ReceiveTimeout = value;}
        }

        bool _IsListening;
        public bool IsListening
        {
            get { return _IsListening; }
        }
        #endregion

        #region Constructor    

        public ServerSocket(string IP, int Port, Logging log)
        {
            _Log = log;
            _ListenPort = Port;


            if (IP.Trim() == "")
               _LocalIP = "127.0.0.1";                                           
            else
              _LocalIP = IP;
            
            
            IPAddress ipAddress = IPAddress.Parse(IP);
            //_Listener = new TcpListener(ipAddress, Port);
            _Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _Listener.Bind(new IPEndPoint(ipAddress,Port)); 
            
        }

        #endregion

       

        public bool Start()
        {
            try
            {                
                _Listener.SendTimeout    = _SendTimeout;
                _Listener.ReceiveTimeout = _ReceiveTimeout;

                _Listener.Listen(10);

                _IsListening = true;
                _Log.Write(LogType.Warning, "=================== Server Socket Start to Listen At " + LocalIP+":"+ListenPort.ToString() + "===============\r\n",true);

                _Listener.BeginAccept(new AsyncCallback(OnClientConnect), null);

                return true;
            }
            catch(Exception ex)
            {
                _IsListening = false;
                _Log.Write(ex);
                return false;
            }


        }

        public void Stop()
        {
            _Listener.Close(1000);
            Clean();
            _IsListening = false;
            _Log.Write(LogType.Warning, "=================== Server Socket Stop to Listen At " + LocalIP + ":" + ListenPort.ToString() + "===============\r\n",true);
        }

        private void Clean()
        {            
            _socketList.Clear();
        }

        public ClientDataReceivedByRequest OnClientDataReceived = null;

        
        private SocketList _socketList = new SocketList();

        // This is the call back function, which will be invoked when a client is connected
        public void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                Socket workerSocket = _Listener.EndAccept(asyn);

                string socketID = Guid.NewGuid().ToString();                

                PacketSocket ps = new PacketSocket(workerSocket, socketID);

                _socketList.Add(ps);

                WaitForData(ps, socketID);

                _Listener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                //System.Diagnostics.Debugger.Log(0, "1", "\r\n OnClientConnection: Socket has been closed\r\n");
                _Log.Write(LogType.Error, "\r\nOnDataReceived: Socket has been closed\r\n");
            }
            catch (SocketException se)
            {                
                _Log.Write(se);
            }

        }

        public AsyncCallback pfnWorkerCallBack;
        // Start waiting for data from the client
        public void WaitForData(PacketSocket psoc, string socketID)
        {
            try
            {
                if (pfnWorkerCallBack == null)
                {
                    pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
                }
                                
                psoc.currentSocket.BeginReceive(psoc.dataBuffer, 0,
                    psoc.dataBuffer.Length,
                    SocketFlags.Peek,  //Important: this flag enable recive()method get char from begin
                    pfnWorkerCallBack,
                    psoc);
            }
            catch (SocketException se)
            {
                _Log.Write(se);
            }
        }

        byte[] bufNull = { 0, 0 };
        private bool SendLine(string sLine, Socket socket)
        {
            byte[] bufSend = System.Text.Encoding.Unicode.GetBytes(sLine);
            int iLeave = bufSend.Length;
            while (iLeave > 0)
            {
                int iSend = socket.Send(bufSend, bufSend.Length - iLeave, iLeave, SocketFlags.None);
                iLeave = iLeave - iSend;
            }

            socket.Send(bufNull);

            return true;
        }

        private int ReadLine(ref string sLine, Socket socket)
        {
            byte[] buf = new byte[2];
            StringBuilder sb = new StringBuilder();

            while (true)
            {
                int r = socket.Receive(buf, 2, SocketFlags.None);
                if (r <= 0)
                    return r;
                if (buf[0] == 0 && buf[1] == 0)
                {
                    sLine = sb.ToString();
                    return sLine.Length;
                }
                
                //sb.Append(System.Text.Encoding.Unicode.GetString(buf));
                sb.Append(System.Text.Encoding.BigEndianUnicode.GetString(buf));
            }
        }

        private bool ParseLine(string sLine, CmdReqBase req)
        {

            int iPos = sLine.IndexOf("=");
            if (iPos < 0) return false;

            string Name = "", Value = "";
            Name = sLine.Substring(0, iPos);
            if (iPos < sLine.Length-1)
                Value = sLine.Substring(iPos + 1, sLine.Length - iPos - 1);

            if (CommandToken.IsCommandToken(Name))
                req.Command = Value;
            else
                req.AddParameter(Name, Value);
            return true;
        }
        
        public void OnDataReceived(IAsyncResult asyn)
        {
            PacketSocket psoc = (PacketSocket)asyn.AsyncState;
            try
            {
                if (!psoc.currentSocket.Connected)
                    return;
                IPEndPoint ep = ((IPEndPoint)psoc.currentSocket.RemoteEndPoint);

                int iRx = psoc.currentSocket.EndReceive(asyn);
                if (iRx < 1)
                {                    
                    return;
                }
                                
                CmdReqBase request = new CmdReqBase();
                string sLine = "";
                while (true)
                {
                    int r = ReadLine(ref sLine, psoc.currentSocket);
                    if (r == 0)
                        return;
                    if (r <= 0)
                        throw new Exception("Read data error!");
                    if (sLine.Trim() == "DONE")
                        break;
                    ParseLine(sLine, request);

                }
                _Log.Write(LogType.Debug, "Receive Command Success! Command Data as follow:------", true );
                PackageLog.WritePkgLog((object)request, _Log);
                _Log.Write(LogType.Debug, "---------------Command data end!-------------", true);

                // Treate Command 
                CmdRespBase response = new CmdRespBase();
                if (OnClientDataReceived != null)
                    OnClientDataReceived(request, response);
                // Response Command
                sLine = "ErrorCode=" + response.ErrorCode;
                SendLine(sLine, psoc.currentSocket);
                for (int i = 0; i < response.GetParamCount();i++ )
                {
                    sLine = response.GetParamName(i) + "=" + response.GetParamValue(i);
                    SendLine(sLine, psoc.currentSocket);
                }
                sLine = "DONE";
                SendLine(sLine, psoc.currentSocket);

                _Log.Write(LogType.Debug, "Send Command Response Success! Command Data as follow:-----", true);
                PackageLog.WritePkgLog(response, _Log);
                _Log.Write(LogType.Debug, "---------------Command data end!-------------", true);

                // Continue the waiting for data on the Socket
                // WaitForData(psoc, psoc.SocketID); //not need to wait for next request!once connect and disconnecct
                psoc.currentSocket.Close();
                _socketList.Delete(psoc.SocketID);  
            }
            catch (ObjectDisposedException)
            {
                //System.Diagnostics.Debugger.Log(0, "1", "\r\nOnDataReceived: Socket has been closed\r\n");
                _Log.Write(LogType.Error, "\r\nOnDataReceived: Socket has been closed\r\n", true);
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054) // Error code for Connection reset by peer
                {
                    string logmsg = "\r\nClient " + " Disconnected" + "\r\n";
                    _Log.Write(LogType.Error, logmsg,true);

                    // Remove the reference to the worker socket of the closed client
                    // so that this object will get garbage collected
                    psoc.currentSocket.Close();
                    _socketList.Delete(psoc.SocketID);  
                }
                else
                {
                    _Log.Write(se);
                }
            }
            catch (Exception exx)
            {
                _Log.Write(exx);
            }            
        }       
    }

    public class SocketList
    {
        System.Collections.Hashtable _List = new System.Collections.Hashtable();

        public bool Add(PacketSocket ps)
        {
            if (_List.ContainsKey(ps.SocketID))
                _List[ps.SocketID] = ps;
            else
                _List.Add(ps.SocketID, ps);
            return true;
        }

        public bool Delete(string socketID)
        {
            if (_List.ContainsKey(socketID))
                _List.Remove(socketID);
            return true;
        }

        public PacketSocket Find(string socketID)
        {
            if (_List.ContainsKey(socketID))
                return (PacketSocket)_List[socketID];
            else
                return null;
        }

        public void Clear()
        {
            foreach (object ps in _List.Values)
                ((PacketSocket)ps).currentSocket.Close();
            _List.Clear();
        }
    }

    public class PacketSocket
    {
        // Constructor which takes a Socket and a client number
        public PacketSocket(System.Net.Sockets.Socket socket, string socketID )
	    {
		    currentSocket = socket;
            _SocketID = socketID;            
	    }
        
	    public System.Net.Sockets.Socket currentSocket;        

	    private string _SocketID;
        public string SocketID
        {
            get { return _SocketID; }
        }
	    // Buffer to store the data sent by the client
	    public byte[] dataBuffer = new byte[1024];
    }
}
