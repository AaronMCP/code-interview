using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    /// <summary>
    /// "BreakPoint" means the client may send one part of message in on connection and another part in another connection.
    /// </summary>
    [Obsolete("This class has not been finished yet.", true)]
    public class SocketWorkerWithBreakPointSupport : ISocketWorker
    {
        public const string DEVICE_NAME = "BREAKPOINT_WORKER";

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
        internal SocketWorkerWithBreakPointSupport(int id, Socket socket, SocketServer server)
        {
            _server = server;
            _socket = socket;
            UpdateActiveDT();
            _strSocketID = id.ToString();
            _remoteEndPointInfo = _socket.RemoteEndPoint.ToString();
            _buffer = new byte[_server.Config.ReceiveRequestBufferSizeKB * 1024];
        }

        private void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                if (_isClosed)
                {
                    SocketLogMgt.SetLog(SocketLogType.Warning, this, "Data received, but the socket is closed.");
                    return;
                }

                int bytesRead = _socket.EndReceive(asyn);

                ////------------Handle Multi-byte Charactor-------------
                ////Buffer all the bytes received and then decode this bytes at one time.
                ////in order to support multi byte charactor set (for example: utf-8)
                ////However, ASCII or GB or BIG5 or Unicode are easy,
                ////we can decode even bytes at each time.
                //List<BufferWrapper> bufferList = new List<BufferWrapper>();
                ////----------------------------------------------------

                do
                {
                    bytesRead = _socket.Receive(_buffer);
                    SocketLogMgt.SetLog(this, _strSocketID + "Receive succeeded. " + bytesRead.ToString() + " bytes.");

                    UpdateActiveDT();

                    ////------------Handle Multi-byte Charactor-------------
                    //bufferList.Add(BufferWrapper.Create(_buffer, bytesRead));
                    ////foreach (BufferWrapper w in bufferList) SocketLogMgt.SetLog(">>>>>>>>>>>> " + w.Buffer.Length.ToString());
                    ////----------------------------------------------------

                    if (bytesRead > 0)
                    {
                        ////No matter how to cut the buffer, ASCII charactor can always be decoded properly,
                        ////therefore we can use this segment to detect whether an ASCII charactor ending sign (for example "</XMLRequestMessage>") is received.
                        ////See class XmlTest.FormCoding for unit test.
                        //string str = _server.Encoder.GetString(_buffer, 0, bytesRead);
                        //_sb.Append(str);

                        //if (SocketLogMgt.DumpData)
                        //{
                        //    SocketLogMgt.SetLog(this, _strSocketID + ": Data received.");
                        //    SocketLogMgt.SetLog(this, "------------------------");
                        //    SocketLogMgt.SetLog(this, str);
                        //    SocketLogMgt.SetLog(this, "------------------------");
                        //}

                        //string receiveData = _sb.ToString();
                        //if (SocketHelper.FindBlockEnding(receiveData))
                        //{
                        //    string sendData = null;

                        //    //------------Handle Multi-byte Charactor-------------
                        //    receiveData = BufferWrapper.GetString(_server.Encoder, bufferList);
                        //    bufferList.Clear();
                        //    //----------------------------------------------------

                        //    receiveData = SocketHelper.UnpackMessageBlock(receiveData);
                        //    _server.NotifyRequest(receiveData, ref sendData);
                        //    ResponseData(sendData);

                        //    // if allow multiple session per connection, then continue receiving till connection is closed by the client.
                        //    if (_server.Config.AllowMultipleSessionPerConnection)
                        //        continue;
                        //    else
                        //        break;
                        //}
                    }
                }
                while (bytesRead > 0);

                // if(bytesRead==0) which means the connection is closed by the client, we will close the worker socket.
                SocketLogMgt.SetLog(this, "Stop receiving data because the connection is closed by the client.");
                CloseWorker();
            }
            catch (SocketException err)
            {
                //needToContinue = false;
                SocketLogMgt.SetLastError(err);
                // if client disconnected or other network exception when receiving/processing data, then close the server socket.
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
                SocketLogMgt.SetLog(this, _strSocketID + "Sent succeeded. " + bytesSent.ToString() + " bytes.");

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
            get { throw new NotImplementedException(); }
        }

        public DateTime ActiveDT
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
