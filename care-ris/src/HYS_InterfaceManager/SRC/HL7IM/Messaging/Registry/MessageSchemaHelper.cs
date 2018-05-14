using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Registry
{
    /// <summary>
    /// This class is to pack/unpack XObject based XML content into/from XDS Gateway message body.
    /// </summary>
    public class MessageSchemaHelper
    {
        public static T GetMessageContent<T>(Message msg)
            where T : XBase
        {
            return GetMessageContent<T>(msg.Body);
        }

        public static T GetMessageContent<T>(string msgContent)
            where T : XBase
        {
            if (msgContent == null || msgContent.Length < 1) return null;
            return XObjectManager.CreateObject<T>(msgContent);
        }
    }

    /// <summary>
    /// This class is used in PIX prototype only to pack HL7v2 hat and pipe message into XDS Gateway message.
    /// Do not need to use it in XDS Gateway 1.1 deliverables.
    /// </summary>
    public class HL7MessageHelper
    {
        private static string HL7V2Prefix = "<HL7V2Message><![CDATA[";
        private static string HL7V2Appendix = "]]></HL7V2Message>";

        public static void SetHL7V2PayLoad(Message msg, string payload)
        {
            if (msg == null || payload == null) return;
            StringBuilder sb = new StringBuilder();
            sb.Append(HL7V2Prefix).Append(payload).Append(HL7V2Appendix);
            msg.Body = sb.ToString();
        }
        public static string GetHL7V2PayLoad(Message msg)
        {
            if (msg == null) return "";
            if (msg.Body == null || msg.Body.Length < 1) return "";
            int begin = msg.Body.IndexOf(HL7V2Prefix);
            if (begin < 0) return "";
            begin += HL7V2Prefix.Length;
            if (begin >= msg.Body.Length) return "";
            int end = msg.Body.IndexOf(HL7V2Appendix, begin);
            if (end < 0) return "";
            string str = msg.Body.Substring(begin, end - begin);
            return str;
        }
    }

    public class DataTrackingLogHelper
    {
        /// <summary>
        /// It is suggested not to set the keywords in the log message,
        /// and the Log Outbound Adapter should generate keyword and send to RHIS,
        /// in order to ensure performance of the real integration data flow.
        /// </summary>
        /// <param name="logSender"></param>
        /// <param name="logString"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static Message CreateDataTrackingLogMessage(string logSender, string logString, string Memo,params string[] keywords)
        {
            Message msg = new Message();
            msg.Header.ID = Guid.NewGuid();
            msg.Header.Type = MessageRegistry.DataTrackingLogMessageType;

            StringBuilder sb = new StringBuilder();
            sb.Append(@"<LogMessage>");
            sb.Append(@"<LogSender>").Append(logSender).Append(@"</LogSender>");            

            sb.Append(@"<LogString>").Append(logString).Append(@"</LogString>");
            sb.Append(@"<LogMemo><![CDATA[").Append(Memo).Append(@"]]></LogMemo>");

            StringBuilder sbKeyword = new StringBuilder();
            foreach (string k in keywords) sbKeyword.Append(k).Append(',');
            sb.Append(@"<Keywords>").Append(sbKeyword.ToString().TrimEnd(',')).Append(@"</Keywords>");

            
            sb.Append(@"</LogMessage>");
            msg.Body = sb.ToString();

            return msg;
        }
    }
}
