using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketClient : IClient
    {
        public const string DEVICE_NAME = "ONE_SESSION_PER_CONNECTION_CLIENT";

        public readonly SocketClientConfig Config;
        public SocketClient(SocketClientConfig cfg)
        {
            Config = cfg;

            if (Config == null)
                throw new ArgumentNullException("The constructor of the class SocketClient does not accept null in the cfg parameter.");
        }
        public SocketClient()
            : this(new SocketClientConfig())
        {
        }

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
        private ManualResetEvent _allDone = new ManualResetEvent(false);
        private void OnDataSent(IAsyncResult asyn)
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
                SocketLogMgt.SetLastError(this, err);
            }
        }

        public SocketResult SendData(string ip, int port, string content)
        {
            SocketClientConfig config = new SocketClientConfig();
            config.IPAddress = ip;
            config.Port = port;

            return SendData(config, content);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public SocketResult SendData(SocketClientConfig config, string content)
        {
            SocketResult result = SocketResult.Empty;

            try
            {
                SocketLogMgt.SetLog(this, "=============================================");
                SocketLogMgt.SetLog(this, ": Send Data Begin.");
                SocketLogMgt.SetLog(this, config);

                string strSend = SocketHelper.PackMessageBlock(content);

                if (SocketLogMgt.DumpData)
                {
                    SocketLogMgt.SetLog(this, ": Data to be sent.");
                    SocketLogMgt.SetLog(this, "------------------------");
                    SocketLogMgt.SetLog(this, strSend);
                    SocketLogMgt.SetLog(this, "------------------------");
                }

                Byte[] bytesSent = null;
                System.Text.Encoding encoder = SocketHelper.GetEncoder(config);
                if (encoder != null)
                {
                    bytesSent = encoder.GetBytes(strSend);
                }

                if (bytesSent == null)
                {
                    SocketLogMgt.SetLog(SocketLogType.Error, this, "Encode data failed.");
                    return SocketResult.SendFailed;
                }

                Byte[] bytesReceived = new Byte[config.ReceiveResponseBufferSizeKB * 1024];

                SocketLogMgt.SetLog(this, ": Socket prepared.");
                SocketLogMgt.SetLog(this, "------------------------");

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

                    SocketLogMgt.SetLog(this, strID + "Socket created.");
                    bool flag = true;
                    while (flag)
                    {
                        try
                        {
                            socket.Connect(ipe);
                            flag = false;
                        }
                        catch (Exception e)
                        {
                            SocketLogMgt.SetLog(this, strID + "try to connect socket");
                            Thread.Sleep(5000);
                        }
                    }
                    if (socket.Connected)
                    {
                        SocketLogMgt.SetLog(this, strID + "Socket connected.");
                    }
                    else
                    {
                        SocketLogMgt.SetLog(SocketLogType.Warning, this, strID + "Connection failed.");
                        return SocketResult.Disconnect;
                    }

                    _allDone.Reset();
                    SocketLogMgt.SetLog(this, strID + "Send data.");
                    socket.BeginSend(bytesSent, 0, bytesSent.Length, 0,
                        new AsyncCallback(OnDataSent), new SocketWrapper(strID, socket));

                    bool rec = true;
                    while (rec)
                    {
                        if (!socket.Connected)
                        {
                            SocketLogMgt.SetLog(SocketLogType.Warning, this, strID + "Connection closed.");
                            break;
                        }

                        SocketLogMgt.SetLog(this, strID + "Receive data.");
                        int bytes = socket.Receive(bytesReceived, bytesReceived.Length, 0);
                        SocketLogMgt.SetLog(this, strID + "Receive succeeded. " + bytes.ToString() + " bytes.");
                        string str = encoder.GetString(bytesReceived, 0, bytes);
                        sb.Append(str);

                        strReceived = sb.ToString();
                        rec = !SocketHelper.FindBlockEnding(strReceived);
                    }

                    _allDone.WaitOne(); // need not to set timeout here, we can depend on socket timeout of the Receive() method.

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();

                    SocketLogMgt.SetLog(this, strID + "Socket disconnected.");
                }

                SocketLogMgt.SetLog(this, "------------------------");

                if (SocketLogMgt.DumpData)
                {
                    SocketLogMgt.SetLog(this, ": Data received.");
                    SocketLogMgt.SetLog(this, "------------------------");
                    SocketLogMgt.SetLog(this, strReceived);
                    SocketLogMgt.SetLog(this, "------------------------");
                }

                strReceived = SocketHelper.UnpackMessageBlock(strReceived);
                result = new SocketResult(SocketResultType.Success, strReceived);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(this, err);
                result = new SocketResult(err);
            }

            SocketLogMgt.SetLog(this, ": Send Data End");
            SocketLogMgt.SetLog(this, "=============================================\r\n");
            return result;
        }

        #region IClient Members

        public bool Open()
        {
            return true;
        }

        public bool Close()
        {
            return true;
        }

        public SocketResult SendData(string content)
        {
            return SendData(Config, content);
        }

        #endregion
    }
}
