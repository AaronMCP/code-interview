using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using System.Collections.Generic;
using HYS.XmlAdapter.Common.Net;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Inbound.Objects;
using HYS.XmlAdapter.Inbound.Adapters;

namespace HYS.XmlAdapter.Inbound.Controlers
{
    public class XIMServerHelper
    {
        #region XIM protocol

        private static string _requestBeginSign;
        public static string RequestBeginSign
        {
            get
            {
                if (_requestBeginSign == null)
                {
                    _requestBeginSign = "<XMLRequestMessage";
                }
                return _requestBeginSign;
            }
        }

        private static string _requestEndSign;
        public static string RequestEndSign
        {
            get
            {
                if (_requestEndSign == null)
                {
                    _requestEndSign = "</XMLRequestMessage>";
                }
                return _requestEndSign;
            }
        }

        private static string _responseEndSign;
        public static string ResponseEndSign
        {
            get
            {
                if (_responseEndSign == null)
                {
                    _responseEndSign = "</XMLResponseMessage>";
                }
                return _responseEndSign;
            }
        }

        //private static string _responseHeader;
        //public static string ResponseHeader
        //{
        //    get
        //    {
        //        if (_responseHeader == null)
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            //sb.AppendLine("POST /HYS-EC HTTP/1.1");
        //            //sb.AppendLine("Content-Type: text/xml");
        //            //sb.AppendLine("Content-Length: 1024");
        //            //sb.AppendLine("SOAPAction: \"\"");
        //            //sb.AppendLine("Host: localhost:" + Program.ConfigMgt.Config.SocketConfig.Port.ToString());
        //            //sb.AppendLine("Pragma: no-cache");
        //            //sb.AppendLine();

        //            sb.AppendLine("HTTP/1.1 200 OK");
        //            sb.AppendLine("Server: XIM(HL7) Adapter of GC Gateway 2.0");
        //            sb.AppendLine("Content-Type: text/xml");
        //            sb.AppendLine("Content-Length: 1024");
        //            sb.AppendLine("SOAPAction: ");
        //            sb.AppendLine("Host: ");
        //            sb.AppendLine();

        //            _responseHeader = sb.ToString();
        //        }
        //        return _responseHeader;
        //    }
        //}

        public static string GetResponseSuccess(string transactionID, SocketConfig config)
        {
            //StringBuilder sb = new StringBuilder();

            //if (Program.ConfigMgt.Config.IncludeResponseHeader)
            //{
            //    sb.Append(ResponseHeader);
            //}

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<XMLResponseMessage SchemaVersion=\"1.0\">");
            sb.AppendLine("	<XISVersion>3.0.</XISVersion>");
            sb.AppendLine("	<Name>EVENT_COMPLETE</Name>");
            sb.AppendLine("	<TransactionID>" + transactionID + "</TransactionID>");
            sb.AppendLine("	<Status>");
            sb.AppendLine("		<Code>SUCCESS</Code>");
            sb.AppendLine("		<FailureCode/>");
            sb.AppendLine("		<Message/>");
            sb.AppendLine("	</Status>");
            sb.AppendLine("	<DeviceResponse>");
            sb.AppendLine("		<Device>" + config.SourceDeviceName + "</Device>");
            sb.AppendLine("		<Status>");
            sb.AppendLine("			<Code>SUCCESS</Code>");
            sb.AppendLine("			<Message>The event was responded to successfully</Message>");
            sb.AppendLine("		</Status>");
            sb.AppendLine("	</DeviceResponse>");
            sb.AppendLine("	<XIM XIMSchemaVersion=\"2.0\">");
            sb.AppendLine("	</XIM>");
            sb.Append("</XMLResponseMessage>");

            //string msg = sb.ToString();
            //string header = ResponseHeader;
            //header = header.Replace("1024", msg.Length.ToString());
            //return header + msg;

            return sb.ToString();
        }
        public static string GetResponseFailed(string transactionID, string message, SocketConfig config)
        {
            StringBuilder sb = new StringBuilder();

            //if (Program.ConfigMgt.Config.IncludeResponseHeader)
            //{
            //    sb.Append(ResponseHeader);
            //}

            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<XMLResponseMessage SchemaVersion=\"1.0\">");
            sb.AppendLine("	<XISVersion>3.0.</XISVersion>");
            sb.AppendLine("	<Name>EVENT_COMPLETE</Name>");
            sb.AppendLine("	<TransactionID>" + transactionID + "</TransactionID>");
            sb.AppendLine("	<Status>");
            sb.AppendLine("		<Code>GENERAL_FAILURE</Code>");
            sb.AppendLine("		<FailureCode>ERROR_PARSING_REQUEST</FailureCode>");
            sb.AppendLine("		<Message>" + message + "</Message>");
            sb.AppendLine("	</Status>");
            sb.AppendLine("	<DeviceResponse>");
            sb.AppendLine("		<Device>" + config.SourceDeviceName + "</Device>");
            sb.AppendLine("		<Status>");
            sb.AppendLine("			<Code>CANCELED_TRANSACTION</Code>");
            sb.AppendLine("			<Message>" + message + "</Message>");
            sb.AppendLine("		</Status>");
            sb.AppendLine("	</DeviceResponse>");
            sb.AppendLine("	<XIM XIMSchemaVersion=\"2.0\">");
            sb.AppendLine("	</XIM>");
            sb.Append("</XMLResponseMessage>");

