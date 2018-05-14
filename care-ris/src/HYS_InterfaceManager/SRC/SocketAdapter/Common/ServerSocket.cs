using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Command;

namespace HYS.SocketAdapter.Common
{

    /// <summary>
    /// Called by main programs
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="result"></param>
    public delegate string ClientDataReceivedStrEvent(string msg);
    
    //public delegate byte[] ClientDataReceivedByteEvent(byte[] package);
    
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

        //PackageHeadLenth different on different code page, program need to treat the case used by different method
        int PackageHeadLength = CommandBase.PackageHeadLength;

        string _CodePageName = "utf-8";
        public string CodePageName
        {
            get { return _CodePageName; }
            set
            {
                PackageHeadLength = EncodingPage.GetAsciiWithOnTheCodePage(value) * CommandBase.PackageHeadLength;
                _CodePageName = value;
            }
        }

        #endregion

        #region constructor
        public ServerSocket(string IP, int Port, Logging log)
        {
            _Log = log;
            _ListenPort = Port;

            if (IP.Trim() == "")
                _LocalIP = "127.0.0.1";                
            else
                _LocalIP = IP;

            _Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if(_LocalIP=="127.0.0.1")
                _Listener.Bind(new IPEndPoint(IPAddress.Any, Port));                
            else
               _Listener.Bind(new IPEndPoint(IPAddress.Parse(_LocalIP), Port));                
        }
        #endregion


        #region Start & Stop
        bool _IsListening;
        public bool IsListening
        {
            get { return _IsListening; }
        }


        public bool Start()
        {
            try
            {                
                _Listener.SendTimeout    = _SendTimeout;
                _Listener.ReceiveTimeout = _ReceiveTimeout;

                _Listener.Listen(100);

                _IsListening = true;
                _Log.Write(LogType.Debug, "=================== Server Socket Start to Listen At " + LocalIP+":"+ListenPort.ToString() + "===============\r\n");

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
            _Listener.Close();
            Clean();
            _IsListening = false;
            _Log.Write(LogType.Warning, "\r\n=================== Server Socket Stop to Listen At " + LocalIP + ":" + ListenPort.ToString() + "===============");
        }

        private void Clean()
        {
            
            _socketList.Clear();
        }
        #endregion

        public ClientDataReceivedStrEvent OnClientDataReceived=null;
        private SocketList _socketList = new SocketList();

        #region Data Receive & Send Treation
        // This is the call back function, which will be invoked when a client is connected
        public void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                // Here we complete/end the BeginAccept() asynchronous call
                // by calling EndAccept() - which returns the reference to
                // a new Socket object
                Socket workerSocket = _Listener.EndAccept(asyn);

                string socketID = Guid.NewGuid().ToString();                

                PacketSocket ps = new PacketSocket(workerSocket, socketID);

                _socketList.Add(ps);
                
                // Let the worker Socket do the further processing for the 
                // just connected client
                WaitForData(ps, socketID);

                // Since the main Socket is now free, it can go back and wait for
                // other clients who are attempting to connect
                _Listener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (ObjectDisposedException)
            {
                //System.Diagnostics.Debugger.Log(0, "1", "\r\n OnClientConnection: Socket has been closed\r\n");
                _Log.Write(LogType.Error, "OnDataReceived: Socket has been closed\r\n");
            }
            catch (SocketException se)
            {
                //MessageBox.Show(se.Message);
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

                //Note: SocketFlags.Peek: 读到缓冲区的数据 仍 可通过Receive取得
                //      SocketFlags.None: 读到缓冲区的数据 不 可通过Receive取得
                psoc.currentSocket.BeginReceive(psoc.dataBuffer, 0,
                    psoc.dataBuffer.Length,
                    SocketFlags.Peek,
                    pfnWorkerCallBack,
                    psoc);
            }
            catch (SocketException se)
            {
                _Log.Write(se);
            }
        }
       

