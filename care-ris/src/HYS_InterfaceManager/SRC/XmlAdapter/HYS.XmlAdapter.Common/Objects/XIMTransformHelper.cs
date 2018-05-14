using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;
using HYS.XmlAdapter.Common.Net;
using HYS.Adapter.Base;

namespace HYS.XmlAdapter.Common.Objects
{
    public class XIMTransformHelper
    {
        public static string GetTransactionID()
        {
            return "GATEWAY2_" + Guid.NewGuid().ToString();
        }
        public static void RefreshXSLFileName(XIMMessage message)
        {
            if (message is XIMInboundMessage)
            {
                message.XSLFileName = message.HL7EventType.GetKey() + "_to_" + message.GWEventType.Code + ".xsl";
            }
            else if (message is XIMOutboundMessage)
            {
                message.XSLFileName = message.GWEventType.Code + "_to_" + message.HL7EventType.GetKey() + ".xsl";
            }
        }

        #region Generate XSL Script

        public const string XSLFolder = "XSL";
        public const string DataSetXPathRoot = "/NewDataSet/Table/";
        public const string TemplateFileNameIn = "XIMInTemplate.xsl";
        public const string TemplateFileNameOut = "XIMOutTemplate.xsl";
        public const string TransactionID = "[TransactionID]";

        private static string GetTypeTemplateForOutbound(XIMType type)
        {
            switch (type)
            {
                default: return null;
                case XIMType.ID:
                case XIMType.PNM:
                    {
                        return "TOut_" + type.ToString();
                    }
            }
        }
        private static string GetTypeTemplateForInbound(XIMType type)
        {
            switch (type)
            {
                default: return null;
                case XIMType.ID:
                case XIMType.PNM:
                    {
                        return "TIn_" + type.ToString();
                    }
            }
        }
        public static string GenerateOutboundTemplate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            sb.AppendLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">");

            sb.AppendLine();
            sb.AppendLine("<xsl:template name =\"" + GetTypeTemplateForOutbound(XIMType.ID) + "\">");
            sb.AppendLine(" <ID><xsl:value-of select =\"current()\"/></ID>");
            sb.AppendLine("</xsl:template>");

            ////sb.AppendLine();
            ////sb.AppendLine("<xsl:template name =\"" + GetTypeTemplateForOutbound(XIMType.PNM) + "\">");
            ////sb.AppendLine(" <LAST><xsl:value-of select =\"substring-after(current(),'^')\"/></LAST>");
            ////sb.AppendLine(" <FIRST><xsl:value-of select =\"substring-before(current(),'^')\"/></FIRST>");
            ////sb.AppendLine("</xsl:template>");

            //sb.AppendLine();
            //sb.AppendLine("<xsl:template name =\"" + GetTypeTemplateForOutbound(XIMType.PNM) + "\">");
            //sb.AppendLine("<xsl:choose>");
            //sb.AppendLine(" <xsl:when test = \"contains(current(),'^')\">");
            //sb.AppendLine("  <LAST>");
            //sb.AppendLine("   <xsl:if test = \"string-length(substring-after(current(),'^'))=0\">");
            //sb.AppendLine("    <xsl:value-of select =\"concat('',' ')\"/>");
            //sb.AppendLine("   </xsl:if>");
            //sb.AppendLine("   <xsl:if test = \"string-length(substring-after(current(),'^'))!=0\">");
            //sb.AppendLine("    <xsl:value-of select =\"substring-before(concat(substring-after(current(),'^'),'^'),'^')\"/>");
            //sb.AppendLine("   </xsl:if>");
            //sb.AppendLine("  </LAST>");
            //sb.AppendLine("  <FIRST>");
            //sb.AppendLine("   <xsl:value-of select =\"substring-before(current(),'^')\"/>");
            //sb.AppendLine("  </FIRST>");
            //sb.AppendLine(" </xsl:when>");
            //sb.AppendLine(" <xsl:otherwise>");
            //sb.AppendLine("  <LAST> </LAST><FIRST><xsl:value-of select =\"current()\"/></FIRST>");
            //sb.AppendLine(" </xsl:otherwise>");
            //sb.AppendLine("</xsl:choose>");
            //sb.AppendLine("</xsl:template>");

