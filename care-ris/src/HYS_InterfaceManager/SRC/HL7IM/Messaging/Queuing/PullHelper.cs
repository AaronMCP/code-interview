using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Queuing.RPC;

namespace HYS.IM.Messaging.Queuing
{
    public class PullHelper
    {
        public static PullSenderBase CreatePullSender(PullChannelConfig config, ILog log)
        {
            if (config == null) return null;

            switch (config.ProtocolType)
            {
                case ProtocolType.RPC_NamedPipe:
                case ProtocolType.RPC_TCP:
                case ProtocolType.RPC_SOAP:
                    return new RPCPullSender(config, log);
                case ProtocolType.LPC:
                    return new LPCQuerier(config, log);
            }

            return null;
        }
        public static PullReceiverBase CreatePullReceiver(PullChannelConfig config, ILog log)
        {
            if (config == null) return null;

            switch (config.ProtocolType)
            {
                case ProtocolType.RPC_NamedPipe:
                case ProtocolType.RPC_TCP:
                case ProtocolType.RPC_SOAP:
                    return new RPCPullReceiver(config, log);
                case ProtocolType.LPC :
                    return new LPCQueryReceiver(config, log);
            }

            return null;
        }
    }
}