        public void OnDataReceived(IAsyncResult asyn)
        {
            PacketSocket psoc = (PacketSocket)asyn.AsyncState;
            try
            {
                _Log.Write(LogType.Debug, "-------------- Receive Data Begin..-----------------",true);
                IPEndPoint ep = ((IPEndPoint)psoc.currentSocket.RemoteEndPoint);

                // Complete the BeginReceive() asynchronous call by EndReceive() method
                // which will return the number of characters written to the stream by the client
                int iRx = psoc.currentSocket.EndReceive(asyn);
                if (!psoc.currentSocket.Connected)
                {
                    _Log.Write(LogType.Debug, "psoc.currentSocket.Connected=false", true);
                    //return;
                }
                if (iRx < 1)
                {
                    // Close Socket 
                    psoc.currentSocket.Close();
                    _socketList.Delete(psoc.SocketID);
                    return;
                }

                byte[] bufHead = new byte[PackageHeadLength];
                int iLeave = PackageHeadLength;
                while (iLeave > 0)
                {
                    int iReceive = psoc.currentSocket.Receive(bufHead, PackageHeadLength - iLeave, iLeave, SocketFlags.None);
                    if (iReceive < 0)
                        throw new Exception("Receive Error! ReceiveCount<0");
                    iLeave = iLeave - iReceive;
                }

                int pLen = Convert.ToInt32(System.Text.Encoding.GetEncoding(this.CodePageName).GetString(bufHead, 0, PackageHeadLength));

                // Receive Package body
                byte[] bufReceive = new byte[pLen + PackageHeadLength];
                Array.Copy(bufHead, bufReceive, bufHead.Length);
                iLeave = pLen ;
                while (iLeave > 0)
                {
                    int iReceive = psoc.currentSocket.Receive(bufReceive, PackageHeadLength + pLen - iLeave, iLeave, SocketFlags.None);
                    if (iReceive < 0)
                        throw new Exception("Receive Error! ReceiveCount<0");
                    iLeave = iLeave - iReceive;
                }
                
                string recMsg = System.Text.Encoding.GetEncoding(this._CodePageName).GetString(bufReceive);
                _Log.Write(LogType.Debug, recMsg);
                _Log.Write(LogType.Debug, "--------------  Receive data success from " + psoc.SocketID + " " + ep.Address.ToString() + ":" + ep.Port.ToString() + "\r\n " );

                recMsg = recMsg.Substring(CommandBase.PackageHeadLength);   //char lenth, not byte lenth
                //Call main program function                      
                if (OnClientDataReceived == null)
                {
                    _Log.Write(LogType.Warning,"OnClientDataReceived is not implemented!",true);
                    return;
                }

                //byte[] result = OnClientDataReceived(bufReceive);
                string result = OnClientDataReceived(recMsg);

                if(result == null)
                {
                    _Log.Write(LogType.Warning, "No result from OnClientDataReceived !", true);
                    return;
                }
                      
          
                SendMsgToClient(result, psoc.SocketID);

                // Close Socket 
                psoc.currentSocket.Close();
                _socketList.Delete(psoc.SocketID);

                //WaitForData(psoc, psoc.SocketID);
            }
            catch (ObjectDisposedException)
            {
                //System.Diagnostics.Debugger.Log(0, "1", "\r\nOnDataReceived: Socket has been closed\r\n");
                _Log.Write(LogType.Error, "\r\nOnDataReceived: Socket has been closed\r\n");
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054) // Error code for Connection reset by peer
                {
                    string logmsg = "\r\nClient " + " Disconnected" + "\r\n";
                    _Log.Write(LogType.Error, logmsg);

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
        }
        #endregion

        #region Send Message
        private bool SendMsgToClient(string sMsg, string socketID)
        {
            
            byte[] bMsg = System.Text.Encoding.GetEncoding(this._CodePageName).GetBytes(sMsg);

            string sLen = bMsg.Length.ToString("D0" + CommandBase.PackageHeadLength); //CommandBase.PackageHeadLength=7

            byte[] buf = new byte[this.PackageHeadLength + bMsg.Length];             //this.PackageHeadLength = 7 * 1/2/4

            Array.Copy(System.Text.Encoding.GetEncoding(this.CodePageName).GetBytes(sLen), buf, this.PackageHeadLength);
            Array.Copy(bMsg, 0, buf, this.PackageHeadLength, bMsg.Length);

            return SendMsgToClient(buf, socketID);
        }
        
        /// <summary>
        /// Find client Socket by the socketID, and send data to client
        /// </summary>
        /// <param name="package"></param>
        /// <param name="socketID"></param>
        /// <returns></returns>               
        private bool SendMsgToClient(byte[] package, string socketID)
        {
            PacketSocket psoc = (PacketSocket)_socketList.Find(socketID);
            string msg;
            try
            {
                if (psoc == null)
                    throw new Exception("Cannot find PacketSocket whose socketID = " + socketID);


                msg = System.Text.Encoding.GetEncoding(this.CodePageName).GetString(package);

                IPEndPoint ep = ((IPEndPoint)psoc.currentSocket.RemoteEndPoint);

                _Log.Write(LogType.Debug, "--------------- Send Data begin...------------------\r\n", true);
                _Log.Write(LogType.Debug, msg);

                // Send PackageHead
                int iLeave = PackageHeadLength;
                int iSend = 0;
                while (iLeave > 0)
                {
                    iSend = psoc.currentSocket.Send(package, PackageHeadLength - iLeave, iLeave, SocketFlags.None);
                    if (iSend < 0)
                        throw new Exception("Error find on the process of sending data!");
                    iLeave = iLeave - iSend;
                }

                // Send body
                int pLen = Convert.ToInt32(System.Text.Encoding.GetEncoding(this.CodePageName).GetString(package, 0, PackageHeadLength));
                iLeave = pLen;
                while (iLeave > 0)
                {
                    iSend = psoc.currentSocket.Send(package, PackageHeadLength + pLen - iLeave, iLeave, SocketFlags.None);
                    if (iSend < 0)
                        throw new Exception("Error find on the process of sending data!");
                    iLeave = iLeave - iSend;
                }
                _Log.Write(LogType.Debug, "\r\nSend Data Success To " + psoc.SocketID + " " + ep.Address.ToString() + ":" + ep.Port.ToString() + "\r\n " + msg + "\r\n");
                return true;
            }
            catch (ObjectDisposedException)
            {
                //System.Diagnostics.Debugger.Log(0, "1", "\r\nOnDataReceived: Socket has been closed\r\n");
                _Log.Write(LogType.Error, "\r\nOnDataReceived: Socket has been closed\r\n");
                return false;
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054) // Error code for Connection reset by peer
                {
                    string logmsg = "Client " + " Disconnected" + "\r\n";
                    _Log.Write(LogType.Error, logmsg);

                    // Remove the reference to the worker socket of the closed client
                    // so that this object will get garbage collected
                    psoc.currentSocket.Close();
                    _socketList.Delete(psoc.SocketID);
                    return false;

                }
                else
                {
                    _Log.Write(se);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _Log.Write(ex);
                return false;
            }
        }
        #endregion

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
	    public byte[] dataBuffer = new byte[1024*8];
    }
}
