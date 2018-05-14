using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Queuing.LPC
{
    public class LPCQueryReceiver : PullReceiverBase
    {
        public LPCQueryReceiver(PullChannelConfig config, ILog log)
            : base(config, log)
        {
            log.Write("Registering LPC responser. Pull route: " + config.ID.ToString());
            LPCReceiverDictionary.RegisterPullReceiver(config.ID, this);
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

        internal bool MessageReceive(Message request, out Message response)
        {
            return NotifyMessageReceived(request, out response);
        }
    }
}
