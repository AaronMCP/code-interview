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
    /// Synchronous Socket to send and receive data
    /// </summary>
    public class ClientSocket
    {       

        #region Property
        int _SendTimeout = 1000 * 60;
        public int SendTimeout
        {
            get { return _SendTimeout; }
            set { _SendTimeout = value; }
        }
        
        int _ReceiveTimeout = 1000 * 60;
        public int ReceiveTimeout
        {
            get { return _ReceiveTimeout; }
            set { _ReceiveTimeout = value; }
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

        public string LocalIP
        {
           get
           {
               if (IsConnected())
                   return ((IPEndPoint)(_clientSocket.LocalEndPoint)).Address.ToString();
               else
                   return "";
           }
        }

        public int LocalPort
        {
            get
            {
                if (IsConnected())
                    return ((IPEndPoint)(_clientSocket.LocalEndPoint)).Port;
                else
                    return -1;
            }
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

        Logging _Log;
        public Logging Log
        {
            get { return _Log; }
        }

        #endregion

        #region Contrutor
        Socket _clientSocket;
        public ClientSocket(Logging log)
        {
            _Log = log;            
        }

        private void CreateSocket()
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocket.SendTimeout = _SendTimeout;
            _clientSocket.ReceiveTimeout = _ReceiveTimeout;
            _clientSocket.ReceiveBufferSize = 8192;
            _clientSocket.SendBufferSize = 8192;
            _clientSocket.NoDelay = true;
            _clientSocket.Blocking = true;
        }
        #endregion

        #region Connect & Disconnect
        public bool IsConnected()
        {            
            if (_clientSocket == null)
                return false;
            else
                return _clientSocket.Connected; 
        
        }

        
        public bool Connect(string IP, int Port, Logging log)
        {            
            _ServerIP = IP;
            _ServerPort = Port;
            _Log = log;
            return vConnect();            
        }

        public bool Connect()
        {
            return vConnect();
        }

        private bool vConnect()
        {
            try
            {
                if (_clientSocket == null)
                    this.CreateSocket();

                _clientSocket.ReceiveTimeout = _ReceiveTimeout;
                _clientSocket.SendTimeout = _SendTimeout;

                _clientSocket.Connect(IPAddress.Parse(_ServerIP), _ServerPort);
                if (_clientSocket.Connected)
                    _Log.Write(LogType.Debug, "================ Connect " + _ServerIP + ":" + _ServerPort.ToString() + "Success =======");
                else
                    _Log.Write(LogType.Debug, "================ Connect " + _ServerIP + ":" + _ServerPort.ToString() + "failure =======");
                return _clientSocket.Connected;
            }
            catch (Exception ex)
            {
                _Log.Write(ex);
                return false;
            }
        }


        public void DisConnect(bool IsResue)
        {
            if (_clientSocket != null)
            {
                _clientSocket.Close();
                _clientSocket = null;
            }
        }
        #endregion

        byte[] bufReceive = new byte[1024];

        public string SendMsg(string sMsg)
        {
            
            byte[] bMsg = System.Text.Encoding.GetEncoding(this.CodePageName).GetBytes(sMsg);

            string sLen = bMsg.Length.ToString("D0" + CommandBase.PackageHeadLength); //CommandBase.PackageHeadLength=7

            byte[] buf = new byte[this.PackageHeadLength + bMsg.Length];              //this.PackageHeadLength=7 * 1/2/4

            Array.Copy(System.Text.Encoding.GetEncoding(this.CodePageName).GetBytes(sLen), buf, this.PackageHeadLength);
            Array.Copy(bMsg, 0, buf, this.PackageHeadLength, bMsg.Length);

            
            byte[] result = SendMsg(buf);

            if (result == null)
                return null;
            else
                return System.Text.Encoding.GetEncoding(this.CodePageName).GetString(result).Substring(CommandBase.PackageHeadLength);
            
        }

        private byte[] SendMsg(byte[] package)
        {
            try
            {
                if (!this.IsConnected())
                    throw new Exception("Socket has not connected to server!");

                // Send PackageHead
                _Log.Write(LogType.Debug, "---------------- Send Data Begin... ------------------", true);
                _Log.Write(LogType.Debug, System.Text.Encoding.GetEncoding(this.CodePageName).GetString(package));

                int iLeave = PackageHeadLength;
                int iSend = 0;
                while (iLeave > 0)
                {
                    iSend = _clientSocket.Send(package, PackageHeadLength - iLeave, iLeave, SocketFlags.None);
                    if (iSend < 0)
                        return null;
                    iLeave = iLeave - iSend;
                }

                // Send body
                int pLen = Convert.ToInt32(System.Text.Encoding.GetEncoding(this.CodePageName).GetString(package, 0, PackageHeadLength));
                iLeave = pLen;
                while (iLeave > 0)
                {
                    iSend = _clientSocket.Send(package, PackageHeadLength + pLen - iLeave, iLeave, SocketFlags.None);
                    if (iSend < 0)
                        return null;
                    iLeave = iLeave - iSend;
                }                
                _Log.Write(LogType.Debug, "---------------- Send Data Success! ------------------\r\n",true);
                

                // Receive Msg                              
                _Log.Write(LogType.Debug, "---------------- Receive Data Begin...-----------------", true);
                byte[] buf = new byte[PackageHeadLength];
                iLeave = PackageHeadLength;
                int iReceive = 0;
                while (iLeave > 0)
                {
                    iReceive = _clientSocket.Receive(buf, PackageHeadLength - iLeave, iLeave, SocketFlags.None);
                    if (iReceive < 0) return null;
                    iLeave = iLeave - iReceive;
                }

                pLen = Convert.ToInt32(System.Text.Encoding.GetEncoding(this.CodePageName).GetString(buf, 0, PackageHeadLength));
                iLeave = pLen;
                byte[] result = new byte[pLen + PackageHeadLength];
                Array.Copy(buf, result, buf.Length);
                while (iLeave > 0)
                {
                    iReceive = _clientSocket.Receive(bufReceive, pLen - iLeave, iLeave, SocketFlags.None);
                    if (iReceive < 0)
                        return null;
                    iLeave = iLeave - iReceive;
                }

                Array.Copy(bufReceive, 0, result, PackageHeadLength, pLen);

                _Log.Write(LogType.Debug, System.Text.Encoding.GetEncoding(this.CodePageName).GetString(result, 0, pLen + PackageHeadLength) );
                _Log.Write(LogType.Debug, "---------------- Receive Data Success!-----------------\r\n",true);
                
                return result;
            }
            catch (ObjectDisposedException)
            {
                _Log.Write(LogType.Error, " OnDataReceived: Socket has been closed\r\n");
                return null;
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054) // Error code for Connection reset by peer
                {
                    string logmsg = "Client " + _ServerIP + ":" + _ServerPort.ToString() + " Disconnected" + "\r\n";
                    _Log.Write(LogType.Error, logmsg);
                    return null;
                }
                else
                {
                    _Log.Write(se);
                    return null;
                }
            }           
        }
    }
}
