using System;
using System.Text;
using System.Messaging;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Objects.PublishModel;

namespace HYS.IM.Messaging.Queuing.MSMQ
{
    public class MSMQHelper
    {
        public static string GetMessageLabel(HYS.IM.Messaging.Objects.Message message)
        {
            if (message == null) return "";

            StringBuilder sb = new StringBuilder();
            sb.Append(message.Header.Type.ToString()).Append(" ").Append(message.Header.ID);
            return sb.ToString();
        }

        public static HYS.IM.Messaging.Objects.Message GetMessage(System.Messaging.Message msg)
        {
            if (msg == null) return null;

            string xml = msg.Body.ToString();
            return XObjectManager.CreateObject<HYS.IM.Messaging.Objects.Message>(xml);
        }

        /// <summary>
        /// Use Case : Avoid message accumulated in the MSMQ (when Message Box rety speed the faster then the File Out processing speed)
        /// 
        /// Before sending message to MSMQ, some Message Entity, such as Message Box's retry mechanism,
        /// wants to ensure there are not so many messages still existing in the MSMQ (because File Out may be very slow in processing them),
        /// in order to avoid messages accumulated in the MSMQ for a long time, as well as reduce the chance to send duplicated message into the MSMQ.
        /// </summary>
        /// <returns></returns>
        public static int GetMessageCount(string msmqPath, ILog log)
        {
            try
            {
                using (MessageQueue queue = new MessageQueue(msmqPath))
                {
                    queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                    int count = GetMessageCountWithPeek(queue);
                    queue.Close();
                    return count;
                }
            }
            catch (Exception err)
            {
                log.Write(LogType.Error, "Get message count failed from MSMQ: " + msmqPath);
                log.Write(err);
                return -1;
            }
        }

        private static System.Messaging.Message PeekWithoutTimeout(MessageQueue q, Cursor cursor, PeekAction action)
        {
            System.Messaging.Message ret = null;
            try
            {
                ret = q.Peek(new TimeSpan(1), cursor, action);
            }
            catch (MessageQueueException mqe)
            {
                //if (!mqe.Message.ToLower().Contains("timeout"))
                if (mqe.MessageQueueErrorCode != MessageQueueErrorCode.IOTimeout)
                {
                    throw;
                }
            }
            return ret;
        }

        private static int GetMessageCountWithPeek(MessageQueue q)
        {
            // remember to release resource as soon as possible,
            // if not there may be a "An invalid handle was passed to the function." exception

            int count = 0;
            using (Cursor cursor = q.CreateCursor())
            {
                System.Messaging.Message m = PeekWithoutTimeout(q, cursor, PeekAction.Current);
                if (m != null)
                {
                    m.Dispose();

                    count = 1;
                    while ((m = PeekWithoutTimeout(q, cursor, PeekAction.Next)) != null)
                    {
                        m.Dispose();

                        count++;
                    }
                }
            }
            return count;
        }

    }
}
