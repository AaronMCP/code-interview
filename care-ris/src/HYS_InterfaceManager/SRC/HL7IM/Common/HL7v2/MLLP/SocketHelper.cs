using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Text;
using HYS.IM.Common.Logging;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketHelper
    {
        private static int _socketID = 0;
        public static int GetNewSocketID()
        {
            //thread safe, and handle overflow automatically
            return Interlocked.Increment(ref _socketID);
        }

        //public const int ReceiveRequestBufferSize = 1024 * 8;
        //public const int ReceiveResponseBufferSize = 1024;

        public static string BlockBeginSign = string.Format("{0}", (char)0x0B);                 // 0x0B=\v
        public static string BlockEndSign = string.Format("{0}{1}", (char)0x1C, (char)0x0D);    // 0x0D=\r 0x0A=\n

        public static string PackMessageBlock(string content)
        {
            string str = string.Format("{0}{1}{2}", BlockBeginSign, content, BlockEndSign);
            return str;
        }
        public static string UnpackMessageBlock(string block)
        {
            if (block == null) return "";
            int beginIndex = block.IndexOf(BlockBeginSign);
            if (beginIndex < 0) return "";
            beginIndex += BlockBeginSign.Length;
            if (beginIndex >= block.Length) return "";
            int endIndex = block.IndexOf(BlockEndSign, beginIndex);
            if (endIndex < 0) return "";
            return block.Substring(beginIndex, endIndex - beginIndex);
        }
        public static string UnpackMessageBlock(string block, int startIndex)
        {
            if (block == null) return "";
            int beginIndex = block.IndexOf(BlockBeginSign, startIndex);
            if (beginIndex < 0) return "";
            beginIndex += BlockBeginSign.Length;
            if (beginIndex >= block.Length) return "";
            int endIndex = block.IndexOf(BlockEndSign, beginIndex);
            if (endIndex < 0) return "";
            return block.Substring(beginIndex, endIndex - beginIndex);
        }
        public static string TrimMessageBlock(string block)
        {
            string str = block;
            if (str == null) return "";
            if (str.StartsWith(BlockBeginSign)) 
                str = str.Remove(0, BlockBeginSign.Length);
            if (str.Length >= BlockEndSign.Length && str.EndsWith(BlockEndSign)) 
                str = str.Remove(str.Length - BlockEndSign.Length, BlockEndSign.Length);
            return str;
        }
        public static bool FindBlockEnding(string segment)
        {
            if (segment == null) return false;
            int index = segment.IndexOf(BlockEndSign);
            return index >= 0;
        }

        public static System.Text.Encoding GetEncoder(ICodePageIndicator codePage)
        {
            try
            {
                if (codePage.CodePageCode < 0)
                {
                    return System.Text.Encoding.GetEncoding(codePage.CodePageName);
                }
                else
                {
                    return System.Text.Encoding.GetEncoding(codePage.CodePageCode);
                }
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                return System.Text.Encoding.UTF8;
            }
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

        public static SocketResult SendData(string ip, int port, string content)
        {
            SocketClientConfig config = new SocketClientConfig();
            config.IPAddress = ip;
            config.Port = port;

            return SendData(config, content);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static SocketResult SendData(SocketClientConfig config, string content)
        {
            SocketResult result = SocketResult.Empty;

            try
            {
                SocketLogMgt.SetLog("=============================================");
                SocketLogMgt.SetLog(": Send Data Begin.");
                SocketLogMgt.SetLog(config);

                string strSend = SocketHelper.PackMessageBlock(content);

                if (SocketLogMgt.DumpData)
                {
                    SocketLogMgt.SetLog(": Data to be sent.");
                    SocketLogMgt.SetLog("------------------------");
                    SocketLogMgt.SetLog(strSend);
                    SocketLogMgt.SetLog("------------------------");
                }

                Byte[] bytesSent = null;
                System.Text.Encoding encoder = SocketHelper.GetEncoder(config);
                if (encoder != null)
                {
                    bytesSent = encoder.GetBytes(strSend);
                }

                if (bytesSent == null)
                {
                    SocketLogMgt.SetLog(SocketLogType.Error, "Encode data failed.");
                    return SocketResult.SendFailed;
                }

                Byte[] bytesReceived = new Byte[config.ReceiveResponseBufferSizeKB * 1024];

                SocketLogMgt.SetLog(": Socket prepared.");
                SocketLogMgt.SetLog("------------------------");

                string strReceived = null;
                StringBuilder sb = new StringBuilder();
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(config.IPAddress), config.Port);
                using (Socket socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
                {
                    int id = SocketHelper.GetNewSocketID();
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

                    #region session 1

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
                        rec = !SocketHelper.FindBlockEnding(strReceived);
                    }

                    #endregion

                    #region session 2

                    //SocketLogMgt.SetLog(strID + "Send data.");
                    //socket.BeginSend(bytesSent, 0, bytesSent.Length, 0,
                    //    new AsyncCallback(OnDataSent), new SocketWrapper(strID, socket));

                    //rec = true;
                    //while (rec)
                    //{
                    //    if (!socket.Connected)
                    //    {
                    //        SocketLogMgt.SetLog(SocketLogType.Warning, strID + "Connection closed.");
                    //        break;
                    //    }

                    //    SocketLogMgt.SetLog(strID + "Receive data.");
                    //    int bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                    //    SocketLogMgt.SetLog(strID + "Receive succeeded. " + bytes.ToString() + " bytes.");
                    //    string str = encoder.GetString(bytesReceived, 0, bytes);
                    //    sb.Append(str);

                    //    strReceived = sb.ToString();
                    //    rec = !SocketHelper.FindBlockEnding(strReceived);
                    //}

                    #endregion

                    _allDone.WaitOne(); // need not to set timeout here, we can depend on socket timeout of the Receive() method.

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

                strReceived = SocketHelper.UnpackMessageBlock(strReceived);
                result = new SocketResult(SocketResultType.Success, strReceived);
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

        #endregion

        #region Log Handler

        private static ILog _log;

        public static void EnableSocketLogging(ILog log, bool dumpData)
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
