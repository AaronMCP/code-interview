using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Objects;
using System.Diagnostics;

namespace HYS.IM.Messaging.Queuing.LPC
{
    public class LPCNotificationReceiver : PushReceiverBase
    {
        public LPCNotificationReceiver(PushChannelConfig config, ILog log)
             : base(config, log)
        {
            //StackTrace t = new StackTrace(true);
            //log.Write(t.ToString());

            log.Write("Registering LPC subscriber. Push route: " + config.ID .ToString());
            LPCReceiverDictionary.RegisterPushReceiver(config.ID, this);
        }

        public override bool Initialize()
        {
            return true;
        }
        public override bool Start()
        {
            return true;
        }
        public override bool Stop()
        {
            return true;
        }
        public override bool Unintialize()
        {
            return true;
        }

        internal void ReceiveMessage(Message msg)
        {
            NotifyMessageReceived(msg);
        }
    }
}
