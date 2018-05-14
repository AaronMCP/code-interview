using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects.PublishModel;

namespace HYS.IM.Messaging.Queuing.MSMQ
{
    public class MSMQReceiveCancelException: Exception
    {
        public MSMQReceiveCancelException()
            : base("Error happened in subscriber when processing message and need to cancel receiving message from MSMQ.")
        {
        }

        public MSMQReceiveCancelException(string message)
            : base("Error happened in subscriber when processing message and need to cancel receiving message from MSMQ. \r\n Inner exception message: " + message)
        {
        }

        public static void RaiseMSMQException(IPushRoute route)
        {
            PushChannelConfig cfg = route as PushChannelConfig;
            if (cfg != null && cfg.ProtocolType == ProtocolType.MSMQ)
            {
                throw new MSMQReceiveCancelException();
            }
        }

        public static void RaiseMSMQException(IPushRoute route, string message)
        {
            PushChannelConfig cfg = route as PushChannelConfig;
            if (cfg != null && cfg.ProtocolType == ProtocolType.MSMQ)
            {
                throw new MSMQReceiveCancelException(message);
            }
        }
    }
}
