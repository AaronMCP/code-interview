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
    public class PullSenderAgent : IDuplexProcessChannel
    {
        private ILog _log;
        private IRequester _requester;
        public PullSenderAgent(IRequester requester, ILog log)
        {
            _log = log;
            _requester = requester;
            _requester.OnMessageRequest += new MessageRequestHandler(requester_OnMessageRequest);
        }

        private List<PullSenderBase> _channels = new List<PullSenderBase>();

        private PullSenderBase GetPullSender(IPullRoute route)
        {
            if (route == null || _channels.Count < 1) return null;

            foreach (PullSenderBase s in _channels)
            {
                if (s.Channel.ID == route.ID) return s;
            }

            return null;
        }

        private bool requester_OnMessageRequest(IPullRoute route, Message request, out Message response)
        {
            DumpHelper.DumpRequesterSendingMessage(_log, request);

            if (OnPreProcessing != null)
            {
                if (!OnPreProcessing(ref request))
                {
                    _log.Write(LogType.Warning,
                        string.Format("The pre-processing handler rejected the requesting message (ID: {0}) from going out.",
                        (request != null) ? request.Header.ID.ToString() : "(null)"));
                    response = null;
                    return false;
                }
            }

            response = null;
            bool res = false;
            if (route != null)
            {
                PullSenderBase s = GetPullSender(route);
                if (s != null) res = s.SendMessage(request, out response);
            }
            else
            {
                foreach (PullSenderBase s in _channels)
                {
                    res = s.SendMessage(request, out response);
                    if (res) break;
                }
            }
            if (!res) return res;

            DumpHelper.DumpRequesterReceivingMessage(_log, response);

            if (OnPostProcessing != null)
            {
                if (!OnPostProcessing(request, ref response))
                {
                    _log.Write(LogType.Warning,
                        string.Format("The post-processing handler rejected the responsing message (ID: {0}) from coming in.",
                        (response != null) ? response.Header.ID.ToString() : "(null)"));
                    return false;
                }
            }

            return true;
        }

        public void RegisterChannel(PullChannelConfig channel)
        {
            PullSenderBase sender = PullHelper.CreatePullSender(channel, _log);
            _channels.Add(sender);
        }

        public void InitializeChannels()
        {
            List<PullSenderBase> failureChannels = new List<PullSenderBase>();
            foreach (PullSenderBase s in _channels)
            {
                if (!s.Initialize()) failureChannels.Add(s);
            }
            foreach (PullSenderBase s in failureChannels) _channels.Remove(s);
        }

        public void UninitializeChannels()
        {
            foreach (PullSenderBase s in _channels) s.Unintialize();
        }

        #region IDuplexProcessChannel Members

        public event DuplexMessagePreProcessHandler OnPreProcessing;

        public event DuplexMessagePostProcessHanlder OnPostProcessing;

        #endregion
    }
}
