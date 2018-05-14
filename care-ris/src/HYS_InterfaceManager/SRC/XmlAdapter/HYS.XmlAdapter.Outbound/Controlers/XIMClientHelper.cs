using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HYS.Common.Objects.Config;
using HYS.XmlAdapter.Common.Files;
using HYS.XmlAdapter.Common.Objects;
using HYS.XmlAdapter.Outbound.Objects;
using HYS.XmlAdapter.Outbound.Adapters;

namespace HYS.XmlAdapter.Outbound.Controlers
{
    public class XIMClientHelper
    {
        #region XIM protocol

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

        //private static string _requestHeader;
        //public static string RequestHeader
        //{
        //    get
        //    {
        //        if (_requestHeader == null)
        //        {
        //            StringBuilder sb = new StringBuilder();

        //            sb.AppendLine("POST /HYS-EC HTTP/1.1");
        //            sb.AppendLine("Content-Type: text/xml");
        //            sb.AppendLine("Content-Length: 1024");
        //            sb.AppendLine("SOAPAction: \"\"");
        //            sb.AppendLine("Host: localhost:" + Program.ConfigMgt.Config.SocketConfig.Port.ToString());
        //            sb.AppendLine("Pragma: no-cache");
        //            sb.AppendLine();

        //            _requestHeader = sb.ToString();
        //        }
        //        return _requestHeader;
        //    }
        //}

        #endregion

        #region XIM process

        public const string ModuleName = "XIMClient";

        private const string ITEM = "/XMLRequestMessage/XIM/ITEM";
        private const string PATIENT_ID = "/XMLRequestMessage/XIM/ITEM/PATIENT/IDENTIFICATION/ID/ID";
        private const string ACCESSION_NUMBER = "/XMLRequestMessage/XIM/ITEM/ORDER/IDENTIFICATION/ACCESSION_NUMBER/ID";
        private const string STUDY_INSTANCE_UID = "/XMLRequestMessage/XIM/ITEM/STUDY/IDENTIFICATION/GLOBAL_IDENTIFIER/INSTANCE_UID";
        private const string SCHEDULED_PROCEDURE_STEP = "/XMLRequestMessage/XIM/ITEM/SCHEDULED_PROCEDURE_STEP";
        private static string GetPKPath(int index)
        {
            switch (index)
            {
                default: return "";
                case 0: return PATIENT_ID;
                case 1: return ACCESSION_NUMBER;
                case 2: return STUDY_INSTANCE_UID;
            }
        }

        public static List<DataSet> SplitDataSet(DataSet ds)
        {
            if (ds == null || ds.Tables.Count < 1) return null;

            List<DataSet> dsList = new List<DataSet>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataSet dsNew = new DataSet();
                dsNew.Merge(new DataRow[] { dr });
                dsList.Add(dsNew);
            }

            return dsList;
        }
        public static List<XIMWrapper> Transform(XMLTransformer transformer, List<DataSet> dataSetList)
        {
            if (transformer == null || dataSetList == null) return null;
            List<XIMWrapper> resultList = new List<XIMWrapper>();
            foreach (DataSet ds in dataSetList)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                ds.WriteXml(sw);
                string xml = sb.ToString();

                string str = "";
                if (transformer.TransformString(xml, ref str))
                {
                    XIMWrapper w = new XIMWrapper(str);
                    w.GUIDList.Add(ConfigMain.GetGUID(ds));
                    resultList.Add(w);
                }
            }
            return resultList;
        }
        public static List<XIMWrapper> MergeProcedureStep(List<XIMWrapper> msgList, string pkPath)
        {
            if (msgList == null || pkPath == null || pkPath.Length < 1) return null;

            int index = 0;
            Dictionary<string, XIMWrapper> mergeTable = new Dictionary<string, XIMWrapper>();
            foreach (XIMWrapper msg in msgList)
            {
                msg.Document = new XmlDocument();
                msg.Document.LoadXml(msg.XIMDocument);

                string pkValue = null;
                XmlNode node = msg.Document.SelectSingleNode(pkPath);
                if (node != null) pkValue = node.InnerText;
                if (pkValue == null || pkValue.Length < 1)
                    pkValue = "_" + (index++).ToString();

                if (mergeTable.ContainsKey(pkValue))
                {
                    XIMWrapper oldmsg =mergeTable[pkValue];
                    foreach (string guid in msg.GUIDList) oldmsg.GUIDList.Add(guid);
                    XmlNode itemNode = oldmsg.Document.SelectSingleNode(ITEM);
                    if (itemNode != null)
                    {
                        XmlNode spsNode = msg.Document.SelectSingleNode(SCHEDULED_PROCEDURE_STEP);
                        if (spsNode != null)
                        {
                            //itemNode.AppendChild(spsNode.Clone());
                            string xml = itemNode.InnerXml;
                            xml = xml + spsNode.OuterXml;
                            itemNode.InnerXml = xml;
                        }
                    }
                }
                else
                {
                    mergeTable.Add(pkValue, msg);
                }
            }

            List<XIMWrapper> resultList = new List<XIMWrapper>();
            foreach (KeyValuePair<string, XIMWrapper> pair in mergeTable)
            {
                XIMWrapper w= pair.Value;
                w.XIMDocument = w.Document.OuterXml;
                resultList.Add(w);
            }
            return resultList;
        }
        public static List<XIMWrapper> MergeProcedureStep(List<XIMWrapper> msgList, int pkIndex)
        {
            return MergeProcedureStep(msgList, GetPKPath(pkIndex));
        }

        #endregion

        #region File helper

        private static string DataFolder = "data";
        private static string IndexFolder = "index";
        private static string IndexFileExtension = ".idx";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool SaveToFile(XIMOutboundConfig config, string content)
        {
            if (config == null || content == null) return false;

            try
            {
                string pathData = ConfigHelper.GetFullPath(config.TargetPath + "\\" + DataFolder);
                string pathIndex = ConfigHelper.GetFullPath(config.TargetPath + "\\" + IndexFolder);
                if (!Directory.Exists(pathData)) Directory.CreateDirectory(pathData);
                if (!Directory.Exists(pathIndex)) Directory.CreateDirectory(pathIndex);

                string fileName = config.FileNamePrefix + DirectoryMonitor.GetRandomString();

                string fnData = pathData + "\\" + fileName + config.FileNameSuffix;
                Program.Log.Write("Write to file: " + fnData);
                using (StreamWriter sw = File.CreateText(fnData))
                {
                    sw.Write(content);
                }

                string fnIndex = pathIndex + "\\" + fileName + IndexFileExtension;
                Program.Log.Write("Write to file: " + fnIndex);
                using (StreamWriter sw = File.CreateText(fnIndex))
                {
                }

                return true;
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
                return false;
            }
        }

        #endregion
    }
}
