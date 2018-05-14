using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Common.Logging;
using System.ServiceModel;
using HYS.IM.Messaging.Objects;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing.RPC
{
    public class RPCPullReceiver : PullReceiverBase
    {
        public RPCPullReceiver(PullChannelConfig config, ILog log)
            : base(config, log)
        {
        }

        private RPCServiceHost _host;

        internal bool ProcessMessage(string request, out string response)
        {
            response = "";

            try
            {
                Message req = XObjectManager.CreateObject<Message>(request);

                if (req == null || req.Header == null)
                {
                    _log.Write(LogType.Error,
                        string.Format("RPCPullReceiver receive NULL message or message without header via {0}",
                        Channel.RPCConfig.URI));
                    return false;
                }
                else
                {
                    _log.Write(string.Format("RPCPullReceiver receive message id {0} via {1}",
                               req.Header.ID.ToString(), Channel.RPCConfig.URI));
                }

                Message rsp = null;
                bool res = NotifyMessageReceived(req, out rsp);

                if (res == false || rsp == null || rsp.Header == null)
                {
                    _log.Write(LogType.Error,
                       "RPCPullReceiver notify the requesting message to the responser entity failed, or receive NULL responsing message, or receive responsing message without header.");
                    return false;
                }
                else
                {
                    response = rsp.ToXMLString();

                    _log.Write(string.Format("RPCPullReceiver sending response message id {0} via {1}",
                              rsp.Header.ID.ToString(), Channel.RPCConfig.URI));
                }

                return true;
            }
            catch (Exception err)
            {
                _log.Write(err);
                return false;
            }
        }

        public override bool Initialize()
        {
            try
            {
                _host = new RPCServiceHost(typeof(RPCPullService));
                _host.Tag = this;

                switch (Channel.ProtocolType)
                {
                    case ProtocolType.RPC_NamedPipe:
                        _host.AddServiceEndpoint("HYS.IM.Messaging.Queuing.RPC.IRPCPullService",
                            new NetNamedPipeBinding(), Channel.RPCConfig.URI);
                        break;
                    case ProtocolType.RPC_TCP:
                        _host.AddServiceEndpoint("HYS.IM.Messaging.Queuing.RPC.IRPCPullService",
                            new NetTcpBinding(), Channel.RPCConfig.URI);
                        break;
                    case ProtocolType.RPC_SOAP:
                        _host.AddServiceEndpoint("HYS.IM.Messaging.Queuing.RPC.IRPCPullService",
                            new BasicHttpBinding(), Channel.RPCConfig.URI);
                        break;
                }

                _log.Write("RPCPullReceiver initialize succeeded. " + Channel.RPCConfig.URI);
                return true;
            }
            catch (Exception err)
            {
                _log.Write(err);
                return false;
            }
        }

        public override bool Start()
        {
            try
            {
                _log.Write("RPCPullReceiver try to start listener.");

                if (_host == null) return false;
                _host.Open();

                _log.Write("RPCPullReceiver listener started at: " + Channel.RPCConfig.URI);
                return true;
            }
            catch (Exception err)
            {
                _log.Write(err);
                return false;
            }
        }

        public override bool Stop()
        {
            try
            {
                _log.Write("RPCPullReceiver try to stop listener.");

                if (_host == null) return false;
                _host.Close();

                _log.Write("RPCPullReceiver listener stopped at: " + Channel.RPCConfig.URI);
                return true;
            }
            catch (Exception err)
            {
                _log.Write(err);
                return false;
            }
        }

        public override bool Unintialize()
        {
            try
            {
                IDisposable h = _host as IDisposable;
                if (h != null) h.Dispose();

                _log.Write("RPCPullReceiver uninitialize succeeded. " + Channel.RPCConfig.URI);
                return true;
            }
            catch (Exception err)
            {
                _log.Write(err);
                return false;
            }
        }
    }
}
