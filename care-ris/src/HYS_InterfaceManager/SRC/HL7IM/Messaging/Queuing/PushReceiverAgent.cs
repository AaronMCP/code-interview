using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Objects.ProcessModel;

namespace HYS.IM.Messaging.Queuing
{
    public class PushReceiverAgent : IOneWayProcessChannel
    {
        private ILog _log;
        private ISubscriber _subscriber;
        public PushReceiverAgent(ISubscriber subscriber, ILog log)
        {
            _log = log;
            _subscriber = subscriber;
        }

        private List<PushReceiverBase> _channels = new List<PushReceiverBase>();

        private void receiver_OnMessageReceived(PushReceiverBase receiver, Message message)
        {
            DumpHelper.DumpSubscriberMessage(_log, message);

            if (OnProcessing != null)
            {
                if (!OnProcessing(ref message))
                {
                    _log.Write(LogType.Warning,
                        string.Format("The processing handler rejected the subscribed message (ID: {0}) from coming in.",
                        (message != null) ? message.Header.ID.ToString() : "(null)"));
                    return;
                }
            }

            _subscriber.ReceiveMessage(receiver.Channel, message);
        }

        public void RegisterChannel(PushChannelConfig channel)
        {
            PushReceiverBase receiver = PushHelper.CreatePushReceiver(channel, _log);
            if (receiver == null) return;

            receiver.OnMessageReceived += new PushMessageReceivedHandler(receiver_OnMessageReceived);
            _channels.Add(receiver);
        }

        public void InitializeChannels()
        {
            List<PushReceiverBase> failureChannels = new List<PushReceiverBase>();
            foreach (PushReceiverBase r in _channels)
            {
                if (!r.Initialize()) failureChannels.Add(r);
            }
            foreach (PushReceiverBase r in failureChannels) _channels.Remove(r);
        }

        public void StartChannels()
        {
            foreach (PushReceiverBase r in _channels) r.Start();
        }

        public void StopChannels()
        {
            foreach (PushReceiverBase r in _channels) r.Stop();
        }

        public void UninitializeChannels()
        {
            foreach (PushReceiverBase r in _channels) r.Unintialize();
        }

        #region IOneWayProcessChannel Members

        public event OneWayMessageProcessHandler OnProcessing;

        #endregion
    }
}
