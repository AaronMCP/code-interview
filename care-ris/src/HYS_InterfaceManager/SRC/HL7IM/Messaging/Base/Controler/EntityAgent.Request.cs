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
        private PullSenderAgent _requesterAgent;

        private void InitializeRequesterAgent(EntityConfigBase cfg)
        {
            if ((cfg.Interaction & InteractionTypes.Requester) != InteractionTypes.Requester) return;

            IRequester p = EntityInstance as IRequester;
            if (p == null) return;

            _requesterAgent = new PullSenderAgent(p, _log);
            foreach (PullChannelConfig c in cfg.RequestConfig.Channels)
            {
                _requesterAgent.RegisterChannel(c);
            }

            _requesterAgent.InitializeChannels();

            if (cfg.RequestConfig.ProcessConfig.PreProcessConfig.IsEnable())
            {
                _requesterAgent.OnPreProcessing += new DuplexMessagePreProcessHandler(_requesterAgent_OnPreProcessing);
                _log.Write(LogType.Debug, "Message pre processing handler is injected for requester. " + cfg.EntityID.ToString());
            }

            if (cfg.RequestConfig.ProcessConfig.PostProcessConfig.IsEnable())
            {
                _requesterAgent.OnPostProcessing += new DuplexMessagePostProcessHanlder(_requesterAgent_OnPostProcessing);
                _log.Write(LogType.Debug, "Message post processing handler is injected for requester. " + cfg.EntityID.ToString());
            }
        }

        private void UnintializeRequesterAgent()
        {
            if (_requesterAgent != null) _requesterAgent.UninitializeChannels();
        }

        private bool _requesterAgent_OnPreProcessing(ref Message requestMsg)
        {
            _log.Write(LogType.Debug,
                string.Format("Begin processing requesting message (ID: {0}).",
                (requestMsg != null) ? requestMsg.Header.ID.ToString() : "(null)"));

            Message newMsg = null;
            bool res = EntityConfig.RequestConfig.ProcessConfig.PreProcessConfig.TransformMessage(requestMsg, out newMsg,
                Config.InitializeArgument.ConfigFilePath, _log);
            if (res) requestMsg = newMsg;

            _log.Write(LogType.Debug,
                string.Format("Finish processing requesting message (ID: {0}). Result: {1}.",
                (requestMsg != null) ? requestMsg.Header.ID.ToString() : "(null)", res.ToString()));
            return res;
        }

        private bool _requesterAgent_OnPostProcessing(Message requestMsg, ref Message responseMsg)
        {
            _log.Write(LogType.Debug,
                string.Format("Begin processing responsing message (ID: {0}).",
                (responseMsg != null) ? responseMsg.Header.ID.ToString() : "(null)"));

            Message newMsg = null;
            bool res = EntityConfig.RequestConfig.ProcessConfig.PostProcessConfig.TransformMessage(responseMsg, out newMsg,
                Config.InitializeArgument.ConfigFilePath, _log);
            if (res) responseMsg = newMsg;

            _log.Write(LogType.Debug,
                string.Format("Finish processing responsing message (ID: {0}). Result: {1}.",
                (responseMsg != null) ? responseMsg.Header.ID.ToString() : "(null)", res.ToString()));
            return res;
        }
    }
}
