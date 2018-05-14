using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using HYS.IM.Common.WCFHelper;
using System.ServiceModel;
using HYS.IM.Common.WCFHelper.Configurability;
using HYS.Common.Objects.Logging;
using System.IO;

namespace HYS.Common.Soap
{
    [ServiceContract]
    public interface IAbstractServerContract
    {
        [OperationContract(Action = "ProcessMessage", ReplyAction = "*")]
        Message ProcessMessage(Message request);
    }

    public class AbstractService : IAbstractServerContract
    {
        private readonly SOAPServer _server;
        public AbstractService()
        {
            ConfigurableServiceHost host = OperationContext.Current.Host as ConfigurableServiceHost;
            if (host == null)
                throw new ArithmeticException("Cannot find ConfigurableServiceHost in current operation context.");

            _server = host.Tag as SOAPServer;
            if (_server == null)
                throw new ArithmeticException("Cannot find SOAPReceiver in current operation context.");
        }

        public Message ProcessMessage(Message request)
        {
            try
            {
                string req = SoapMessageHelper.DumpWCFMessage(request);
                if (req == null || req.Length < 1) return null;

                string rsp = null;
                if (!_server.NotifyMessageReceived(req, out rsp) ||
                    string.IsNullOrEmpty (rsp))
                {
                    _server._log.Write("Preparing SOAP error message from file: " + _server._soapErrFile);
                    rsp = _server._soapErrEnvelope;
                }

                Message msg = SoapMessageHelper.CreateWCFMessageWithEnvelope(
                    SoapEnvelopeVersion.Soap11,
                    WSAddressingVersion.None,
                    "*", rsp);

                return msg;
            }
            catch (Exception e)
            {
                _server._log.Write(e);
                return null;
            }
        }
    }

    public delegate bool ReceiveSOAPMessageHandler(string requestSOAPEnvelope, out string responseSOAPEnvelope);
    
    public class SOAPServer
    {
        internal readonly ILogging _log;
        internal readonly string _soapErrEnvelope;
        internal readonly string _soapErrFile;
        private readonly string _configFile;
        private ServiceHost _host;

        public SOAPServer(string cfgFile, string soapErrFile, ILogging log)
        {
            _log = log;
            _configFile = cfgFile;
            _soapErrFile = soapErrFile;

            try
            {
                _soapErrEnvelope = File.ReadAllText(_soapErrFile);
            }
            catch (Exception err)
            {
                log.Write(err);
            }
        }

        public event ReceiveSOAPMessageHandler OnMessageReceived;
        internal bool NotifyMessageReceived(string requestSOAPEnvelope, out string responseSOAPEnvelope)
        {
            responseSOAPEnvelope = null;
            if (OnMessageReceived == null) return false;
            return OnMessageReceived(requestSOAPEnvelope, out responseSOAPEnvelope);
        }

        public bool Start(string uri)
        {
            bool res = false;
            if (uri == null || uri.Length < 1) return res;
            _log.Write(string.Format("Begin starting SOAP server on {0}.", uri));

            Stop();

            try
            {
                _host = new ConfigurableServiceHost(_configFile, typeof(AbstractService), new Uri(uri));
                ((ConfigurableServiceHost)_host).Tag = this;
                _host.Open();
                res = true;
            }
            catch (Exception e)
            {
                _log.Write(e);
            }

            _log.Write(string.Format("End starting SOAP server on {0}. Result: {1}", uri, res));
            return res;
        }
        public bool Stop()
        {
            bool res = false;
            if (_host == null) return res;

            _log.Write("Begin stopping SOAP server.");

            try
            {
                _host.Close();
                IDisposable h = _host as IDisposable;
                if (h != null) h.Dispose();
                res = true;
            }
            catch (Exception e)
            {
                _log.Write(e);
            }

            _log.Write(string.Format("End stopping SOAP server. Result: {0}", res));
            return res;
        }
    }
}
