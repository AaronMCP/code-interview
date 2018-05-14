using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.IM.Common.Logging;
using System.ServiceModel;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Queuing.RPC
{
    public class RPCPullSender : PullSenderBase
    {
        public RPCPullSender(PullChannelConfig config, ILog log)
            : base(config, log)
        {
        }

        private ChannelFactory<IRPCPullService> _factory;
        
        //do not maintain this object, if it is in the faulted state (for example time out for one time)
        //it could not be used any more

        //private IRPCPullService _client;

        public override bool Initialize()
        {
            try
            {
                switch (Channel.ProtocolType)
                {
                    case ProtocolType.RPC_NamedPipe:
                        _factory = new ChannelFactory<IRPCPullService>
                            (new NetNamedPipeBinding(), Channel.RPCConfig.URI);
                        break;
                    case ProtocolType.RPC_TCP :
                        _factory = new ChannelFactory<IRPCPullService>
                            (new NetTcpBinding(), Channel.RPCConfig.URI);
                        break;
                    case ProtocolType.RPC_SOAP :
                        _factory = new ChannelFactory<IRPCPullService>
                            (new BasicHttpBinding(), Channel.RPCConfig.URI);
                        break;
                }

                if (_factory != null) _log.Write("RPCPullSender initialize succeeded. " + Channel.RPCConfig.URI);
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
                if (_factory != null)
                {
                    _factory.Close();
                    IDisposable f = _factory as IDisposable;
                    if (f != null) f.Dispose();
                }

                _log.Write("RPCPullSender uninitialize succeeded. " + Channel.RPCConfig.URI);
                return true;
            }
            catch (Exception err)
            {
                _log.Write(err);
                return false;
            }
        }

        public override bool SendMessage(Message request, out Message response)
        {
            response = null;

            if (request == null || request.Header == null)
            {
                _log.Write(LogType.Error, "RPCPullSender cannot send NULL message or message without header.");
                return false;
            }

            string strReq = request.ToXMLString();

            _log.Write(string.Format("RPCPullSender sending requesting message id {0} via {1}",
                    request.Header.ID.ToString(), Channel.RPCConfig.URI));

            try
            {
                bool res = false;
                string strRsp = null;

                IRPCPullService client = _factory.CreateChannel();
                using (client as IDisposable)
                {
                    res = client.ProcessMessage(strReq, out strRsp);
                }

                if (!res)
                {
                    _log.Write(LogType.Error, 
                        string.Format("RPCPullSender receive process failed from the responser entity via {0}",
                        Channel.RPCConfig.URI));
                    return false;
                }
                else
                {
                    response = XObjectManager.CreateObject<Message>(strRsp);
                    if (response == null || response.Header == null)
                    {
                        _log.Write(LogType.Error,
                            string.Format("RPCPullSender receive responsing NULL message or message without header via {0}",
                            Channel.RPCConfig.URI));
                        return false;
                    }
                    else
                    {
                        _log.Write(string.Format("RPCPullSender receive responsing message id {0} via {1}",
                            response.Header.ID.ToString(), Channel.RPCConfig.URI));
                    }
                }

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
