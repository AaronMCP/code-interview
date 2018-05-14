using System;
using System.Text;
using System.Collections.Generic;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.Entity;
using HYS.IM.Messaging.Objects.RequestModel;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Objects.ProcessModel;

namespace HYS.IM.Messaging.Queuing
{
    public class PullReceiverAgent : IDuplexProcessChannel
    {
        private ILog _log;
        private IResponser _responser;
        public PullReceiverAgent(IResponser responser, ILog log)
        {
            _log = log;
            _responser = responser;
        }

        private List<PullReceiverBase> _channels = new List<PullReceiverBase>();

        private bool receiver_OnMessageReceived(PullReceiverBase receiver, Message request, out Message response)
        {
            DumpHelper.DumpResponserReceivingMessage(_log, request);

            if (OnPreProcessing != null)
            {
                if (!OnPreProcessing(ref request))
                {
                    _log.Write(LogType.Warning,
                        string.Format("The pre-processing handler rejected the requesting message (ID: {0}) from coming in.",
                        (request != null) ? request.Header.ID.ToString() : "(null)"));
                    response = null;
                    return false;
                }
            }

            bool res = _responser.ProcessMessage(receiver.Channel, request, out response);

            DumpHelper.DumpResponserSendingMessage(_log, response);

            if (OnPostProcessing != null)
            {
                if (!OnPostProcessing(request, ref response))
                {
                    _log.Write(LogType.Warning,
                        string.Format("The post-processing handler rejected the responsing message (ID: {0}) from going out.",
                        (response != null) ? response.Header.ID.ToString() : "(null)"));
                    return false;
                }
            }

            return res;
        }

        public void RegisterChannel(PullChannelConfig channel)
        {
            PullReceiverBase receiver = PullHelper.CreatePullReceiver(channel, _log);
            if (receiver == null) return;

            receiver.OnMessageReceived += new PullMessageReceivedHandler(receiver_OnMessageReceived);
            _channels.Add(receiver);
        }

        public void InitializeChannels()
        {
            List<PullReceiverBase> failureChannels = new List<PullReceiverBase>();
            foreach (PullReceiverBase r in _channels)
            {
                if (!r.Initialize()) failureChannels.Add(r);
            }
            foreach (PullReceiverBase r in failureChannels) _channels.Remove(r);
        }

        public void StartChannels()
        {
            foreach (PullReceiverBase r in _channels) r.Start();
        }

        public void StopChannels()
        {
            foreach (PullReceiverBase r in _channels) r.Stop();
        }

        public void UninitializeChannels()
        {
            foreach (PullReceiverBase r in _channels) r.Unintialize();
        }

        #region IDuplexProcessChannel Members

        public event DuplexMessagePreProcessHandler OnPreProcessing;

        public event DuplexMessagePostProcessHanlder OnPostProcessing;

        #endregion
    }
}
