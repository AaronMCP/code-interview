using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Queuing.LPC;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Queuing
{
    public class PushHelper
    {
        public static PushSenderBase CreatePushSender(PushChannelConfig config, ILog log)
        {
            if (config == null) return null;

            switch (config.ProtocolType)
            {
                case ProtocolType.MSMQ :
                    return new MSMQSender(config, log);
                case ProtocolType.LPC :
                    return new LPCNotifier(config, log);
            }

            return null;
        }
        public static PushReceiverBase CreatePushReceiver(PushChannelConfig config, ILog log)
        {
            if (config == null) return null;

            switch (config.ProtocolType)
            {
                case ProtocolType.MSMQ:
                    return new MSMQReceiver(config, log);
                case ProtocolType.LPC :
                    return new LPCNotificationReceiver(config, log);
            }

            return null;
        }
    }
}
