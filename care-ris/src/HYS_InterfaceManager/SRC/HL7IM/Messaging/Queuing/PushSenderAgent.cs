using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Objects.RoutingModel;
using HYS.IM.Messaging.Objects.ProcessModel;

namespace HYS.IM.Messaging.Queuing
{
    public class PushSenderAgent : IOneWayProcessChannel
    {
        private ILog _log;
        private IPublisher _publisher;
        private IPublisherObserver _observer;
        public PushSenderAgent(IPublisher publisher, ILog log)
        {
            _log = log;
            _publisher = publisher;
            _publisher.OnMessagePublish += new MessagePublishHandler(publisher_OnMessagePublish);
            _observer = publisher as IPublisherObserver;
        }

        private List<PushSenderBase> _channels = new List<PushSenderBase>();

        private bool Match(string subscriberName, SubscriptionRule desc, Message message)
        {
            if (message == null || message.Header == null) return false;

            RoutingRuleValidator v = RoutingRuleValidator.CreateRoutingRuleValidatorFromCache(desc);
            if (v == null)
            {
                _log.Write(LogType.Error, "[PushSenderAgent] Create routing rule validator failed for " + subscriberName);
                return false;
            }

            if (v.Match(message))
            {
                _log.Write("[PushSenderAgent] Message (type " + message.Header.Type + ") matched the subscription rule of " + subscriberName + ", route the message.");
                return true;
            }
            else
            {
                if (v.LastError != null) _log.Write(v.LastError);
                _log.Write("[PushSenderAgent] Message (type " + message.Header.Type + ") does not matched the subscription rule of " + subscriberName + ", block the message.");
                return false;
            }
        }

        //private bool Match(string subscriberName, SubscriptionRule desc, Message message)
        //{
        //    if (message == null) return false;
        //    if (desc == null || desc.MessageTypeList.Count < 1)
        //    {
        //        _log.Write("[PushSenderAgent] No message type criteria in subscription rule of " + subscriberName + ", route all messages.");
        //        return true;
        //    }

        //    MessageType mt = message.Header.Type;
        //    foreach (MessageType t in desc.MessageTypeList)
        //    {
        //        if (mt.EqualsTo(t))
        //        {
        //            _log.Write("[PushSenderAgent] Message type " + mt.ToString() + " matched the subscription rule of " + subscriberName + ", route the message.");
        //            return true;
        //        }
        //    }

        //    _log.Write("[PushSenderAgent] Message type " + mt.ToString() + " does not matched the subscription rule of " + subscriberName + ", block the message.");
        //    return false;
        //}

        private bool publisher_OnMessagePublish(Message message)
        {
            DumpHelper.DumpPublisherMessage(_log, message);

            if (OnProcessing != null)
            {
                if (!OnProcessing(ref message))
                {
                    _log.Write(LogType.Warning,
                       string.Format( "The processing handler rejected the publishing message (ID: {0}) from going out.",
                        (message != null) ? message.Header.ID.ToString() : "(null)"));
                    return false;
                }
            }

            if (_channels.Count < 1)
            {
                if (_observer != null) _observer.PublishingMessage(PublishResultType.NoChannel, message, null);
                
                // When there is no subscriber, we define to return true to upper application.
                // According to publish/subscribe model, publisher "send and forget",
                // publisher need to ensure send is success.
                // Therefore if there is subscriber and send failed, we return false.
                // If there is no subscriber, do nothing is success, so we return true.

                return true;
            }
            else
            {
                bool atLessOneSent = false;

                foreach (PushSenderBase s in _channels)
                {
                    if (Match(s.Channel.ReceiverEntityName, s.Channel.Subscription, message))
                    {
                        if (s.SendMessage(message))
                        {
                            atLessOneSent = true;       // If no channel matched, it also means failure.

                            if (_observer != null) _observer.PublishingMessage(PublishResultType.SendSucceeded, message, s.Channel);
                        }
                        else
                        {
                            if (_observer != null) _observer.PublishingMessage(PublishResultType.SendFailed, message, s.Channel);

                            //return false;               // Any channel failed means failure.
                        }
                    }
                    else
                    {
                        if (_observer != null) _observer.PublishingMessage(PublishResultType.NotMatched, message, s.Channel);
                    }
                }

                return atLessOneSent;
            }
        }

        public void RegisterChannel(PushChannelConfig channel)
        {
            PushSenderBase sender = PushHelper.CreatePushSender(channel, _log);
            _channels.Add(sender);
        }

        public void InitializeChannels()
        {
            List<PushSenderBase> failureChannels = new List<PushSenderBase>();
            foreach (PushSenderBase s in _channels)
            {
                if (!s.Initialize()) failureChannels.Add(s);
            }
            foreach (PushSenderBase s in failureChannels) _channels.Remove(s);
        }

        public void UninitializeChannels()
        {
            foreach (PushSenderBase s in _channels) s.Unintialize();
        }

        #region IOneWayProcessChannel Members

        public event OneWayMessageProcessHandler OnProcessing;

        #endregion
    }
}
