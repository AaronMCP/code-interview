using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using HYS.IM.Common.Logging;
using HYS.IM.Common.WCFHelper.Configurability;
using HYS.IM.Common.WCFHelper;
using System.Threading;
using HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Config;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler
{
    [ServiceContract]
    public interface IAbstractContract
    {
        [OperationContract(Action = "ProcessMessage", ReplyAction = "*")] 
        Message ProcessMessage(Message request);
    }

    public class AbstractService : IAbstractContract
    {
        private readonly SOAPReceiver _receiver;
        public AbstractService()
        {
            ConfigurableServiceHost host = OperationContext.Current.Host as ConfigurableServiceHost;
            if (host == null)
                throw new ArithmeticException("Cannot find ConfigurableServiceHost in current operation context.");

            _receiver = host.Tag as SOAPReceiver;
            if (_receiver == null)
                throw new ArithmeticException("Cannot find SOAPReceiver in current operation context.");
        }

        public Message ProcessMessage(Message request)
        {
            try
            {
                string req = SoapMessageHelper.DumpWCFMessage(request);
                if (req == null || req.Length < 1) return null;

                SOAPReceiverSession session = new SOAPReceiverSession(req, _receiver._enableSessionStatusLog);
                _receiver._log.Write("Begin processing " + session.ToString());

                string rsp = "";
                if (_receiver.NotifyMessageReceived(session))
                {
                    rsp = session.OutgoingSOAPEnvelope;
                }
                else
                {
                    _receiver._log.Write("Preparing SOAP error message from file: " + _receiver._cfg.GetSOAPErrorMessageFileFullPath());
                    rsp = _receiver._cfg.GetSOAPErrorMessageContent();
                }

                Message msg = SoapMessageHelper.CreateWCFMessageWithEnvelope(
                    SoapEnvelopeVersion.Soap11,
                    WSAddressingVersion.None,
                    "*", rsp);

                if (_receiver._enableSessionStatusLog) _receiver._log.Write("Session status history: " + session.GetStatusLog());
                _receiver._log.Write("End processing " + session.ToString());
                _receiver._log.Write("");
                return msg;
            }
            catch (Exception e)
            {
                _receiver._log.Write(e);
                return null;
            }
        }
    }

    public delegate bool ReceiveSOAPMessageHandler(SOAPReceiverSession session);

    public class SOAPReceiver
    {
        internal readonly ILog _log;
        internal readonly SOAPServerConfig _cfg;
        internal readonly bool _enableSessionStatusLog;

        private readonly string _configFile;
        private ServiceHost _host;

        public SOAPReceiver(SOAPServerConfig cfg, string cfgFile, ILog log)
        {
            _cfg = cfg;
            _log = log;
            _configFile = cfgFile;
        }
        public SOAPReceiver(SOAPServerConfig cfg, string cfgFile, ILog log, bool enableSessionStatusLog)
        {
            _cfg = cfg;
            _log = log;
            _configFile = cfgFile;
            _enableSessionStatusLog = enableSessionStatusLog;
        }

        public event ReceiveSOAPMessageHandler OnMessageReceived;
        internal bool NotifyMessageReceived(SOAPReceiverSession session)
        {
            if (OnMessageReceived == null) return false;
            return OnMessageReceived(session);
        }

        public bool Start(string uri)
        {
            bool res = false;
            if (uri == null || uri.Length < 1) return res;
            _log.Write(string.Format("Begin starting SOAP receiver on {0}.", uri));

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

            _log.Write(string.Format("End starting SOAP reciever on {0}. Result: {1}", uri, res));
            return res;
        }
        public bool Stop()
        {
            bool res = false;
            if (_host == null) return res;

            _log.Write("Begin stopping SOAP receiver.");

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

            _log.Write(string.Format("End stopping SOAP reciever. Result: {0}", res));
            return res;
        }
    }
}
