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
        private PushSenderAgent _publisherAgent;

        private void InitializePublisherAgent(EntityConfigBase cfg)
        {
            if ((cfg.Interaction & InteractionTypes.Publisher) != InteractionTypes.Publisher) return;

            IPublisher p = EntityInstance as IPublisher;
            if (p == null) return;

            _publisherAgent = new PushSenderAgent(p, _log);
            foreach (PushChannelConfig c in cfg.PublishConfig.Channels)
            {
                _publisherAgent.RegisterChannel(c);
            }

            _publisherAgent.InitializeChannels();

            if (cfg.PublishConfig.ProcessConfig.IsEnable())
            {
                _publisherAgent.OnProcessing += new OneWayMessageProcessHandler(_publisherAgent_OnProcessing);
                _log.Write(LogType.Debug, "Message processing handler is injected for publisher. " + cfg.EntityID.ToString());
            }
        }

        private void UnintializePublisherAgent()
        {
            if (_publisherAgent != null) _publisherAgent.UninitializeChannels();
        }

        private bool _publisherAgent_OnProcessing(ref Message msg)
        {
            _log.Write(LogType.Debug, 
                string.Format("Begin processing publishing message (ID: {0}).",
                (msg != null) ? msg.Header.ID.ToString() : "(null)"));

            Message newMsg = null;
            bool res = EntityConfig.PublishConfig.ProcessConfig.TransformMessage(msg, out newMsg,
                Config.InitializeArgument.ConfigFilePath, _log);
            if (res) msg = newMsg;

            _log.Write(LogType.Debug,
                string.Format("Finish processing publishing message (ID: {0}). Result: {1}.",
                (msg != null) ? msg.Header.ID.ToString() : "(null)", res.ToString()));
            return res;
        }
    }
}
