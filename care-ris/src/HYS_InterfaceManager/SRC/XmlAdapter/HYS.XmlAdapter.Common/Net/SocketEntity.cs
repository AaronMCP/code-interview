using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Text;

namespace HYS.XmlAdapter.Common.Net
{
    public class SocketEntity : IEntity
    {
        private Timer _timer;
        private Socket _listener;
        private SocketConfig _config;
        public SocketConfig Config
        {
            get { return _config; }
        }
        private SocketEntity(Socket listener, SocketConfig config)
        {
            _listener = listener;
            _config = config;

            _timer = new Timer(_config.ConnectionCollectionInterval);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }

        public static SocketEntity Create(int port)
        {
            try
            {
                SocketConfig cfg = new SocketConfig();
                cfg.Port = port;

                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(IPAddress.Any, port));
                return new SocketEntity(listener, cfg);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                return null;
            }
        }
        public static SocketEntity Create(string ip, int port)
        {
            if (ip == null || ip.Length < 1 || ip == "127.0.0.1") return Create(port);
            try
            {
                SocketConfig cfg = new SocketConfig();
                cfg.IPAddress = ip;
                cfg.Port = port;

                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                listener.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
                return new SocketEntity(listener, cfg);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                return null;
            }
        }
        public static SocketEntity Create(SocketConfig config)
        {
            try
            {
                int port = config.Port;
                string ip = config.IPAddress;

                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (ip == null || ip.Length < 1 || ip == "127.0.0.1")
                {
                    listener.Bind(new IPEndPoint(IPAddress.Any, port));
                }
                else
                {
                    listener.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
                }
                return new SocketEntity(listener, config);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                return null;
            }
        }

        private Encoding _encoder;
        internal Encoding Encoder
        {
            get
            {
                if (_encoder == null) _encoder = SocketHelper.GetEncoder(_config.CodePageName);
                return _encoder;
            }
        }

        private bool _isListening;
        public bool IsListening
        {
            get { return _isListening; }
        }
        public bool Start()
        {
            bool result;
            SocketLogMgt.SetLog(this, "=============================================");
            SocketLogMgt.SetLog(this, ": Start Socket Listening.");

            try
            {
                if (_config.EnableConnectionCollecting)
                    _timer.Start();

                _listener.SendTimeout = _config.SendTimeout;
                _listener.ReceiveTimeout = _config.ReceiveTimeout;
                _listener.Listen(_config.BackLog);
                _listener.BeginAccept(new AsyncCallback(OnClientConnect), null);

                _isListening = true;
                result = true;
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                _isListening = false;
                result = false;
            }

            SocketLogMgt.SetLog(this, ": Start Socket Listening Result: " + result.ToString());
            SocketLogMgt.SetLog(this, "=============================================\r\n");
            return result;
        }
        public bool Stop()
        {
            bool result;
            SocketLogMgt.SetLog(this, "=============================================");
            SocketLogMgt.SetLog(this, ": Stop Socket Listening.");

            try
            {
                if (_timer.Enabled)
                    _timer.Stop();

                _isListening = false;
                _listener.Close();
                result = true;
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                result = false;
            }

            SocketLogMgt.SetLog(this, ": Stop Socket Listening Result: " + result.ToString());
            SocketLogMgt.SetLog(this, "=============================================\r\n");
            return result;
        }

        private void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                if (!_isListening) return;
                Socket socket = _listener.EndAccept(asyn);
                if (socket == null) return;

                int id = SocketHelper.GetNewSocketID();

                SocketLogMgt.SetLog(this, "=============================================");
                SocketLogMgt.SetLog(this, ": Client Connected.");
                SocketLogMgt.SetLog(this, ": Worker Socket Created. ID: " + id.ToString());

                //socket.NoDelay = true;
                SocketWorker worker = new SocketWorker(id, socket, this);
                worker.ReceiveData();

                _listener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
            }
        }
        internal void NotifyRequest(string receiveData, ref string sendData)
        {
            if (OnRequest != null) OnRequest(receiveData, ref sendData);
        }
        public event RequestEventHandler OnRequest;

        public List<SocketWorker> Workers = new List<SocketWorker>();
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            DisplayWorker();

            DateTime dt = DateTime.Now;
            List<SocketWorker> gcList = new List<SocketWorker>();
            foreach (SocketWorker sw in Workers)
            {
                //if (sw._socket.Connected) continue;
                TimeSpan ts = dt.Subtract(sw.CreateDT);
                if (ts.Seconds >= _config.ConnectionTimeoutSecond) gcList.Add(sw);
            }

            foreach (SocketWorker sw in gcList)
            {
                try
                {
                    SocketLogMgt.SetLog(SocketLogType.Warning, "Collect Garbage Connection: (" + sw.LogID + ") ");
                    sw._socket.Shutdown(SocketShutdown.Both);
                    sw._socket.Close();
                }
                catch (Exception err)
                {
                    SocketLogMgt.SetLastError(err);
                }
                finally
                {
                    //put it into .Net GC
                    Workers.Remove(sw);
                }
            }

            DisplayWorker();

            _timer.Start();
        }
        private void DisplayWorker()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Worker List: ");
            foreach (SocketWorker sw in Workers) sb.Append(sw.LogID);
            SocketLogMgt.SetLog(sb.ToString());
        }

        internal void AddWorker(SocketWorker worker)
        {
            Workers.Add(worker);
            //DisplayWorker();
        }
        internal void RemoveWorker(SocketWorker worker)
        {
            Workers.Remove(worker);
            //DisplayWorker();
        }
    }

    
}
