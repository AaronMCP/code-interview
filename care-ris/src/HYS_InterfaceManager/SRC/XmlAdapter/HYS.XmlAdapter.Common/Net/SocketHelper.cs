using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Text;

using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.XmlAdapter.Common.Net
{
    public class SocketHelper
    {
        private static int _socketID = 0;
        public static int GetNewSocketID()
        {
            //thread safe, and handle overflow automatically
            return Interlocked.Increment(ref _socketID);
        }

        public const int ReceiveRequestBufferSize = 1024 * 8;
        public const int ReceiveResponseBufferSize = 1024;
        
        public static Encoding GetEncoder(string codePageName)
        {
            try
            {
                return Encoding.GetEncoding(codePageName);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                return Encoding.UTF8;
            }
        }

        public static bool IsEOF(string strReceived, string strEndSign)
        {
            string strSign = strEndSign;
            if (strSign == null) return true;

            string strRec = strReceived;
            if (strRec == null) strRec = "";
            strRec = strRec.TrimEnd(new char[] { '\n', '\r' });

            int strLen = strRec.Length;
            int signLen = strSign.Length;

            if (strLen <= signLen) return false;
            return (strSign == strRec.Substring(strLen - signLen, signLen));
        }
        public static string EnsureEOF(string strToBeSend, string strEndSign)
        {
            string strSign = strEndSign;
            if (strSign == null) strSign = "";

            string strSend = strToBeSend;
            if (strSend == null || strSend.Length < 1) return strSign;

            int sendLen = strSend.Length;
            int signLen = strSign.Length;

            if (sendLen < signLen || strSign != strSend.Substring(sendLen - signLen, signLen))
                strSend = strSend + strSign;

            return strSend;
        }
        public static string DeleteEOF(string strReceived, string strEndSign)
        {
            string strRec = strReceived;
            if (strRec == null) strRec = "";

            string strSign = strEndSign;
            if (strSign == null) return strRec;

            int recLen = strRec.Length;
            int signLen = strSign.Length;

            if (recLen > 0 && recLen >= signLen &&
                strSign == strRec.Substring(recLen - signLen, signLen))
                strRec = strRec.Substring(0, recLen - signLen);

            return strRec;
        }

        #region Synchronous Send

        private class SocketWrapper
        {
            public string ID;
            public Socket Socket;
            public SocketWrapper(string id, Socket s)
            {
                ID = id;
                Socket = s;
            }
        }
        private static ManualResetEvent _allDone = new ManualResetEvent(false);
        private static void OnDataSent(IAsyncResult asyn)
        {
            try
            {
                SocketWrapper s = asyn.AsyncState as SocketWrapper;
                if (s == null) return;

                int bytesSent = s.Socket.EndSend(asyn);
                SocketLogMgt.SetLog(s.ID + "Sent succeeded. " + bytesSent.ToString() + " bytes.");

                _allDone.Set();
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
            }
        }

        public static SocketResult SendData(string ip, int port, string content, bool includeHeader)
        {
            SocketConfig config = new SocketConfig();
            config.IncludeHeader = includeHeader;
            config.IPAddress = ip;
            config.Port = port;

            return SendData(config, content);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SocketResult SendData(SocketConfig config, string content)
        {
            SocketResult result = SocketResult.Empty;

            try
            {
                SocketLogMgt.SetLog("=============================================");
                SocketLogMgt.SetLog(": Send Data Begin.");
                SocketLogMgt.SetLog(config);

                string strSend = EnsureEOF(content, config.SendEndSign);

                if (SocketLogMgt.DumpData)
                {
                    SocketLogMgt.SetLog(": Data to be sent.");
                    SocketLogMgt.SetLog("------------------------");
                    SocketLogMgt.SetLog(strSend);
                    SocketLogMgt.SetLog("------------------------");
                }

                Byte[] bytesSent = null;
                Encoding encoder = GetEncoder(config.CodePageName);
                if (config.IncludeHeader)
                {
                    bytesSent = GetRequestByteWithHeader(encoder, strSend, config);
                }
                else
                {
                    bytesSent = encoder.GetBytes(strSend);
                }
                if (bytesSent == null)
                {
                    SocketLogMgt.SetLog(SocketLogType.Error, "Encode data failed.");
                    return SocketResult.SendFailed;
                }

                Byte[] bytesReceived = new Byte[ReceiveResponseBufferSize];

                SocketLogMgt.SetLog(": Socket prepared.");
                SocketLogMgt.SetLog("------------------------");

                string strReceived = null;
                StringBuilder sb = new StringBuilder();
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(config.IPAddress), config.Port);
                using (Socket socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    int id = GetNewSocketID();
                    string strID = "(" + id.ToString() + ") ";

                    //socket.NoDelay = true;
                    socket.SendTimeout = config.SendTimeout;
                    socket.ReceiveTimeout = config.ReceiveTimeout;

                    SocketLogMgt.SetLog(strID + "Socket created.");

                    socket.Connect(ipe);
                    if (socket.Connected)
                    {
                        SocketLogMgt.SetLog(strID + "Socket connected.");
                    }
                    else
                    {
                        SocketLogMgt.SetLog(SocketLogType.Warning, strID + "Connection failed.");
                        return SocketResult.Disconnect;
                    }

                    _allDone.Reset();
                    SocketLogMgt.SetLog(strID + "Send data.");
                    socket.BeginSend(bytesSent, 0, bytesSent.Length, 0,
                        new AsyncCallback(OnDataSent), new SocketWrapper(strID, socket));

                    bool rec = true;
                    while (rec)
                    {
                        if (!socket.Connected)
                        {
                            SocketLogMgt.SetLog(SocketLogType.Warning, strID + "Connection closed.");
                            break;
                        }

                        SocketLogMgt.SetLog(strID + "Receive data.");
                        int bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                        SocketLogMgt.SetLog(strID + "Receive succeeded. " + bytes.ToString() + " bytes.");
                        string str = encoder.GetString(bytesReceived, 0, bytes);
                        sb.Append(str);

                        strReceived = sb.ToString();
                        rec = !IsEOF(strReceived, config.ReceiveEndSign);
                    }

                    _allDone.WaitOne();

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();

                    SocketLogMgt.SetLog(strID + "Socket disconnected.");
                }

                SocketLogMgt.SetLog("------------------------");

                if (SocketLogMgt.DumpData)
                {
                    SocketLogMgt.SetLog(": Data received.");
                    SocketLogMgt.SetLog("------------------------");
                    SocketLogMgt.SetLog(strReceived);
                    SocketLogMgt.SetLog("------------------------");
                }

                strReceived = GetMessageContent(strReceived);
                result = new SocketResult(strReceived);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                result = new SocketResult(err);
            }

            SocketLogMgt.SetLog(": Send Data End");
            SocketLogMgt.SetLog("=============================================\r\n");
            return result;
        }

        private static string _requestHeaderTemplate;
        public static string RequestHeaderTemplate
        {
            get
            {
                if (_requestHeaderTemplate == null)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("POST /HYS-EC HTTP/1.1");
                    sb.AppendLine("Content-Type: text/xml");
                    sb.AppendLine("Content-Length: [LENGTH]");
                    sb.AppendLine("SOAPAction: \"\"");
                    sb.AppendLine("Host: localhost:[PORT]");
                    sb.AppendLine("Pragma: no-cache");
                    sb.AppendLine();

                    _requestHeaderTemplate = sb.ToString();
                }
                return _requestHeaderTemplate;
            }
        }
        private static string _responseHeaderTemplate;
        public static string ResponseHeaderTemplate
        {
            get
            {
                if (_responseHeaderTemplate == null)
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("HTTP/1.1 200 OK");
                    sb.AppendLine("Server: XIM(HL7) Adapter of GC Gateway 2.0");
                    sb.AppendLine("Content-Type: text/xml");
                    sb.AppendLine("Content-Length: [LENGTH]");
                    sb.AppendLine("SOAPAction: ");
                    sb.AppendLine("Host: ");
                    sb.AppendLine();

                    _responseHeaderTemplate = sb.ToString();
                }
                return _responseHeaderTemplate;
            }
        }
        public static byte[] GetRequestByteWithHeader(Encoding encoder, string strSend, SocketConfig config)
        {
            if (encoder == null || strSend == null || config == null) return null;
            int bcount = encoder.GetByteCount(strSend);
            string header = RequestHeaderTemplate;
            header = header.Replace("[LENGTH]", bcount.ToString());
            header = header.Replace("[PORT]", config.Port.ToString());
            return encoder.GetBytes(header + strSend);
        }
        public static byte[] GetResponseByteWithHeader(Encoding encoder, string strSend, SocketConfig config)
        {
            if (encoder == null || strSend == null || config == null) return null;
            int bcount = encoder.GetByteCount(strSend);
            string header = ResponseHeaderTemplate;
            header = header.Replace("[LENGTH]", bcount.ToString());
            header = header.Replace("[PORT]", config.Port.ToString());
            return encoder.GetBytes(header + strSend);
        }

        private static string _responseBeginSign;
        private static string ResponseBeginSign
        {
            get
            {
                if (_responseBeginSign == null)
                {
                    _responseBeginSign = "<XMLResponseMessage";
                }
                return _responseBeginSign;
            }
        }
        private static string GetMessageContent(string receiveData)
        {
            if (receiveData == null) return "";
            int index = receiveData.IndexOf(ResponseBeginSign);
            if (index < 0) return "";
            return receiveData.Substring(index, receiveData.Length - index);
        }

        #endregion

        #region Log Handler

        private static Logging _log;

        public static void EnableSocketLogging(Logging log, bool dumpData)
        {
            _log = log;
            SocketLogMgt.DumpData = dumpData;
            SocketLogMgt.OnLog += new SocketLogHandler(SocketLogMgt_OnLog);
            SocketLogMgt.OnError += new EventHandler(SocketLogMgt_OnError);
        }

        private static void SocketLogMgt_OnError(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[SOCKET_EXCEPTION] ");
            if (sender == null)
            {
                sb.Append("[NULL]");
            }
            else
            {
                sb.Append(sender.ToString());
            }
            Exception err = SocketLogMgt.LastError;
            if (err == null)
            {
                sb.Append(" [NULL]");
            }
            else
            {
                sb.Append("\r\n");
                sb.Append(err.ToString());
            }
            _log.Write(LogType.Error, sb.ToString());
        }

        private static void SocketLogMgt_OnLog(SocketLogType type, object sender, string message)
        {
            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                default:
                case SocketLogType.Debug:
                    {
                        if (sender == null)
                        {
                            sb.Append("[SOCKET_CLIENT] ");
                        }
                        else
                        {
                            sb.Append("[SOCKET_SERVER] ");
                        }
                        break;
                    }
                case SocketLogType.Error:
                    {
                        sb.Append("[SOCKET_ERROR] ");
                        break;
                    }
                case SocketLogType.Warning:
                    {
                        sb.Append("[SOCKET_WARNING] ");
                        break;
                    }
            }
            if (message == null)
            {
                sb.Append("[NULL]");
            }
            else
            {
                sb.Append(message);
            }
            _log.Write(sb.ToString());
        }

        #endregion
    }
}
