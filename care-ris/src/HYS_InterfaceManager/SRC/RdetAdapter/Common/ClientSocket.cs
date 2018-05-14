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

        int _ReceiveTimeout = 1000 * 120;
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
                if (_Socket.Connected)
                    return ((IPEndPoint)(_Socket.LocalEndPoint)).Address.ToString();
                else
                    return "";
            }
        }

        public int LocalPort
        {
            get
            {
                if (_Socket.Connected)
                    return ((IPEndPoint)(_Socket.LocalEndPoint)).Port;
                else
                    return -1;
            }
        }

        #endregion

        Socket _Socket = null;

        public ClientSocket(Logging log)            
        {
            _Log = log;
           // CreateSocket(log);
        }

        private void CreateSocket(Logging log)
        {
            _Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _Socket.SendTimeout = _SendTimeout;
            _Socket.ReceiveTimeout = _ReceiveTimeout;
            _Socket.ReceiveBufferSize = 8192;
            _Socket.SendBufferSize = 8192;
            _Socket.NoDelay = true;
            _Socket.Blocking = true;
            // clear buf before send data
            //_Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, false);            
        }

        public bool IsConnected
        {
            get
            {
                if (_Socket == null)
                    return false;
                else
                    return _Socket.Connected;
            }
        }

        Logging _Log;
        public bool Connect(string IP, int Port)
        {
            _ServerIP = IP;
            _ServerPort = Port;
            return vConnect();
        }

        public bool Connect()
        {
            return vConnect();
        }

        private bool vConnect()
        {
            if (_Socket !=null)
            {
                _Socket.Close();
                _Socket = null;
            }

            if (_Socket == null)
                CreateSocket(this._Log);

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    _Socket.ReceiveTimeout = _ReceiveTimeout;
                    _Socket.SendTimeout = _SendTimeout;
                    _Socket.Connect(_ServerIP, _ServerPort);
                    if (_Socket.Connected)
                        _Log.Write(LogType.Debug, "================ Connect " + _ServerIP + ":" + _ServerPort.ToString() + "Success =======\r\n");
                    else
                        _Log.Write(LogType.Debug, "================ Connect " + _ServerIP + ":" + _ServerPort.ToString() + "failure =======\r\n");

                    if (_Socket.Connected)
                        return true;
                }
                catch (Exception ex)
                {
                    _Log.Write(LogType.Error, " Connect " + _ServerIP + ":" + _ServerPort.ToString()  +" " +i.ToString() + " times  Failure. \r\n");
                    _Log.Write(ex);             
                }
            }

            return false;
        }


        public void DisConnect(bool bResuse)
        {         
            if (_Socket != null)
            {
                _Socket.Close();                
                _Socket = null;
            }
        }

        byte[] bufReceive = new byte[1024 * 8];

        byte[] bufNull = { 0,0};

        /// <summary>
        /// RDET Server Side exist a defect: 
        ///     when it send data , the high byte is later(writebyte(),writebyte(0),
        ///     when it receive data, the lower byte is later(readChar() )
        ///     So Out SendLine use System.Text.Encoding.BigEndianUnicode.GetBytes(sLine)
        ///            ReceiveLine using System.Text.Encoding. Unicode.GetBytes(sLine)
        /// 
        /// Convert String to Unicode then send out
        /// add #0#0 at end
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        private bool SendLine(string sLine)
        {
            byte[] bufSend = System.Text.Encoding.BigEndianUnicode.GetBytes(sLine);            
            int iLeave = bufSend.Length;
            while (iLeave > 0)
            {
                int iSend = _Socket.Send(bufSend, bufSend.Length - iLeave, iLeave, SocketFlags.None);
                iLeave = iLeave - iSend;
            }

            _Socket.Send(bufNull);

            return true;
        }



        private int ReadLine(ref string sLine)
        {
            byte[] buf = new byte[2];
            StringBuilder sb = new StringBuilder();
            
            
            while( true )
            {
                int iLeave = 2;
                while (iLeave > 0)
                {
                    int r = _Socket.Receive(buf,2-iLeave, iLeave, SocketFlags.None);
                    if (r <= 0)
                        return r;                    
                    iLeave = iLeave - r;
                }
                
                if (buf[0] == 0 && buf[1] == 0)
                {
                    int r = 0;
                    if (sb.Length < 1)
                    {
                        byte[] temp=new byte[1024*4];
                        r = _Socket.Receive(temp, 0, 1024*4, SocketFlags.None);
                        this.ReceiveBytes(temp, 0, r);
                    }
                                                            
                    sLine = sb.ToString();
                    return sLine.Length;
                }

                this.ReceiveBytes(buf, 0, 2);

                sb.Append(System.Text.Encoding.Unicode.GetString(buf));               
                
            }
        }

        

        private bool ParseLine(string sLine, CmdRespBase resp)
        {
            
            int iPos = sLine.IndexOf("=");
            if(iPos<0) return false;

            string Name = "", Value = "";
            Name = sLine.Substring(0, iPos);
            if (iPos < sLine.Length-1)
                Value = sLine.Substring(iPos + 1, sLine.Length - iPos -1);

            if (CommandToken.IsErrorCodeToken(Name))
                resp.ErrorCode = Value;
            else
                resp.AddParameter(Name, Value);
            return true;
        }
        
        public CmdRespBase SendCommand( CmdReqBase Request )
        {
            try
            {
                if (!this.IsConnected)
                {
                    if (!Connect())
                        return null;
                }

                #region Send
                try
                {
                    _Log.Write(LogType.Debug, "----------- Send Command Begin...-----------\r\n", true);
                    //send command
                    SendLine("Command=" + Request.Command);
                    //send parameter
                    for (int i = 0; i < Request.GetParamCount();i++ )
                    {
                        SendLine(Request.GetParamName(i) + "=" + Request.GetParamValue(i));
                    }
                    //send DONE
                    SendLine("DONE");

                    // log
                    PackageLog.WritePkgLog(Request, _Log);
                    _Log.Write(LogType.Debug, "------------ Send Command Success!----------\r\n", true);

                }
                catch (Exception ex)
                {
                    PackageLog.WritePkgLog(Request, _Log);
                    _Log.Write(LogType.Error, "---------- Send Command Failure!------------\r\n", true);
                    throw ex;
                }
                #endregion

                #region Receive
                try
                {
                    this.ResetCache();
   
                    _Log.Write(LogType.Debug, "----------- Receive Response Begin...-------\r\n", true);
                    CmdRespBase response ;
                    if (CommandToken.IsNewPatient(Request.Command))
                        response = new CmdRespNewPatient();
                    else                        
                       response = new CmdRespBase();

                    string sLine = "";
                    int r = ReadLine(ref sLine);
                    while (r > 0)
                    {
                        if (sLine == "DONE")
                            break;
                        else
                            ParseLine(sLine, response);

                        r = ReadLine(ref sLine);
                    }

                    if (r > 0)
                    {
                        PackageLog.WritePkgLog(response, _Log);
                        _Log.Write(LogType.Debug, "----------- Receive Response Success!-------\r\n", true);                        
                        return response;
                    }
                    else
                    {
                        _Log.Write(LogType.Error, "Recive response Failure\r\nReceived byte as follow:\r\n" + this.GetCatchHexString(), true);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    _Log.Write(LogType.Error, "Recive response Failure\r\nReceived byte as follow:\r\n"+this.GetCatchHexString(), true);
                    throw ex;
                }
                #endregion

            }
            catch (ObjectDisposedException)
            {
                _Log.Write(LogType.Error, "OnDataReceived: Socket has been closed\r\n");
                return null;
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054) // Error code for Connection reset by peer
                {
                    string logmsg = "Server " + _ServerIP + ":" + _ServerPort.ToString() + " Disconnected" + "\r\n";
                    _Log.Write(LogType.Error, logmsg, true);
                    return null;
                }
                else
                {
                    _Log.Write(se);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _Log.Write(ex);
                return null;
            }
        }

        #region cache , when error produced, dump received byte

        byte[] _ReceiveBuf = new byte[1024 * 8];
        int _iPosOfBuf = 0;

        private void ResetCache()
        {
            _iPosOfBuf = 0;
        }
        private void ReceiveBytes(byte[] buf, int iStart, int iLen)
        {
            Array.Copy(buf, iStart, _ReceiveBuf, _iPosOfBuf, iLen);
            _iPosOfBuf += iLen;
        }

        private string GetCatchHexString()
        {
            return GetHexString(_ReceiveBuf, 0, _iPosOfBuf);
        }

        private string GetHexString(byte[] bList, int iStart, int iLen)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("byte list total-"+iLen.ToString()+" bytes as hex string");
            sb.AppendLine("===========");
            for (int i = iStart; i < iStart + iLen; i++)
            {
                sb.AppendLine(bList[i].ToString("x2"));
            }
            sb.AppendLine("===========");
            return sb.ToString();
        }
        #endregion
    }    
}
