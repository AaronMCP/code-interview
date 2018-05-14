using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;
using System.Data;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using HYS.IM.Common.Logging;
using MSG = HYS.IM.Messaging.Objects;
using System.Data.OleDb;
using HYS.IM.Messaging.Base.Config;
using HYS.Common.Xml;
using HYS.IM.MessageDevices.CSBAdpater.Outbound.Config;
using System.Text.RegularExpressions;
using HYS.IM.Messaging.Mapping.Transforming;
using HYS.IM.MessageDevices.CSBAdpater.Outbound.Adapters;
using HYS.IM.Messaging.Queuing.MSMQ;
using HYS.IM.Messaging.Objects.PublishModel;

namespace HYS.IM.MessageDevices.CSBAdpater.Outbound.Controler
{
    public class CSBrokerOutboundControler
    {
        private EntityImpl _entity;
        public CSBrokerOutboundControler(EntityImpl entity)
        {
            _entity = entity;
        }

        public bool ProcessSubscribedMessage(IPushRoute route, MSG.Message msg)
        {
            if (msg == null || msg.Header == null) return false;
            string msgId = msg.Header.ID.ToString();

            bool res = false;
            _entity.Context.Log.Write(string.Format("Begin processing message. ID: {0}", msgId));

            MSG.Message csbMsg = TransformToCSBDataSetSchemedXDSGWMessage(msg, msgId);
            if (csbMsg != null)
            {
                DataSet ds = GenerateCSBrokerDataSet(csbMsg.Body);
                if (ds != null) res = InsertIntoCSBrokerDatabase(route, ds);
            }

            if (!res) DumpErrorMessage(msg, msgId);
            _entity.Context.Log.Write(string.Format("Finish processing message. ID: {0}, Result: {1}.\r\n", msgId, res));
            return res;
        }

        private MSG.Message TransformToCSBDataSetSchemedXDSGWMessage(MSG.Message msg, string msgID)
        {
            if (!_entity.Context.ConfigMgr.Config.EnableXMLTransform) return msg;

            _entity.Context.Log.Write("Begin transforming XDSGW message.");
            string file = ConfigHelper.GetFullPath(_entity.Context.AppArgument.ConfigFilePath, _entity.Context.ConfigMgr.Config.XSLTFilePath);
            XMLTransformer t = XMLTransformer.CreateFromFileWithCache(file, _entity.Context.Log);
            if (t == null) return null;

            string csbMsgXml = "";
            string otherMsgXml = msg.ToXMLString();
            if (!t.TransformString(otherMsgXml, ref csbMsgXml, XSLTExtensionTypes.None)) return null;

            MSG.Message csbMsg = XObjectManager.CreateObject<MSG.Message>(csbMsgXml);
            if (csbMsg == null)
            {
                _entity.Context.Log.Write(LogType.Error, "Cannot deserialize the transformed XML into XDSGW message.");
                string fname = string.Format("{0}_transformed", msgID);
                DumpErrorMessage(csbMsgXml, fname);
            }

            _entity.Context.Log.Write("Finish transforming XDSGW message.");
            return csbMsg;
        }
        private DataSet GenerateCSBrokerDataSet(string csbDataSetXml)
        {
            try
            {
                _entity.Context.Log.Write("Begin deserialize CS Broker DataSet.");
                DataSet ds = new DataSet();
                XmlReadMode m = ds.ReadXml(XmlReader.Create(new StringReader(csbDataSetXml)));
                _entity.Context.Log.Write(string.Format("Finish deserialize CS Broker DataSet. Name: {0}, RowCount: {1}, XmlReadMode: {2}.",
                    ds.DataSetName, ds.Tables.Count > 0 ? ds.Tables[0].Rows.Count : -1, m));
                return ds;
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
                return null;
            }
        }
        private bool InsertIntoCSBrokerDatabase(IPushRoute route, DataSet csbDataSet)
        {
            bool res = false;

            try
            {
                if (csbDataSet == null || csbDataSet.Tables.Count < 1) return false;
                _entity.Context.Log.Write("Begin calling CS Broker storage procedure.");

                if (_entity.Context.ConfigMgr.Config.EnaleKanJiReplacement)
                {
                    KanJiReplacement(csbDataSet);
                }
                


                string dbcnn = _entity.Context.ConfigMgr.Config.CSBrokerOLEDBConnectionString;
                string spName = string.Format("sp_{0}_{1}", // e.g. "sp_SQLIN_Order", 
                    _entity.Context.ConfigMgr.Config.CSBrokerPassiveSQLInboundInterfaceName,
                    csbDataSet.DataSetName);

                DataTable tb = csbDataSet.Tables[0];
                CSBrokerOutboundHelper.ReplaceValueInCSBrokerDataSet(tb, _entity.Context);


               

                int i = 1;
                int c = tb.Rows.Count;
                foreach (DataRow dr in tb.Rows)
                {
                    _entity.Context.Log.Write(string.Format("Inserting record {0}/{1}", i++, c));

                    OleDbCommand cmd = new OleDbCommand(spName);
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (DataColumn dc in tb.Columns)
                    {
                        string pvalue = dr[dc] as string;
                        string pname = string.Format("@{0}_{1}",
                            _entity.Context.ConfigMgr.Config.CSBrokerPassiveSQLInboundInterfaceName,
                            dc.ColumnName);

                        _entity.Context.Log.Write(string.Format("{0}:{1}", pname, pvalue));

                        OleDbParameter p = new OleDbParameter(pname, OleDbType.VarWChar);
                        p.Direction = ParameterDirection.Input;
                        p.Value = (pvalue != null) ? pvalue : "";
                        cmd.Parameters.Add(p);
                    }

                    using (OleDbConnection conn = new OleDbConnection(dbcnn))
                    {
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                res = true;
            }
            catch (OleDbException dberr)
            {
                _entity.Context.Log.Write(dberr);
                MSMQReceiveCancelException.RaiseMSMQException(route, dberr.Message);
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
            }

            _entity.Context.Log.Write(string.Format("Finish calling CS Broker storage procedure. Result: " + res));
            return res;
        }


        private void KanJiReplacement(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0 || !ds.Tables[0].Columns.Contains("PATIENT_PATIENT_NAME"))
                return;
            string kanjiReplacementChar=_entity.Context.ConfigMgr.Config.KanJiReplacementChar;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string patientName=Convert.ToString(dr["PATIENT_PATIENT_NAME"]);

                dr["PATIENT_PATIENT_NAME"] = Level3KanJiReplacement.Replacement(patientName, kanjiReplacementChar);
            }
        }

        private void DumpErrorMessage(MSG.Message msg, string msgID)
        {
            DumpErrorMessage(msg.ToXMLString(), msgID);
        }
        private void DumpErrorMessage(string msgXml, string msgFileName)
        {
            try
            {
                string pathName = Path.Combine(Application.StartupPath, "CSBrokerOutboundErrorMessage");
                string fileName = Path.Combine(pathName, string.Format("{0}.xml", msgFileName));
                _entity.Context.Log.Write(LogType.Information, "Dumping message into following file: " + fileName);
                if (!Directory.Exists(pathName)) Directory.CreateDirectory(pathName);
                File.WriteAllText(fileName, msgXml, Encoding.UTF8);
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
            }
        }
    }
}
