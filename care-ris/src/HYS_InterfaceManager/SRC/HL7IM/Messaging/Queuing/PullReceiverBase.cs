using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Common.Logging;

namespace HYS.IM.Messaging.Queuing
{
    public abstract class PullReceiverBase
    {
        protected ILog _log;
        public readonly PullChannelConfig Channel;

        public PullReceiverBase(PullChannelConfig config, ILog log)
        {
            _log = log;
            Channel = config;
        }

        public abstract bool Initialize();
        public abstract bool Start();
        public abstract bool Stop();
        public abstract bool Unintialize();

        public event PullMessageReceivedHandler OnMessageReceived;
        protected bool NotifyMessageReceived(Message request, out Message response)
        {
            if (OnMessageReceived != null) return OnMessageReceived(this, request, out response);
            else response = null;
            return false;
        }
    }

    public delegate bool PullMessageReceivedHandler(PullReceiverBase receiver, Message request, out Message response);
}
