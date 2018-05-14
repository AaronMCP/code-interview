using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Queuing
{
    public abstract class PushReceiverBase
    {
        protected ILog _log;
        public readonly PushChannelConfig Channel;

        public PushReceiverBase(PushChannelConfig config, ILog log)
        {
            _log = log;
            Channel = config;
        }

        public abstract bool Initialize();
        public abstract bool Start();
        public abstract bool Stop();
        public abstract bool Unintialize();

        public event PushMessageReceivedHandler OnMessageReceived;
        protected void NotifyMessageReceived(Message msg)
        {
            if (OnMessageReceived != null) OnMessageReceived(this, msg);
        }
    }

    public delegate void PushMessageReceivedHandler(PushReceiverBase receiver, Message message);
}
