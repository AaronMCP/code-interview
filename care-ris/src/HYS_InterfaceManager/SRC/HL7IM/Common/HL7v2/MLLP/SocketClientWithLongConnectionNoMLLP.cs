using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Net;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketClientWithLongConnectionNoMLLP : IClient
    {
        public const string DEVICE_NAME = "NO_MLLP_MULTIPLE_SESSION_PER_CONNECTION_CLIENT";

        public readonly SocketClientConfig Config;
        public SocketClientWithLongConnectionNoMLLP(SocketClientConfig cfg)
        {
            Config = cfg;

            if (Config == null)
                throw new ArgumentNullException("The constructor of the class SocketClientWithLongConnectionNoMLLP does not accept null in the cfg parameter.");
        }
        public SocketClientWithLongConnectionNoMLLP()
            : this(new SocketClientConfig())
        {
        }

        private Socket _socket;
        private string _socketID;
        private Socket Connect(SocketClientConfig config)
        {
            if (_socket != null && _socket.Connected) return _socket;

            Disconnect();

            try
            {
                SocketLogMgt.SetLog(this, "=============================");
                SocketLogMgt.SetLog(this, "Opening connection...");
                SocketLogMgt.SetLog(this, config);

                _socketID = string.Format("({0}) ", SocketHelper.GetNewSocketID());

                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(config.IPAddress), config.Port);
                _socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _socket.ReceiveTimeout = config.ReceiveTimeout;
                _socket.SendTimeout = config.SendTimeout;
                _socket.Connect(ipe);

                if (_socket.Connected)
                {
                    SocketLogMgt.SetLog(this, _socketID + "Connection opened.");
                    return _socket;
                }
                else
                {
                    SocketLogMgt.SetLog(SocketLogType.Error, this, _socketID + "Connection failed.");
                    return null;
                }
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(this, err);
                return null;
            }
        }
        private void Disconnect()
        {
            if (_socket != null)
            {
                try
                {
                    SocketLogMgt.SetLog(this, _socketID + "Closing connection...");

                    _socket.Shutdown(SocketShutdown.Both);
                    _socket.Close();
                    _socket = null;

                    SocketLogMgt.SetLog(this, _socketID + "Connection closed.");
                    SocketLogMgt.SetLog(this, "=============================");
                }
                catch (Exception err)
                {
                    SocketLogMgt.SetLastError(this, err);
                }
            }
        }

        private System.Text.Encoding _encoder;
        private System.Text.Encoding GetEncoder()
        {
            if (_encoder == null) _encoder = SocketHelper.GetEncoder(Config);
            return _encoder;
        }
        private ManualResetEvent _allDone = new ManualResetEvent(false);
        private void OnDataSent(IAsyncResult asyn)
        {
            try
            {
                int bytesSent = _socket.EndSend(asyn);
                SocketLogMgt.SetLog(_socketID + "Sent succeeded. " + bytesSent.ToString() + " bytes.");

                _allDone.Set();
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(this, err);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private SocketResult Send(SocketClientConfig config, string content)
        {
            SocketLogMgt.SetLog(this, "=============================================");
            SocketLogMgt.SetLog(this, ": Send Data Begin.");

            try
            {
                Socket socket = Connect(config);
                if (socket == null) return SocketResult.Disconnect;     // failed to connect to remote server

                //string strSend = SocketHelper.PackMessageBlock(content);
                string strSend = content;

                if (SocketLogMgt.DumpData)
                {
                    SocketLogMgt.SetLog(this, ": Data to be sent.");
                    SocketLogMgt.SetLog(this, "------------------------");
                    SocketLogMgt.SetLog(this, strSend);
                    SocketLogMgt.SetLog(this, "------------------------");
                }

                Byte[] bytesSent = null;
                System.Text.Encoding encoder = GetEncoder();
                if (encoder != null) bytesSent = encoder.GetBytes(strSend);

                if (bytesSent == null)
                {
                    SocketLogMgt.SetLog(SocketLogType.Error, this, _socketID + "Encode data failed.");
                    return SocketResult.SendFailed;     // failed to encoding outgoing message
                }

                _allDone.Reset();
                SocketLogMgt.SetLog(this, _socketID + "Send data.");
                socket.BeginSend(bytesSent, 0, bytesSent.Length, 0, new AsyncCallback(OnDataSent), null);

                bool rec = true;
                string strReceived = null;
                StringBuilder sb = new StringBuilder();
                Byte[] bytesReceived = new Byte[config.ReceiveResponseBufferSizeKB * 1024];

                while (rec)
                {
                    if (!socket.Connected)
                    {
                        SocketLogMgt.SetLog(SocketLogType.Warning, this, _socketID + "Connection closed.");
                        break;
                    }

                    SocketLogMgt.SetLog(this, _socketID + "Receive data.");
                    int bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                    SocketLogMgt.SetLog(this, _socketID + "Receive succeeded. " + bytes.ToString() + " bytes.");
                    string str = encoder.GetString(bytesReceived, 0, bytes);
                    sb.Append(str);

                    strReceived = sb.ToString();
                    //rec = !SocketHelper.FindBlockEnding(strReceived);
                    rec = false;

                    // This socket client assume that there is a completed HL7 message (without MLLP signs) in one Receive();
                }

                _allDone.WaitOne(); // need not to set timeout here, we can depend on socket timeout of the Receive() method.

                if (SocketLogMgt.DumpData)
                {
                    SocketLogMgt.SetLog(this, ": Data received.");
                    SocketLogMgt.SetLog(this, "------------------------");
                    SocketLogMgt.SetLog(this, strReceived);
                    SocketLogMgt.SetLog(this, "------------------------");
                }

                //strReceived = SocketHelper.UnpackMessageBlock(strReceived);
                strReceived = SocketHelper.TrimMessageBlock(strReceived);
                return new SocketResult(SocketResultType.Success, strReceived);     // send and receive success
            }
            catch (SocketException se)
            {
                SocketLogMgt.SetLastError(this, se);
                SocketResult ret = new SocketResult(se);
                ret.Type = SocketResultType.Unknown;
                return ret;

                // meet exception during sending or receiving
                // (for example, it may be caused by the connection has expired [controled by the lower levels]
                // after some period of time without sending or receving),
                // and we need to recreate a new connection and try again,
                // please see SendData() for details.
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(this, err);
                return new SocketResult(err);   
            }
            finally
            {
                SocketLogMgt.SetLog(this, ": Send Data End");
                SocketLogMgt.SetLog(this, "=============================================\r\n");
            }
        }

        #region IClient Members

        public bool Open()
        {
            return Connect(Config) != null;
        }

        public SocketResult SendData(string content)
        {
            SocketResult r = Send(Config, content);
            if (r.Type != SocketResultType.Unknown) return r;
            return Send(Config, content);

            // the SocketResultType.Unknown has a special meaning here (see Send() for details)
            // which is allowed to retry one more time (to create a new connection) to send the message
        }

        public bool Close()
        {
            Disconnect();
            return true;
        }

        #endregion
    }
}
