using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketWorker : ISocketWorker
    {
        public const string DEVICE_NAME = "SINGLE_SESSION_WORKER";

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
        internal SocketWorker(int id, Socket socket, SocketServer server)
        {
            _server = server;
            _socket = socket;
            UpdateActiveDT();
            _strSocketID = id.ToString();
            _remoteEndPointInfo = _socket.RemoteEndPoint.ToString();
            _buffer = new byte[_server.Config.ReceiveRequestBufferSizeKB * 1024];
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
                int bytesRead = _socket.EndReceive(asyn);

                //------------Handle Multi-byte Charactor-------------
                //Buffer all the bytes received and then decode this bytes at one time.
                //in order to support multi byte charactor set (for example: utf-8)
                //However, ASCII or GB or BIG5 or Unicode are easy,
                //we can decode even bytes at each time.
                List<BufferWrapper> bufferList = new List<BufferWrapper>();
                //----------------------------------------------------

                do
                {
                    bytesRead = _socket.Receive(_buffer);
                    SocketLogMgt.SetLog(this, _strSocketID + "Receive succeeded. " + bytesRead.ToString() + " bytes.");
                    
                    UpdateActiveDT();

                    //------------Handle Multi-byte Charactor-------------
                    bufferList.Add(BufferWrapper.Create(_buffer, bytesRead));
                    //foreach (BufferWrapper w in bufferList) SocketLogMgt.SetLog(">>>>>>>>>>>> " + w.Buffer.Length.ToString());
                    //----------------------------------------------------

                    if (bytesRead > 0)
                    {
                        //No matter how to cut the buffer, ASCII charactor can always be decoded properly,
                        //therefore we can use this segment to detect whether an ASCII charactor ending sign (for example "</XMLRequestMessage>") is received.
                        //See class XmlTest.FormCoding for unit test.
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
                        if (SocketHelper.FindBlockEnding(receiveData))
                        {
                            string sendData = null;

                            //------------Handle Multi-byte Charactor-------------
                            receiveData = BufferWrapper.GetString(_server.Encoder, bufferList);
                            bufferList.Clear();
                            //----------------------------------------------------

                            receiveData = SocketHelper.UnpackMessageBlock(receiveData);
                            _server.NotifyRequest(receiveData, ref sendData);
                            ResponseData(sendData);

                            break;
                        }
                    }
                }
                while (bytesRead > 0);
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
                int bytesSent = _socket.EndSend(asyn);
                SocketLogMgt.SetLog(this, _strSocketID + "Sent succeeded. " + bytesSent.ToString() + " bytes.");

                UpdateActiveDT();
            }
            catch (Exception e)
            {
                SocketLogMgt.SetLastError(e);
                // if communication is ok, but process message failed, do not need to close connection.
            }
            finally
            {
                CloseWorker();
                // close socket only when send data finished
                // however if you cose the socket after receive data processing finished
                // (which means the async sending is began), it seams the data can also be succussfully sent,
                // but in this case, we cannot access the socket object after send data finished.
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
                SocketLogMgt.SetLog(this, _strSocketID + "Begin sending data.");
                _socket.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(OnDataSent), null);
            }
        }
        private void ReceiveData()
        {
            SocketLogMgt.SetLog(this, _strSocketID + "Begin receiving data.");
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.Peek,
                    new AsyncCallback(OnDataReceived), null);
        }

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
