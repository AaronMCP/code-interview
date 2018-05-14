using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Messaging.Objects.PublishModel;

namespace HYS.IM.Messaging.Queuing.LPC
{
    /// <summary>
    /// Publish/subscibe module is "send and forget", but some sender need to know wether the reciever is able to recieve it (the message has been successfully sent out), 
    /// so sender can decide whether to retry. Therefore, we do not want to change the interface to much, (see IPublisher definition)
    /// but throw an exception to notify sender in LPC. In other cases (like MSMQ), it is easy to know whether the reciever is able to recieve it.
    /// </summary>
    public class LPCException : Exception
    {
        public LPCException()
            : base("Error happened in subscriber when receiving message with LPC.")
        {
        }

        public LPCException(string message)
            : base("Error happened in subscriber when receiving message with LPC. \r\n Inner exception message: " + message)
        {
        }

        public static void RaiseLPCException(IPushRoute route)
        {
            PushChannelConfig cfg = route as PushChannelConfig;
            if (cfg != null && cfg.ProtocolType == ProtocolType.LPC)
            {
                throw new LPCException();
            }
        }

        public static void RaiseLPCException(IPushRoute route, string message)
        {
            PushChannelConfig cfg = route as PushChannelConfig;
            if (cfg != null && cfg.ProtocolType == ProtocolType.LPC)
            {
                throw new LPCException(message);
            }
        }
    }
}
