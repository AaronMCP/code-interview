using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;

namespace HYS.XmlAdapter.Common.Net
{
    public class SocketWorker
    {
        private int _id;
        private string _logID;
        public string LogID
        {
            get { return _logID; }
        }
        internal DateTime CreateDT;

        internal Socket _socket;
        private SocketEntity _entity;
        internal SocketWorker(int id, Socket socket, SocketEntity entity)
        {
            _id = id;
            _socket = socket;
            _entity = entity;
            _logID = "(" + id.ToString() + ") ";
            CreateDT = DateTime.Now;
        }

        //Buffer all the bytes received and then decode this bytes at one time.
        //in order to support multi byte charactor set (for example: utf-8)
        //However, ASCII or GB or BIG5 or Unicode are easy,
        //we can decode even bytes at each time.
        private List<BufferWrapper> _bufferList = new List<BufferWrapper>();
        private class BufferWrapper
        {
            public static string GetString(Encoding ecoder, List<BufferWrapper> list)
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

        private byte[] _buffer = new byte[SocketHelper.ReceiveRequestBufferSize];
        private StringBuilder _sb = new StringBuilder();

        private void OnDataReceived(IAsyncResult asyn)
        {
            int bytesRead = _socket.EndReceive(asyn);
            
            //------------Handle Multi-byte Charactor-------------
            _bufferList = new List<BufferWrapper>();
            //----------------------------------------------------

            do
            {
                bytesRead = _socket.Receive(_buffer);
                SocketLogMgt.SetLog(this, _logID + "Receive succeeded. " + bytesRead.ToString() + " bytes.");

                //------------Handle Multi-byte Charactor-------------
                _bufferList.Add(BufferWrapper.Create(_buffer, bytesRead));
                //foreach (BufferWrapper w in _bufferList) SocketLogMgt.SetLog(">>>>>>>>>>>> " + w.Buffer.Length.ToString());
                //----------------------------------------------------

                if (bytesRead > 0)
                {
                    //No matter how to cut the buffer, ASCII charactor can always be decoded properly,
                    //therefore we can use this segment to detect whether an ending sign (for example "</XMLRequestMessage>") is received.
                    //See class XmlTest.FormCoding for unit test.
                    string str = _entity.Encoder.GetString(_buffer, 0, bytesRead);
                    _sb.Append(str);

                    if (SocketLogMgt.DumpData)
                    {
                        SocketLogMgt.SetLog(this, _logID + ": Data received.");
                        SocketLogMgt.SetLog(this, "------------------------");
                        SocketLogMgt.SetLog(this, str);
                        SocketLogMgt.SetLog(this, "------------------------");
                    }

                    string receiveData = _sb.ToString();
                    if (SocketHelper.IsEOF(receiveData, _entity.Config.ReceiveEndSign))
                    {
                        string sendData = null;

                        //------------Handle Multi-byte Charactor-------------
                        receiveData = BufferWrapper.GetString(_entity.Encoder, _bufferList);
                        _bufferList = null;
                        //----------------------------------------------------

                        _entity.NotifyRequest(receiveData, ref sendData);
                        ResponseData(sendData);
                        break;
                    }
                }
            }
            while (bytesRead > 0);
        }
        private void OnDataSent(IAsyncResult asyn)
        {
            try
            {
                int bytesSent = _socket.EndSend(asyn);
                SocketLogMgt.SetLog(this, _logID + "Sent succeeded. " + bytesSent.ToString() + " bytes.");

                _socket.Shutdown(SocketShutdown.Both);
                _socket.Close();

                _entity.RemoveWorker(this);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
            }

            SocketLogMgt.SetLog(this, ": Client Disconnected.");
            SocketLogMgt.SetLog(this, "=============================================\r\n");
        }

        private void ResponseData(string sendData)
        {
            string strSend = SocketHelper.EnsureEOF(sendData, _entity.Config.SendEndSign);

            if (SocketLogMgt.DumpData)
            {
                SocketLogMgt.SetLog(this, _logID + ": Data to be sent.");
                SocketLogMgt.SetLog(this, "------------------------");
                SocketLogMgt.SetLog(this, strSend);
                SocketLogMgt.SetLog(this, "------------------------");
            }

            byte[] byteData = null;
            if (_entity.Config.IncludeHeader)
            {
                byteData = SocketHelper.GetResponseByteWithHeader(_entity.Encoder, strSend, _entity.Config);
            }
            else
            {
                byteData = _entity.Encoder.GetBytes(strSend);
            }

            if (byteData == null)
            {
                SocketLogMgt.SetLog(SocketLogType.Error, "Encode data failed.");
            }
            else
            {
                _socket.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(OnDataSent), null);
            }
        }
        public void ReceiveData()
        {
            _entity.AddWorker(this);
            SocketLogMgt.SetLog(this, _logID + "Begin receive data.");

            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.Peek,
                    new AsyncCallback(OnDataReceived), null);
        }
    }
}
