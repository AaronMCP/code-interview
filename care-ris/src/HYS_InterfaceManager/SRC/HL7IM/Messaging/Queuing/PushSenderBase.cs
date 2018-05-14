using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Queuing
{
    public abstract class PushSenderBase
    {
        protected ILog _log;
        public readonly PushChannelConfig Channel;

        public PushSenderBase(PushChannelConfig config, ILog log)
        {
            _log = log;
            Channel = config;
        }

        public abstract bool Initialize();
        public abstract bool Unintialize();
        public abstract bool SendMessage(Message message);
    }
}