            sb.AppendLine();
            sb.AppendLine("<xsl:template name =\"" + GetTypeTemplateForOutbound(XIMType.PNM) + "\">");
            sb.AppendLine("<xsl:choose>");
            sb.AppendLine(" <xsl:when test = \"contains(current(),'^')\">");
            sb.AppendLine("  <LAST>");
            sb.AppendLine("   <xsl:value-of select =\"substring-before(current(),'^')\"/>");
            sb.AppendLine("  </LAST>");
            sb.AppendLine("  <FIRST>");
            sb.AppendLine("   <xsl:if test = \"string-length(substring-after(current(),'^'))=0\">");
            sb.AppendLine("    <xsl:value-of select =\"concat('',' ')\"/>");
            sb.AppendLine("   </xsl:if>");
            sb.AppendLine("   <xsl:if test = \"string-length(substring-after(current(),'^'))!=0\">");
            sb.AppendLine("    <xsl:value-of select =\"substring-before(concat(substring-after(current(),'^'),'^'),'^')\"/>");
            sb.AppendLine("   </xsl:if>");
            sb.AppendLine("  </FIRST>");
            sb.AppendLine(" </xsl:when>");
            sb.AppendLine(" <xsl:otherwise>");
            sb.AppendLine("  <LAST> </LAST><FIRST><xsl:value-of select =\"current()\"/></FIRST>");
            sb.AppendLine(" </xsl:otherwise>");
            sb.AppendLine("</xsl:choose>");
            sb.AppendLine("</xsl:template>");

