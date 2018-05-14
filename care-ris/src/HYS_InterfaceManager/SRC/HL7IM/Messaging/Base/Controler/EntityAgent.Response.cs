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
        private PullReceiverAgent _responserAgent;

        private void InitializeResponserAgent(EntityConfigBase cfg)
        {
            if ((cfg.Interaction & InteractionTypes.Responser) != InteractionTypes.Responser) return;

            IResponser p = EntityInstance as IResponser;
            if (p == null) return;

            _responserAgent = new PullReceiverAgent(p, _log);
            foreach (PullChannelConfig c in cfg.ResponseConfig.Channels)
            {
                _responserAgent.RegisterChannel(c);
            }

            _responserAgent.InitializeChannels();

            if (cfg.ResponseConfig.ProcessConfig.PreProcessConfig.IsEnable())
            {
                _responserAgent.OnPreProcessing += new DuplexMessagePreProcessHandler(_responserAgent_OnPreProcessing);
                _log.Write(LogType.Debug, "Message pre processing handler is injected for responser. " + cfg.EntityID.ToString());
            }

            if (cfg.ResponseConfig.ProcessConfig.PostProcessConfig.IsEnable())
            {
                _responserAgent.OnPostProcessing += new DuplexMessagePostProcessHanlder(_responserAgent_OnPostProcessing);
                _log.Write(LogType.Debug, "Message post processing handler is injected for responser. " + cfg.EntityID.ToString());
            }
        }

        private void StartResponserAgent()
        {
            if (_responserAgent != null) _responserAgent.StartChannels();
        }

        private void StopResponserrAgent()
        {
            if (_responserAgent != null) _responserAgent.StopChannels();
        }

        private void UnintializeResponserAgent()
        {
            if (_responserAgent != null) _responserAgent.UninitializeChannels();
        }

        private bool _responserAgent_OnPreProcessing(ref Message requestMsg)
        {
            _log.Write(LogType.Debug,
                string.Format("Begin processing requesting message (ID: {0}).",
                (requestMsg != null) ? requestMsg.Header.ID.ToString() : "(null)"));

            Message newMsg = null;
            bool res = EntityConfig.ResponseConfig.ProcessConfig.PreProcessConfig.TransformMessage(requestMsg, out newMsg,
                Config.InitializeArgument.ConfigFilePath, _log);
            if (res) requestMsg = newMsg;

            _log.Write(LogType.Debug,
                string.Format("Finish processing requesting message (ID: {0}). Result: {1}.",
                (requestMsg != null) ? requestMsg.Header.ID.ToString() : "(null)", res.ToString()));
            return res;
        }

        private bool _responserAgent_OnPostProcessing(Message requestMsg, ref Message responseMsg)
        {
            _log.Write(LogType.Debug,
                string.Format("Begin processing responsing message (ID: {0}).",
                (responseMsg != null) ? responseMsg.Header.ID.ToString() : "(null)"));

            Message newMsg = null;
            bool res = EntityConfig.ResponseConfig.ProcessConfig.PostProcessConfig.TransformMessage(responseMsg, out newMsg,
                Config.InitializeArgument.ConfigFilePath, _log);
            if (res) responseMsg = newMsg;

            _log.Write(LogType.Debug,
                string.Format("Finish processing responsing message (ID: {0}). Result: {1}.",
                (responseMsg != null) ? responseMsg.Header.ID.ToString() : "(null)", res.ToString()));
            return res;
        }
    }
}