            return sb.ToString();
        }

        public static string GetMessageContent(string receiveData)
        {
            if (receiveData == null) return "";
            int index = receiveData.IndexOf(RequestBeginSign);
            if (index < 0) return "";
            return receiveData.Substring(index, receiveData.Length - index);
        }

        #endregion

        #region XIM process

        public const string ModuleName = "XIMServer";

        private const string NAME = "/XMLRequestMessage/Name";
        private const string QUALIFIER = "/XMLRequestMessage/Qualifier";
        private const string TRANSACTIONID = "/XMLRequestMessage/TransactionID";
        private const string XIM = "/XMLRequestMessage/XIM";
        private const string ITEM = "/XMLRequestMessage/XIM/ITEM";
        private const string SCHEDULED_PROCEDURE_STEP = "/XMLRequestMessage/XIM/ITEM/SCHEDULED_PROCEDURE_STEP";

        public static XIMInboundMessage GetMessageConfiguration(string xmlString, ref string strName, ref string strQualifier, ref string transactionID)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            XmlNode nodeName = xmlDoc.SelectSingleNode(NAME);
            if (nodeName != null) strName = nodeName.InnerText;

            XmlNode nodeQualifier = xmlDoc.SelectSingleNode(QUALIFIER);
            if (nodeQualifier != null) strQualifier = nodeQualifier.InnerText;

            XmlNode nodeTransactionID = xmlDoc.SelectSingleNode(TRANSACTIONID);
            if (nodeTransactionID != null) transactionID = nodeTransactionID.InnerText;

            foreach (XIMInboundMessage msg in Program.ConfigMgt.Config.Messages)
            {
                if (msg.HL7EventType.Name == strName && 
                    msg.HL7EventType.Qualifier == strQualifier)
                    return msg;
            }

            return null;
        }
        public static void SplitItem(List<XmlNode> nodeList, string xmlString)
        {
            if (nodeList == null) return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            XmlNodeList itemNodeList = xmlDoc.SelectNodes(ITEM);
            if (itemNodeList == null || itemNodeList.Count < 2)
            {
                nodeList.Add(xmlDoc);
            }
            else
            {
                for (int i = 0; i < itemNodeList.Count; i++)
                {
                    XmlNode newDoc = xmlDoc.Clone();
                    XmlNode newXimNode = newDoc.SelectSingleNode(XIM);
                    for (int j = 0; j < newXimNode.ChildNodes.Count; j++)
                    {
                        if (i == j) continue;
                        newXimNode.RemoveChild(newXimNode.ChildNodes[j]);
                    }
                    nodeList.Add(newDoc);
                }
            }
        }
        public static void SplitProcedureStep(List<XmlNode> nodeList)
        {
            if (nodeList == null) return;

            List<XmlNode> tempList = new List<XmlNode>();
            foreach (XmlNode node in nodeList) tempList.Add(node);

            foreach (XmlNode node in tempList)
            {
                XmlNodeList spsNodeList = node.SelectNodes(SCHEDULED_PROCEDURE_STEP);
                if (spsNodeList == null || spsNodeList.Count < 2) continue;

                int index = nodeList.IndexOf(node);
                if (index < 0) continue;

                nodeList.RemoveAt(index);
                for (int i = spsNodeList.Count - 1; i >= 0; i--)
                {
                    XmlNode newNode = node.Clone();
                    XmlNode newItemNode = newNode.SelectSingleNode(ITEM);
                    XmlNodeList newSpsNodeList = newItemNode.SelectNodes(SCHEDULED_PROCEDURE_STEP);
                    for (int j = 0; j < newSpsNodeList.Count; j++)
                    {
                        if (i == j) continue;
                        newItemNode.RemoveChild(newSpsNodeList[j]);
                    }
                    nodeList.Insert(index, newNode);
                }
            }
        }
        public static string[] Transform(XMLTransformer transformer, List<XmlNode> nodeList)
        {
            if (transformer == null || nodeList == null) return null;
            List<string> list = new List<string>();
            foreach (XmlNode node in nodeList)
            {
                string outXML = "";
                string inXML = node.OuterXml;
                if (transformer.TransformString(inXML, ref outXML)) list.Add(outXML);
            }
            return list.ToArray();
        }
        public static DataSet CreateDataSet(string[] dataSetXmlList)
        {
            if( dataSetXmlList== null ) return null;

            List<DataSet> dsList = new List<DataSet>();
            foreach (string xml in dataSetXmlList)
            {
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xml));
                dsList.Add(ds);
            }

            DataSet dsResult = new DataSet();
            foreach (DataSet ds in dsList) dsResult.Merge(ds);
            return dsResult;
        }

        #endregion
    }
}
