using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketWorkerWithLongConnection2 : ISocketWorker
    {
        public const string DEVICE_NAME = "MULTIPLE_SESSION_WORKER2";

        private DateTime _activeDT;
        private void UpdateActiveDT()
        {
            _activeDT = DateTime.Now;
        }

        private byte[] _buffer;
        private readonly Socket _socket;
        private readonly SocketServer _server;
        private readonly string _strSocketID;
        private readonly string _remoteEndPointInfo;
        private StringBuilder _sb = new StringBuilder();
        internal SocketWorkerWithLongConnection2(int id, Socket socket, SocketServer server)
        {
            _server = server;
            _socket = socket;
            UpdateActiveDT();
            _strSocketID = id.ToString();
            _remoteEndPointInfo = _socket.RemoteEndPoint.ToString();
            _buffer = new byte[_server.Config.ReceiveRequestBufferSizeKB * 1024];

            SocketKeepAliveHelper.SetKeepAliveValues(this, _socket, _server.Config);
        }

        private class BufferWrapper
        {
            public static string GetString(System.Text.Encoding ecoder, List<BufferWrapper> list)
            {
                int totallength = 0;
                // what if this integer overflows...

                foreach (BufferWrapper w in list) totallength += w.Buffer.Length;
                byte[] bList = new byte[totallength];

                int clength = 0;
                foreach (BufferWrapper w in list)
                {
                    //SocketLogMgt.SetLog("<<<<<<<<<<<<< " + w.Buffer.Length.ToString() + " -> " + clength.ToString());
                    w.Buffer.CopyTo(bList, clength);
                    clength += w.Buffer.Length;
                }

                return ecoder.GetString(bList, 0, totallength);
            }
            public static BufferWrapper Create(byte[] buffer, int length)
            {
                byte[] bList = new byte[length];
                for (int i = 0; i < length; i++) bList[i] = buffer[i];
                return new BufferWrapper(bList);
            }
            private BufferWrapper(byte[] bytes)
            {
                Buffer = bytes;
            }
            public readonly byte[] Buffer;
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                if (_isClosed)
                {
                    SocketLogMgt.SetLog(SocketLogType.Warning, this, _strSocketID + ": Data received, but the socket is closed.");
                    return;
                }

                int bytesRead = _socket.EndReceive(asyn);

                //------------Handle Multi-byte Charactor-------------
                //Buffer all the bytes received and then decode this bytes at one time.
                //in order to support multi byte charactor set (for example: utf-8)
                //However, ASCII or GB or BIG5 or Unicode are easy,
                //we can decode even bytes at each time.
                List<BufferWrapper> bufferList = new List<BufferWrapper>();
                int startIndex = 0, endIndex = 0;
                //----------------------------------------------------

                do
                {
                    bytesRead = _socket.Receive(_buffer);
                    SocketLogMgt.SetLog(this, _strSocketID + ": Receive succeeded. " + bytesRead.ToString() + " bytes.");
                    
                    UpdateActiveDT();

                    if (bytesRead > 0)
                    {
                        //------------Handle Multi-byte Charactor-------------
                        bufferList.Add(BufferWrapper.Create(_buffer, bytesRead));
                        //foreach (BufferWrapper w in bufferList) SocketLogMgt.SetLog(">>>>>>>>>>>> " + w.Buffer.Length.ToString());
                        //----------------------------------------------------

                        ////No matter how to cut the buffer, ASCII charactor can always be decoded properly,
                        ////therefore we can use this segment to detect whether an ASCII charactor ending sign (for example "</XMLRequestMessage>") is received.
                        ////See class XmlTest.FormCoding for unit test.
                        string str = _server.Encoder.GetString(_buffer, 0, bytesRead);
                        _sb.Append(str);

                        if (SocketLogMgt.DumpData)
                        {
                            SocketLogMgt.SetLog(this, _strSocketID + ": Data received.");
                            SocketLogMgt.SetLog(this, "------------------------");
                            SocketLogMgt.SetLog(this, str);
                            SocketLogMgt.SetLog(this, "------------------------");
                        }

                        string receiveData = _sb.ToString();
                        if (!SocketHelper.FindBlockEnding(receiveData)) continue;

                        //------------Handle Multi-byte Charactor-------------
                        receiveData = BufferWrapper.GetString(_server.Encoder, bufferList);
                        //----------------------------------------------------

                        while (0 <= startIndex && startIndex < receiveData.Length)
                        {
                            endIndex = receiveData.IndexOf(SocketHelper.BlockEndSign, startIndex);
                            if (endIndex < 0) continue;

                            string sendMsg = null;
                            string receiveMsg = SocketHelper.UnpackMessageBlock(receiveData, startIndex);

                            if (SocketLogMgt.DumpData)
                            {
                                SocketLogMgt.SetLog(this, _strSocketID + ": Begin processing message:");
                                SocketLogMgt.SetLog(this, "------------------------");
                                SocketLogMgt.SetLog(this, receiveMsg);
                                SocketLogMgt.SetLog(this, "------------------------");
                            }

                            _server.NotifyRequest(receiveMsg, ref sendMsg);
                            ResponseData(sendMsg);

                            startIndex = endIndex + SocketHelper.BlockEndSign.Length;
                        }
                    }
                }
                while (bytesRead > 0);

                SocketLogMgt.SetLog(this, _strSocketID + ": Receiving 0 byte, do nothing and wait till exception or garbage collection to close the socket.");

                //// if(bytesRead==0) which means the connection is closed by the client, we will close the worker socket.
                //SocketLogMgt.SetLog(this, "Stop receiving data because the connection is closed by the client.");
                //CloseWorker();
            }
            catch (SocketException err)
            {
                //needToContinue = false;
                SocketLogMgt.SetLastError(err);
                // if client does not send data in some period of time and the connection will be expired 
                // (controled by the lower levels, and this exception occurs when the client send message again after the connection expired)
                // or other network exception when receiving data, then close the server socket.
                CloseWorker();
            }
            catch (Exception e)
            {
                SocketLogMgt.SetLastError(e);
                // if communication is ok, but process message failed, do not need to close connection.
            }
        }
        private void OnDataSent(IAsyncResult asyn)
        {
            try
            {
                if (_isClosed)
                {
                    SocketLogMgt.SetLog(SocketLogType.Warning, this, "Data sent, but the socket is closed.");
                    return;
                }

                int bytesSent = _socket.EndSend(asyn);
                SocketLogMgt.SetLog(this, _strSocketID + ": Sent succeeded. " + bytesSent.ToString() + " bytes.");

                UpdateActiveDT();

                //// if not allow multiple session per connection, then close server socket after the first acknowledgement is sent.
                //if (!_server.Config.AllowMultipleSessionPerConnection) Close();
            }
            catch (SocketException err)
            {
                SocketLogMgt.SetLastError(err);
                // if client disconnected or other network exception when sending data, then close the server socket.
                CloseWorker();
            }
            catch (Exception e)
            {
                SocketLogMgt.SetLastError(e);
                // if communication is ok, but process message failed, do not need to close connection.
            }
        }

        private void ResponseData(string sendData)
        {
            string strSend = SocketHelper.PackMessageBlock(sendData);

            if (SocketLogMgt.DumpData)
            {
                SocketLogMgt.SetLog(this, _strSocketID + ": Data to be sent.");
                SocketLogMgt.SetLog(this, "------------------------");
                SocketLogMgt.SetLog(this, strSend);
                SocketLogMgt.SetLog(this, "------------------------");
            }

            byte[] byteData = null;
            byteData = _server.Encoder.GetBytes(strSend);

            if (byteData == null)
            {
                SocketLogMgt.SetLog(SocketLogType.Error, this, "Encode data failed.");
            }
            else
            {
                SocketLogMgt.SetLog(this, _strSocketID + ": Begin sending data.");
                _socket.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(OnDataSent), null);
            }
        }
        private void ReceiveData()
        {
            SocketLogMgt.SetLog(this, _strSocketID + ": Begin receiving data.");
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.Peek,
                    new AsyncCallback(OnDataReceived), null);
        }

        private bool _isClosed; // for performance consideration, do not lock this field currently.
        private void OpenWorker()
        {
            try
            {
                _server.AddWorker(this);

                SocketLogMgt.SetLog(this, "=============================================");
                SocketLogMgt.SetLog(this, ": Client Connected from: " + _remoteEndPointInfo);
                SocketLogMgt.SetLog(this, ": Worker Socket Created. ID: " + _strSocketID);

                ReceiveData();
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
            }
        }
        private void CloseWorker()
        {
            try
            {
                _isClosed = true;

                SocketLogMgt.SetLog(this, ": Closing connection..");

                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
            }
            finally
            {
                _server.RemoveWorker(this);

                SocketLogMgt.SetLog(this, ": Client Disconnected from: " + _remoteEndPointInfo);
                SocketLogMgt.SetLog(this, "=============================================\r\n");
            }
        }

        #region ISocketWorker Members

        public void Open()
        {
            OpenWorker();
        }

        public void Close()
        {
            CloseWorker();
        }

        public string Caption
        {
            get { return _strSocketID; }
        }

        public DateTime ActiveDT
        {
            get { return _activeDT; }
        }

        #endregion
    }
}
