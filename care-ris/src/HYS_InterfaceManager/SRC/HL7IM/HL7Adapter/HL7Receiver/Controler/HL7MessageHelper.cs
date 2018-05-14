using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.MessageDevices.HL7Adapter.HL7Receiver.Controler
{
    /// <summary>
    /// This class has been obsoleted.
    /// </summary>
    [Obsolete("Please use the HYS.IM.Common.HL7v2.Encoding.HL7MessageParser/HL7MessageTemplate instead", true)]
    public class HL7MessageHelper
    {
        public const string SuccessResponse =
            @"MSH|^~\&|<%MSH-5%>|<%MSH-6%>|<%MSH-3%>|<%MSH-4%>|20091209175006||ACK|10|P|2.3" +
            "\r\n" + @"MSA|AA|<%MSH-10%>|";
        public const string ErrorResponse =
            @"MSH|^~\&|<%MSH-5%>|<%MSH-6%>|<%MSH-3%>|<%MSH-4%>|20091209175006||ACK|10|P|2.3" +
            "\r\n" + @"MSA|AE|<%MSH-10%>|Cannot process this HL7 message in current application configuraiton.|";
        public const string Q23QueryResult =
            @"MSH|^~\&|<%MSH-5%>|<%MSH-6%>|<%MSH-3%>|<%MSH-4%>|20050801073008-0600||RSP^K23^RSP_K23|1|P|2.5|" +
            "\r\n" + @"MSA|AA|<%MSH-10%>|" +
            "\r\n" + @"QAK|111069|OK|Q23^Get Corresponding IDs^HL70471|1|" +
            "\r\n" + @"QPD|Q23^Get Corresponding IDs^HL70471|111069|112234^^^METRO HOSPITAL|^^^WEST CLINIC~^^^SOUTH LAB|" +
            "\r\n" + @"PID|||56321A^^^WEST CLINIC~66532^^^SOUTH LAB||~^^^^^^S|";

        private const char SegmentEnding = '\r';
        private const char FieldSeperator = '|';
        private const string FieldTagStart = "<%";
        private const string FieldTagEnd = "%>";
        private const char FieldTagSpliter = '-';

        public static string GetField(string message, string segment, int field)
        {
            if (message == null || message.Length < 1) return "";
            if (segment == null || segment.Length < 1) return "";
            if (field < 0) return "";

            int segStartIndex = message.IndexOf(segment);
            if (segStartIndex < 0) return "";
            int segEndIndex = message.IndexOf(SegmentEnding);
            if (segEndIndex < 0) segEndIndex = message.Length - 1;
            string segContent = message.Substring(segStartIndex, segEndIndex - segStartIndex);
            string[] fields = segContent.Split(FieldSeperator);
            if (field >= fields.Length) return "";
            return fields[field];
        }
        public static string FormatResponseMessage(string rqMessage, string rspMessageTemplate)
        {
            if (rqMessage == null || rqMessage.Length < 1) return "";
            if (rspMessageTemplate == null || rspMessageTemplate.Length < 1) return "";

            Dictionary<string, string> dicTagValue = new Dictionary<string, string>();

            int index = 0;
            while (index < rspMessageTemplate.Length)
            {
                int tagStart = rspMessageTemplate.IndexOf(FieldTagStart, index);
                if (tagStart < 0) break;
                index = tagStart + FieldTagStart.Length;
                int tagEnd = rspMessageTemplate.IndexOf(FieldTagEnd, index);
                if (tagEnd < 0) break;
                index = tagEnd = tagEnd + FieldTagEnd.Length;

                string tag = rspMessageTemplate.Substring(tagStart, tagEnd - tagStart);
                string tagContent = tag.Substring(FieldTagStart.Length, tag.Length - FieldTagStart.Length - FieldTagEnd.Length);
                string[] tagContentList = tagContent.Split(FieldTagSpliter);
                if (tagContentList.Length != 2) break;

                int intFld = -1;
                string seg = tagContentList[0];
                string fld = tagContentList[1];
                if (!int.TryParse(fld, out intFld)) break;

                string value = GetField(rqMessage, seg, intFld);
                dicTagValue.Add(tag, value);
            }

            string rspMessage = rspMessageTemplate;
            foreach (KeyValuePair<string, string> p in dicTagValue)
            {
                rspMessage = rspMessage.Replace(p.Key, p.Value);
            }

            return rspMessage;
        }
    }
}
