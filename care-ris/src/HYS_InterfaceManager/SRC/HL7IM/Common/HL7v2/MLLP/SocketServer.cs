using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public class SocketServer : IServer
    {
        private Timer _timer;
        private Socket _listener;
        private SocketServerConfig _config;
        public SocketServerConfig Config
        {
            get { return _config; }
        }
        private SocketServer(Socket listener, SocketServerConfig config)
        {
            _listener = listener;
            _config = config;

            _timer = new Timer(_config.ConnectionCollectionInterval);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }

        public static IServer Create(int port)
        {
            try
            {
                SocketServerConfig cfg = new SocketServerConfig();
                cfg.Port = port;

                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketKeepAliveHelper.SetKeepAliveValues(cfg, listener, cfg);
                listener.Bind(new IPEndPoint(IPAddress.Any, port));
                return new SocketServer(listener, cfg);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                return null;
            }
        }
        public static IServer Create(string ip, int port)
        {
            if (ip == null || ip.Length < 1 || ip == "127.0.0.1") return Create(port);
            try
            {
                SocketServerConfig cfg = new SocketServerConfig();
                cfg.IPAddress = ip;
                cfg.Port = port;

                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketKeepAliveHelper.SetKeepAliveValues(cfg, listener, cfg);
                listener.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
                return new SocketServer(listener, cfg);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                return null;
            }
        }
        public static IServer Create(SocketServerConfig config)
        {
            try
            {
                int port = config.Port;
                string ip = config.IPAddress;

                Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketKeepAliveHelper.SetKeepAliveValues(config, listener, config);
                if (ip == null || ip.Length < 1 || ip == "127.0.0.1")
                {
                    listener.Bind(new IPEndPoint(IPAddress.Any, port));
                }
                else
                {
                    listener.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
                }
                return new SocketServer(listener, config);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
                return null;
            }
        }

        private System.Text.Encoding _encoder;
        internal System.Text.Encoding Encoder
        {
            get
            {
                if (_encoder == null) _encoder = SocketHelper.GetEncoder(_config);
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
            SocketLogMgt.SetLog(this, Config);

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

                ClearWorkers();
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
                ISocketWorker worker = SocketWorkerFactory.Create(id, socket, this);
                if (worker == null)
                {
                    SocketLogMgt.SetLog(SocketLogType.Error, this, "Create socket worker failed.");
                    return;
                }

                worker.Open();

                //_listener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (Exception err)
            {
                SocketLogMgt.SetLastError(err);
            }
            finally
            {
                _listener.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
        }
        internal void NotifyRequest(string receiveData, ref string sendData)
        {
            if (OnRequest != null) OnRequest(receiveData, ref sendData);
        }
        public event RequestEventHandler OnRequest;

        private List<ISocketWorker> _workers = new List<ISocketWorker>();
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();

            DisplayWorkers();

            SocketLogMgt.SetLog("Begin collect garbage connections.");

            DateTime dt = DateTime.Now;
            List<ISocketWorker> gcList = new List<ISocketWorker>();
            
            lock (_workers)
            {
                foreach (ISocketWorker sw in _workers)
                {
                    TimeSpan ts = dt.Subtract(sw.ActiveDT);
                    if (ts.TotalSeconds >= _config.ConnectionTimeoutSecond) gcList.Add(sw);
                }
            }

            foreach (ISocketWorker sw in gcList)
            {
                SocketLogMgt.SetLog(SocketLogType.Warning, string.Format("Collect garbage connection: ({0})", sw.Caption));
                sw.Close();
            }

            SocketLogMgt.SetLog("End collect garbage connections.");

            DisplayWorkers();

            _timer.Start();
        }
        private void DisplayWorkers()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Workers List: ");

            lock (_workers)
            {
                foreach (ISocketWorker sw in _workers) sb.Append(sw.Caption).Append(" ");
            }

            SocketLogMgt.SetLog(sb.ToString());
        }
        private void ClearWorkers()
        {
            SocketLogMgt.SetLog("Begin clear all connections.");

            List<ISocketWorker> gcList = new List<ISocketWorker>();
            lock (_workers)
            {
                foreach (ISocketWorker sw in _workers) gcList.Add(sw);
            }

            foreach (ISocketWorker sw in gcList)
            {
                SocketLogMgt.SetLog(SocketLogType.Warning, string.Format("Clear connection: ({0})", sw.Caption));
                sw.Close();
            }

            SocketLogMgt.SetLog("End clear all connections.");
        }

        internal void AddWorker(ISocketWorker worker)
        {
            lock (_workers)
            {
                _workers.Add(worker);
            }
            //DisplayWorkers();
        }
        internal void RemoveWorker(ISocketWorker worker)
        {
            lock (_workers)
            {
                _workers.Remove(worker);
            }
            //DisplayWorkers();
        }
    }


}