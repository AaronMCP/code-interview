using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Common.HL7v2.Encoding
{
    /// <summary>
    /// Use pure string processing method to parse and generate HL7 message.
    /// </summary>
    public class HL7MessageParser
    {
        private const char SegmentEnding = '\r';
        private const char FieldSeperator = '|';
        private const char FieldValueSeperator = '~';
        private const char ComponentSeperator = '^';
        private const char SubComponentSeperator = '&';

        private const string HeaderSegment = "MSH";

        private const string FieldTagStart = "<%";
        private const string FieldTagEnd = "%>";
        private const char FieldTagSpliter = '-';

        /// <summary>
        /// Return full segement string without SegmentEnding.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="segment"></param>
        /// <returns></returns>
        public static string GetSegment(string message, string segment)
        {
            if (message == null || message.Length < 1) return "";
            if (segment == null || segment.Length < 1) return "";

            int segStartIndex = message.IndexOf(segment);
            if (segStartIndex < 0) return "";
            int segEndIndex = message.IndexOf(SegmentEnding, segStartIndex + segment.Length);
            if (segEndIndex < 0) segEndIndex = message.Length - 1;

            string seg = message.Substring(segStartIndex, segEndIndex - segStartIndex);
            return seg;
        }
        /// <summary>
        /// The field parameter range from 1 to infinit.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="segment"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string GetField(string message, string segment, int field)
        {
            if (message == null || message.Length < 1) return "";
            if (segment == null || segment.Length < 1) return "";
            if (field < 0) return "";

            int segStartIndex = message.IndexOf(segment);
            if (segStartIndex < 0) return "";
            segStartIndex += segment.Length;
            if (segStartIndex >= message.Length) return "";
            int segEndIndex = message.IndexOf(SegmentEnding, segStartIndex);
            if (segEndIndex < 0) segEndIndex = message.Length - 1;
            string segContent = message.Substring(segStartIndex, segEndIndex - segStartIndex);
            string[] fields = segContent.Split(FieldSeperator);

            int f = field;
            if (segment == HeaderSegment) f = f - 1;
            if (f >= fields.Length) return "";
            return fields[f];
        }
        public static int GetFieldValueCount(string field)
        {
            if (field == null || field.Length < 1) return 0;
            string[] values = field.Split(FieldValueSeperator);
            if (values.Length <= 0) return 0;
            return values.Length - 1;
        }
        public static string[] GetFieldValues(string field)
        {
            if (field == null || field.Length < 1) return new string[] { };
            return field.Split(FieldValueSeperator);
        }
        /// <summary>
        /// The index parameter range from 1 to infinit.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetFieldValue(string field, int index)
        {
            if (field == null || field.Length < 1) return "";

            int i = index - 1;
            if (i < 0) return "";

            string[] values = field.Split(FieldValueSeperator);
            if (i >= values.Length) return "";
            return values[i];
        }
        /// <summary>
        /// The component parameter range from 1 to infinit.
        /// </summary>
        /// <param name="fieldvalue"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static string GetComponent(string fieldvalue, int component)
        {
            if (fieldvalue == null || fieldvalue.Length < 1) return "";

            int index = component - 1;
            if (index < 0) return "";

            string[] coms = fieldvalue.Split(ComponentSeperator);
            if (index >= coms.Length) return "";
            return coms[index];
        }
        /// <summary>
        /// The subcomponent parameter range from 1 to infinit.
        /// </summary>
        /// <param name="fieldvalue"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static string GetSubComponent(string component, int subcomponent)
        {
            if (component == null || component.Length < 1) return "";

            int index = subcomponent - 1;
            if (index < 0) return "";

            string[] subs = component.Split(SubComponentSeperator);
            if (index >= subs.Length) return "";
            return subs[index];
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

        /// <summary>
        /// The field parameter range from 1 to infinit.
        /// This method do not create new segment or field in the message, 
        /// it only update value of exsiting field in exsiting segment.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="segment"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string SetField(string message, string segment, int field, string value)
        {
            if (message == null || message.Length < 1) return "";
            if (segment == null || segment.Length < 1) return "";
            if (value == null) return "";
            if (field < 1) return "";

            int f = field;
            if (segment == HeaderSegment) f = f - 1;

            int segStartIndex = message.IndexOf(segment);
            if (segStartIndex < 0) return "";

            segStartIndex = segStartIndex + segment.Length;
            if (segStartIndex >= message.Length) return "";

            int fieldStartIndex = segStartIndex;
            for (int i = 1; i <= f; i++)
            {
                fieldStartIndex = message.IndexOf(FieldSeperator, fieldStartIndex);
                if (fieldStartIndex < 0) return "";

                fieldStartIndex = fieldStartIndex + 1;
                if (fieldStartIndex >= message.Length) return message + value;
            }

            int fieldEndIndex = message.IndexOf(FieldSeperator, fieldStartIndex);
            if (fieldEndIndex < 0) fieldEndIndex = fieldStartIndex;

            int segEndIndex = message.IndexOf(SegmentEnding, fieldStartIndex);
            if (segEndIndex >= 0 && segEndIndex < fieldEndIndex) fieldEndIndex = segEndIndex;

            string msg = message;
            if (fieldEndIndex > fieldStartIndex)
            {
                msg = msg.Remove(fieldStartIndex, fieldEndIndex - fieldStartIndex);
            }
            msg = msg.Insert(fieldStartIndex, value);
            return msg;
        }
        /// <summary>
        /// This method do not create new segment in the message, 
        /// it only update value of exsiting exsiting segment.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="segment"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string SetSegment(string message, string segment, string value)
        {
            if (message == null || message.Length < 1) return "";
            if (segment == null || segment.Length < 1) return "";
            if (value == null || value.Length < 1) return "";

            int segStartIndex = message.IndexOf(segment);
            if (segStartIndex < 0) return "";
            int segEndIndex = message.IndexOf(SegmentEnding, segStartIndex + segment.Length);
            if (segEndIndex < 0) segEndIndex = message.Length - 1;

            string msg = message;
            if (segEndIndex > segStartIndex)
            {
                msg = msg.Remove(segStartIndex, segEndIndex - segStartIndex);
            }
            msg = msg.Insert(segStartIndex, value);
            return msg;
        }
    }
}
