using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.XmlAdapter.Common.Net;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XIMMappingHelper
    {
        private static void GenerateSourceFieldName(MappingItem item)
        {
            string fieldName = item.GWDataDBField.GetFullFieldName();
            item.SourceField = fieldName.Replace('.', '_');
        }
        private static void GenerateTargetFieldName(MappingItem item)
        {
            string fieldName = item.GWDataDBField.GetFullFieldName();
            item.TargetField = fieldName.Replace('.', '_');
        }
        private static void GenerateSourceFieldName(XIMInboundMessage message)
        {
            if (message == null) return;
            foreach (XIMMappingItem item in message.MappingList)
            {
                if (item.Enable && !XIMHelper.IsComplex(item.Element.Type))
                {
                    GenerateSourceFieldName(item);
                }
            }
        }
        private static void GenerateTargetFieldName(XIMOutboundMessage message)
        {
            if (message == null) return;
            foreach (XIMMappingItem item in message.MappingList)
            {
                if (item.Enable && !XIMHelper.IsComplex(item.Element.Type))
                {
                    GenerateTargetFieldName(item);
                }
            }
        }
        public static void GenerateSourceFieldName(XCollection<XIMInboundMessage> messages)
        {
            foreach (XIMInboundMessage msg in messages)
            {
                GenerateSourceFieldName(msg);
            }
        }
        public static void GenerateTargetFieldName(XCollection<XIMOutboundMessage> messages)
        {
            foreach (XIMOutboundMessage msg in messages)
            {
                GenerateTargetFieldName(msg);
            }
        }

        private static void ClearMapping(XIMMessage message)
        {
            if (message == null) return;

            List<XIMMappingItem> deletelist = new List<XIMMappingItem>();
            foreach (XIMMappingItem item in message.MappingList)
            {
                if (item.Enable && !XIMHelper.IsComplex(item.Element.Type)) continue;
                deletelist.Add(item);
            }

            foreach (XIMMappingItem item in deletelist)
            {
                message.MappingList.Remove(item);
            }
        }
        private static void AddEventTypeItem(XIMMessage message)
        {
            if (message is XIMInboundMessage)
            {
                if (message == null) return;
                XIMMappingItem item = new XIMMappingItem();
                item.GWDataDBField = GWDataDBField.i_EventType.Clone();
                item.Translating.ConstValue = message.GWEventType.Code;
                item.Translating.Type = TranslatingType.FixValue;
                GenerateSourceFieldName(item);
                message.MappingList.Add(item);
            }
            else if (message is XIMOutboundMessage)
            {
                if (message == null) return;
                QueryCriteriaItem item = new QueryCriteriaItem();
                item.GWDataDBField = GWDataDBField.i_EventType.Clone();
                item.Translating.ConstValue = message.GWEventType.Code;
                item.Translating.Type = TranslatingType.FixValue;
                item.Type = QueryCriteriaType.And;
                GenerateTargetFieldName(item);
                ((XIMOutboundMessage)message).Rule.QueryCriteria.MappingList.Add(item);
            }
        }
        public static void ClearMapping(XCollection<XIMInboundMessage> messages)
        {
            foreach (XIMInboundMessage msg in messages)
            {
                ClearMapping(msg);
                AddEventTypeItem(msg);
            }
        }
        public static void ClearMapping(XCollection<XIMOutboundMessage> messages)
        {
            foreach (XIMOutboundMessage msg in messages)
            {
                ClearMapping(msg);
                AddEventTypeItem(msg);
            }
        }

        public static void SaveXSLFiles(XCollection<XIMInboundMessage> messages, SocketConfig config)
        {
            if (messages == null) return;

            string path = Application.StartupPath + "\\" + XIMTransformHelper.XSLFolder;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string tmpFileName = path + "\\" + XIMTransformHelper.TemplateFileNameIn;
            if (!File.Exists(tmpFileName))
            {
                using (StreamWriter sw = File.CreateText(tmpFileName))
                {
                    string tmpString = XIMTransformHelper.GenerateInboundTemplate();
                    sw.Write(tmpString);
                }
            }

            foreach (XIMInboundMessage msg in messages)
            {
                string fileName = path + "\\" + msg.XSLFileName;
                string xslString = XIMTransformHelper.GenerateXSL(msg);
                if (xslString == null) continue;

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.Write(xslString);
                }
            }
        }
        public static void SaveXSLFiles(XCollection<XIMOutboundMessage> messages, SocketConfig config)
        {
            if (messages == null) return;

            string path = Application.StartupPath + "\\" + XIMTransformHelper.XSLFolder;
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string tmpFileName = path + "\\" + XIMTransformHelper.TemplateFileNameOut;
            if (!File.Exists(tmpFileName))
            {
                using (StreamWriter sw = File.CreateText(tmpFileName))
                {
                    string tmpString = XIMTransformHelper.GenerateOutboundTemplate();
                    sw.Write(tmpString);
                }
            }

            foreach (XIMOutboundMessage msg in messages)
            {
                string fileName = path + "\\" + msg.XSLFileName;
                string xslString = XIMTransformHelper.GenerateXSL(msg, config);
                if (xslString == null) continue;

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.Write(xslString);
                }
            }
        }
    }
}
