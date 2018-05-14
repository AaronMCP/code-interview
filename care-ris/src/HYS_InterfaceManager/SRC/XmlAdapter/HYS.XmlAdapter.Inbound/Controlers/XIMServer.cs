using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Collections.Generic;
using HYS.XmlAdapter.Common;
using HYS.XmlAdapter.Common.Net;
using HYS.XmlAdapter.Common.Files;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Inbound.Objects;
using HYS.XmlAdapter.Inbound.Adapters;
using HYS.Common.Objects.Logging;

namespace HYS.XmlAdapter.Inbound.Controlers
{
    public class XIMServer
    {
        private XIMServer()
        {
        }
        private IEntity _entity;
        private ServiceMain _service;
        
        public static XIMServer CreateXIMServer(ServiceMain service)
        {
            XIMServer s = new XIMServer();
            s._service = service;

            IEntity e = null;
            if (Program.ConfigMgt.Config.InboundFromFile)
            {
                e = new DirectoryMonitor(Program.ConfigMgt.Config.DirectoryConfig, Program.Log);
            }
            else
            {
                e = SocketEntity.Create(Program.ConfigMgt.Config.SocketConfig);
            }

            if (e == null)
            {
                return null;
            }
            else
            {
                e.OnRequest += new RequestEventHandler(s._entity_OnRequest);
                s._entity = e;
                return s;
            }
        }

        private bool _entity_OnRequest(string receiveData, ref string sendData)
        {
            //receiveData = SocketHelper.DeleteEOF(receiveData, _entity.Config.ReceiveEndSign);
            //sendData = receiveData.Replace('#', '$');
            //return;

            //using (StreamWriter sw = File.CreateText("d:\\msg.txt"))
            //{
            //    sw.Write(receiveData);
            //}

            string msgContent = XIMServerHelper.GetMessageContent(receiveData);
            if (msgContent == null || msgContent.Length < 1)
            {
                string strErrMsg = "Request message does not exist in request data";
                Program.Log.Write(LogType.Warning, strErrMsg, XIMServerHelper.ModuleName);
                sendData = XIMServerHelper.GetResponseFailed(XIMTransformHelper.GetTransactionID(), strErrMsg, Program.ConfigMgt.Config.SocketConfig);
                return false;
            }

            string strName = "";
            string strQualifier = "";
            string strTransactionID = "";
            XIMInboundMessage msgConfig = XIMServerHelper.GetMessageConfiguration(msgContent, ref strName, ref strQualifier, ref strTransactionID);
            if (msgConfig == null)
            {
                string strErrMsg = "Cannot find mapping rule configuration for current message (Name:" + strName + ", Qualifier:" + strQualifier + ").";
                Program.Log.Write(LogType.Warning, strErrMsg, XIMServerHelper.ModuleName);
                sendData = XIMServerHelper.GetResponseFailed(strTransactionID, strErrMsg, Program.ConfigMgt.Config.SocketConfig);
                return false;
            }

            XMLTransformer transformer = XMLTransformer.CreateFromMessage(msgConfig);
            if (msgConfig == null)
            {
                string strErrMsg = "Cannot find mapping XSL file for current message (XSL File:" + msgConfig.XSLFileName + ").";
                Program.Log.Write(LogType.Warning, strErrMsg, XIMServerHelper.ModuleName);
                sendData = XIMServerHelper.GetResponseFailed(strTransactionID, strErrMsg, Program.ConfigMgt.Config.SocketConfig);
                return false;
            }

            List<XmlNode> nodeList = new List<XmlNode>();
            XIMServerHelper.SplitItem(nodeList, msgContent);
            Program.Log.Write(nodeList.Count.ToString() + " XIM Item(s) are found in request message", XIMServerHelper.ModuleName);
            XIMServerHelper.SplitProcedureStep(nodeList);
            Program.Log.Write(nodeList.Count.ToString() + " XIM Item(s) splited by SPS are found in request message", XIMServerHelper.ModuleName);

            if (nodeList.Count > 0)
            {
                string[] dataSetXmlList = XIMServerHelper.Transform(transformer, nodeList);
                DataSet dataSet = XIMServerHelper.CreateDataSet(dataSetXmlList);
                if (dataSet == null)
                {
                    string strErrMsg = "Failed to transform to Data Set for current message.";
                    Program.Log.Write(LogType.Warning, strErrMsg, XIMServerHelper.ModuleName);
                    sendData = XIMServerHelper.GetResponseFailed(strTransactionID, strErrMsg, Program.ConfigMgt.Config.SocketConfig);
                    return false;
                }

                if (_service.SaveData(msgConfig.Rule, dataSet))
                {
                    Program.Log.Write("Save to GC Gateway database succeeded.");
                    sendData = XIMServerHelper.GetResponseSuccess(strTransactionID, Program.ConfigMgt.Config.SocketConfig);
                    return true;
                }
                else
                {
                    string strErrMsg = "Save to GC Gateway database failed.";
                    Program.Log.Write(LogType.Error, strErrMsg, XIMServerHelper.ModuleName);
                    sendData = XIMServerHelper.GetResponseFailed(strTransactionID, strErrMsg, Program.ConfigMgt.Config.SocketConfig);
                    return false;
                }
            }

            return false;
        }

        public bool IsListening
        {
            get { return _entity.IsListening; }
        }
        public bool Start()
        {
            return _entity.Start();
        }
        public bool Stop()
        {
            return _entity.Stop();
        }
    }
}
