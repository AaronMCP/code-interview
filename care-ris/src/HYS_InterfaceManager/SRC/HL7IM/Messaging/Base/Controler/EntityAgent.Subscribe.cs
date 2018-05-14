using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.PublishModel;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Queuing;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Objects.ProcessModel;

namespace HYS.IM.Messaging.Base.Controler
{
    public partial class EntityAgent
    {
        private PushReceiverAgent _subscriberAgent;

        private void InitializeSubscriberAgent(EntityConfigBase cfg)
        {
            if ((cfg.Interaction & InteractionTypes.Subscriber) != InteractionTypes.Subscriber) return;

            ISubscriber s = EntityInstance as ISubscriber;
            if (s == null) return;

            _subscriberAgent = new PushReceiverAgent(s, _log);
            foreach (PushChannelConfig c in cfg.SubscribeConfig.Channels)
            {
                _subscriberAgent.RegisterChannel(c);
            }

            _subscriberAgent.InitializeChannels();

            if (cfg.SubscribeConfig.ProcessConfig.IsEnable())
            {
                _subscriberAgent.OnProcessing += new OneWayMessageProcessHandler(_subscriberAgent_OnProcessing);
                _log.Write(LogType.Debug, "Message processing handler is injected for subscriber. " + cfg.EntityID.ToString());
            }
        }

        private void StartSubscriberAgent()
        {
            if (_subscriberAgent != null) _subscriberAgent.StartChannels();
        }

        private void StopSubscriberAgent()
        {
            if (_subscriberAgent != null) _subscriberAgent.StopChannels();
        }

        private void UninitializeSubscriberAgent()
        {
            if (_subscriberAgent != null) _subscriberAgent.UninitializeChannels();
        }

        private bool _subscriberAgent_OnProcessing(ref Message msg)
        {
            _log.Write(LogType.Debug,
                string.Format("Begin processing subscribed message (ID: {0}).",
                (msg != null) ? msg.Header.ID.ToString() : "(null)"));

            Message newMsg = null;
            bool res = EntityConfig.SubscribeConfig.ProcessConfig.TransformMessage(msg, out newMsg,
                Config.InitializeArgument.ConfigFilePath, _log);
            if (res) msg = newMsg;

            _log.Write(LogType.Debug,
                string.Format("Finish processing subscribed message (ID: {0}). Result: {1}.",
                (msg != null) ? msg.Header.ID.ToString() : "(null)", res.ToString()));
            return res;
        }
    }
}