            sb.AppendLine();
            sb.AppendLine("</xsl:stylesheet>");
            return sb.ToString();
        }
        public static string GenerateInboundTemplate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            sb.AppendLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">");

            sb.AppendLine();
            sb.AppendLine("<xsl:template name =\"" + GetTypeTemplateForInbound(XIMType.ID) + "\">");
            sb.AppendLine(" <xsl:value-of select =\"ID\"/>");
            sb.AppendLine("</xsl:template>");

            sb.AppendLine();
            sb.AppendLine("<xsl:template name =\"" + GetTypeTemplateForInbound(XIMType.PNM) + "\">");
            sb.Append(" <xsl:value-of select =\"LAST\"/>^")
                .AppendLine("<xsl:value-of select =\"FIRST\"/>");
            sb.AppendLine("</xsl:template>");

            sb.AppendLine();
            sb.AppendLine("</xsl:stylesheet>");
            return sb.ToString();
        }

        private static string GetMultiItemSelection(XmlElement ele, string xPath, string prefix)
        {
            if (ele == null) return null;
            StringBuilder sb = new StringBuilder();
            if (xPath == null || xPath.Length < 1)
            {
                string tName = GetTypeTemplateForInbound(ele.Type);
                if (tName == null)
                {
                    sb.Append(prefix);
                    sb.AppendLine("<xsl:value-of select =\"current()\"/>");
                }
                else
                {
                    sb.Append(prefix);
                    sb.AppendLine("<xsl:call-template name =\"" + tName + "\">");
                    sb.Append(prefix);
                    sb.AppendLine("</xsl:call-template>");
                }
            }
            else
            {
                int index = xPath.IndexOf(XmlElement.MultiItemSymbol);
                if (index <= 0)
                {
                    string tName = GetTypeTemplateForInbound(ele.Type);
                    if (tName == null)
                    {
                        sb.Append(prefix);
                        sb.AppendLine("<xsl:value-of select=\"" + xPath + "\"/>");
                    }
                    else
                    {
                        sb.Append(prefix);
                        sb.AppendLine("<xsl:for-each select =\"" + xPath + "\">");
                        sb.Append(prefix + "  ");
                        sb.AppendLine("<xsl:call-template name =\"" + tName + "\">");
                        sb.Append(prefix + "  ");
                        sb.AppendLine("</xsl:call-template>");
                        sb.Append(prefix);
                        sb.AppendLine("</xsl:for-each>");
                    }
                }
                else
                {
                    string itemPath = xPath.Substring(0, index);

                    sb.Append(prefix);
                    sb.AppendLine("<xsl:for-each select =\"" + itemPath + "\">");

                    string subPath = "";
                    index += XmlElement.MultiItemSymbol.Length + 1;
                    if (index < xPath.Length) subPath = xPath.Substring(index);
                    sb.Append(GetMultiItemSelection(ele, subPath, prefix + "  "));
                    //if (subPath.Length < 1) sb.Append("/");

                    sb.Append(prefix);
                    sb.AppendLine("</xsl:for-each>");
                }
            }
            return sb.ToString();
        }
        public static string GenerateXSL(XIMInboundMessage message)
        {
            if (message == null) return null;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            sb.AppendLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">");
            sb.AppendLine("  <xsl:include href = \"" + TemplateFileNameIn + "\"/>");
            sb.AppendLine("  <xsl:template match=\"/\">");
            sb.AppendLine("    <NewDataSet>");
            sb.AppendLine("        <Table>");

            foreach (XIMMappingItem item in message.MappingList)
            {
                string nodeName = item.SourceField;
                if (item.Translating.Type == TranslatingType.FixValue)
                {
                    sb.Append("          <" + nodeName + ">");
                    sb.Append(item.Translating.ConstValue);
                    sb.AppendLine("</" + nodeName + ">");
                }
                else
                {
                    sb.AppendLine("          <" + nodeName + ">");
                    sb.Append(GetMultiItemSelection(item.Element, item.Element.XPath, "            "));
                    sb.AppendLine("          </" + nodeName + ">");
                }
            }

            sb.AppendLine("        </Table>");
            sb.AppendLine("    </NewDataSet>");
            sb.AppendLine("  </xsl:template>");
            sb.AppendLine("</xsl:stylesheet>");

            return sb.ToString();
        }

        private static XmlNode CreateNode(XmlDocument doc, XmlNode node, string xpath)
        {
            if (doc == null || node == null || xpath == null || xpath.Length < 1) return null;

            string currentPath = xpath.TrimStart(XmlElement.XPathSeperator);
            int index = currentPath.IndexOf(XmlElement.XPathSeperator);
            if (index < 0) index = currentPath.Length;

            string nodeName = currentPath.Substring(0, index);
            string newPath = currentPath.Substring(index, currentPath.Length - index);
            if (nodeName.Length < 1) return null;

            XmlNode snode = node.SelectSingleNode(nodeName);
            if (snode == null)
            {
                snode = doc.CreateNode(XmlNodeType.Element, nodeName, "");
                node.AppendChild(snode);
            }

            if (newPath.Length > 0)
            {
                return CreateNode(doc, snode, newPath);
            }
            else
            {
                return snode;
            }
        }
        private static void SetXIMHeader(XmlDocument doc, XIMOutboundMessage message, SocketConfig config)
        {
            if (doc == null || message == null || config == null) return;

            XmlNode node = null;

            node = CreateNode(doc, doc, XmlMessage.Request.XISVersion.GetXPath());
            if (node != null) node.InnerText = "3.0";

            node = CreateNode(doc, doc, XmlMessage.Request.Name.GetXPath());
            if (node != null) node.InnerText = message.HL7EventType.Name;

            node = CreateNode(doc, doc, XmlMessage.Request.Qualifier.GetXPath());
            if (node != null) node.InnerText = message.HL7EventType.Qualifier;

            node = CreateNode(doc, doc, XmlMessage.Request.OriginatingDevice.GetXPath());
            if (node != null) node.InnerText = config.SourceDeviceName;

            node = CreateNode(doc, doc, XmlMessage.Request.TransactionID.GetXPath());
            if (node != null) node.InnerText = TransactionID;   // GetTransactionID();

            node = CreateNode(doc, doc, XmlMessage.Request.TargetDevice.GetXPath());
            if (node != null) node.InnerText = config.TargetDeviceName;
        }
        public static string GenerateXSL(XIMOutboundMessage message, SocketConfig config)
        {
            if (message == null) return null;

            XmlDocument doc = new XmlDocument();
            SetXIMHeader(doc, message, config);

            foreach (XIMMappingItem item in message.MappingList)
            {
                string xpath = item.Element.GetXPath();
                XmlNode node = CreateNode(doc, doc, xpath);
                if (node == null) continue;

                if (item.Translating.Type == TranslatingType.FixValue)
                {
                    node.InnerText = item.Translating.ConstValue;
                }
                else
                {
                    string tName = GetTypeTemplateForOutbound(item.Element.Type);
                    string xPath = DataSetXPathRoot + item.TargetField;
                    if (tName == null)
                    {
                        node.InnerXml = "<xsl_value-of select=\"" + xPath + "\"/>";
                    }
                    else
                    {
                        StringBuilder sbb = new StringBuilder();
                        sbb.Append("<xsl_for-each select =\"" + xPath + "\">");
                        sbb.Append("<xsl_call-template name =\"" + tName + "\">");
                        sbb.Append("</xsl_call-template>");
                        sbb.Append("</xsl_for-each>");
                        node.InnerXml = sbb.ToString();
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            sb.AppendLine("<xsl:stylesheet version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\">");
            sb.AppendLine("  <xsl:include href = \"" + TemplateFileNameOut + "\"/>");
            sb.AppendLine("  <xsl:template match=\"/\">");
            //StringBuilder sbxml = new StringBuilder();
            //StringWriter sw = new StringWriter(sbxml);
            //XmlTextWriter xtw = new XmlTextWriter(sw);
            //doc.WriteTo(xtw);
            //sb.AppendLine(sbxml.ToString().Replace("<xsl_", "<xsl:").Replace("</xsl_", "</xsl:"));
            sb.AppendLine(doc.OuterXml.Replace("<xsl_", "<xsl:").Replace("</xsl_", "</xsl:"));
            sb.AppendLine("  </xsl:template>");
            sb.AppendLine("</xsl:stylesheet>");
            string str = sb.ToString();

            str = str.Replace("<XMLRequestMessage>", "<XMLRequestMessage SchemaVersion=\"2.0\">");
            str = str.Replace("<XIM>", "<XIM XIMSchemaVersion=\"2.0\">");
            return str;
        }

        #endregion

        #region Logging Helper
        private static Logging _log;
        public static void EnableXSLTLogging(Logging log)
        {
            _log = log;
            XMLTransformer.OnError += new EventHandler(XMLTransformer_OnError);
        }
        private static void XMLTransformer_OnError(object sender, EventArgs e)
        {
            _log.Write(XMLTransformer.LastError, "XSLT");
        }
        #endregion
    }
}
