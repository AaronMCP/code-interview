using System;
using System.Data;
using System.IO;
using HYS.IM.Messaging.Objects;
using HYS.IM.Messaging.Registry;
using HYS.IM.MessageDevices.CSBAdapter.Inbound.Config;
using HYS.IM.Common.Logging;
using System.Text.RegularExpressions;
using System.Text;
using HYS.IM.MessageDevices.CSBAdapter.Inbound.Adapters;
using HYS.Common.Objects.Rule;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Controler
{
    public partial class CSBrokerInboundControler
    {
        private bool DispatchMessage_Custom(DataRow dataRow, ref string strResponse)
        {
            string strFieldName = GWDataDB.GetTableName(GWDataDB.GetTable(_entity.Context.ConfigMgr.Config.MessageDispatchConfig.TableName)) + "_" + _entity.Context.ConfigMgr.Config.MessageDispatchConfig.FieldName;
            string strConditionValue = dataRow[strFieldName].ToString();

            string[] valueSubscribers = _entity.Context.ConfigMgr.Config.MessageDispatchConfig.ValueSubscriber.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in valueSubscribers)
            {
                if (str.Equals(strConditionValue))
                {
                    return DispatchMessage_Publish(dataRow);
                }
            }

            string[] valueResponsers = _entity.Context.ConfigMgr.Config.MessageDispatchConfig.ValueResponser.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str in valueResponsers)
            {
                if (str.Equals(strConditionValue))
                {
                    return DispatchMessage_Request(dataRow, ref strResponse);
                }
            }

            _entity.Context.Log.Write(LogType.Error, "Can not determine how to dispath the following dataset according to current configuration.");
            _entity.Context.Log.Write(LogType.Error, SerializeDataRowToXml(dataRow));

            strResponse = _entity.Context.ConfigMgr.Config.GetBrokerErrorMessageContent();
            return false;
        }
        private bool DispatchMessage_Request(DataRow dataRow, ref string strResponse)
        {
            Message reqMsg = new Message();
            reqMsg.Header.ID = Guid.NewGuid();
            //notify.Header.Type = MessageRegistry.HL7V2_NotificationMessageType;
            reqMsg.Header.Type = MessageRegistry.GENERIC_RequestMessageType;
            reqMsg.Body = SerializeDataRowToXml(dataRow);

            Message rspMsg = null;
            _entity.Context.Log.Write("Begin dispatching message to requester directly.");
            bool res = _entity.NotifyMessageRequest(reqMsg, out rspMsg);
            _entity.Context.Log.Write(string.Format("End dispatching message to requester directly. Result: {0}", res));

            if (res)
            {
                strResponse = rspMsg.Body;
            }
            else
            {
                strResponse = _entity.Context.ConfigMgr.Config.GetBrokerErrorMessageContent();
            }

            return true;
        }
        private bool DispatchMessage_Publish(DataRow dataRow)
        {
            Message notify = new Message();
            notify.Header.ID = Guid.NewGuid();
            //notify.Header.Type = MessageRegistry.HL7V2_NotificationMessageType;
            notify.Header.Type = MessageRegistry.GENERIC_NotificationMessageType;
            notify.Body = SerializeDataRowToXml(dataRow);

            _entity.Context.Log.Write("Begin sending notification to subscriber.");
            bool ret = _entity.NotifyMessagePublish(notify);
            _entity.Context.Log.Write("End sending notification to subscriber. Result: " + ret.ToString());

            return ret;
        }

        private static string SerializeDataRowToXml(DataRow dataRow)
        {
            using (DataTable dt = dataRow.Table.Clone())
            {
                dt.LoadDataRow(dataRow.ItemArray, true);

                //StringWriter sw = new StringWriter();
                //dt.WriteXml(sw);

                //string strContent = sw.ToString();
                //Match match = Regex.Match(strContent, "<" + dt.TableName + ">(.*)</" + dt.TableName + ">");

                StringBuilder sb = new StringBuilder();
                sb.Append("<csb:").Append(dt.TableName).Append(" xmlns:csb=\"http://www.carestream.com/csbroker\">")
                    .Append("<csb:Table>");

                foreach (DataColumn col in dataRow.Table.Columns)
                {
                    if (col.ColumnName.Equals("DATAINDEX_DATA_ID")) continue;
                    sb.AppendFormat("<csb:{0}><![CDATA[{1}]]></csb:{0}>", col.ColumnName, dataRow[col.ColumnName]);
                }

                sb.Append("</csb:Table>")
                    .Append("</csb:").Append(dt.TableName).Append(">");

                return sb.ToString();
            }
        }
    }
}
