using System;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.SQLOutboundAdapterObjects
{
    public class SQLOutAdapterConfigMgt
    {
        public const string _FileName = "SQLOutAdapter.xml";
        protected const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        private static SQLOutAdapterConfig _SQLOutAdapterConfig = new SQLOutAdapterConfig();
        public static SQLOutAdapterConfig SQLOutAdapterConfig {
            get { return _SQLOutAdapterConfig; }
            set { _SQLOutAdapterConfig = value; }
        }

        public static bool Load(string fileName)
        {
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string strXml = sr.ReadToEnd();
                    _SQLOutAdapterConfig = XObjectManager.CreateObject(strXml, typeof(SQLOutAdapterConfig)) as SQLOutAdapterConfig;
                    return (_SQLOutAdapterConfig != null);
                }
            }
            catch (Exception err)
            {
                _lastError = err;
                return false;
            }
        }

        public static bool Save(string fileName)
        {
            try
            {
                if (_SQLOutAdapterConfig == null) return false;
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string strXml = XMLHeader + _SQLOutAdapterConfig.ToXMLString();
                    sw.Write(strXml);
                }
                return true;
            }
            catch (Exception err)
            {
                _lastError = err;
                return false;
            }
        }

        public static bool LoadDefaultPassiveChannels(XCollection<SQLOutboundChanel> chs, string Prefix, string interfaceName)
        {
            //XCollection<SQLOutboundChanel> chs = new XCollection<SQLOutboundChanel>();
            chs.Clear();

            SQLOutboundChanel ch;
            int iFieldID = 0;
            SQLOutQueryResultItem ri;

            #region CH_Patient
            ch = new SQLOutboundChanel();
            ch.ChannelName = "CH_PATITENT";
            ch.SPName = Prefix + GWDataDBTable.Patient.ToString();
            ch.OperationType = ThrPartyDBOperationType.StorageProcedure;
            ch.Modified = false;
            
            #region Criteria
            SQLOutQueryCriteriaItem ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;            
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "00";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "01";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "02";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "03";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            #endregion

            #region Result

            iFieldID = 0;            
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Index))
            {
                // Third party application need to use this ID to update process flag
                //if (f.GetFullFieldName() == GWDataDBField.i_IndexGuid.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_DataDateTime.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_PROCESS_FLAG.GetFullFieldName()) continue;

                ri = new SQLOutQueryResultItem();
                ri.GWDataDBField = f;
                ri.SourceField = f.FieldName.ToUpper();
                ri.TargetField = f.GetFullFieldName().Replace(".","_").ToUpper();
                ri.ThirdPartyDBPatamter.FieldName = f.GetFullFieldName().Replace(".", "_").ToUpper();

                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.Translating.Type = TranslatingType.None;
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.Translating.Type = TranslatingType.None;

                ch.Rule.QueryResult.MappingList.Add(ri);
            }

            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Patient))
            {
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_DT.GetFullFieldName()) continue;                

                ri = new SQLOutQueryResultItem();
                ri.GWDataDBField = f;
                ri.SourceField = f.FieldName.ToUpper();
                ri.TargetField = f.GetFullFieldName().Replace(".", "_").ToUpper();
                ri.ThirdPartyDBPatamter.FieldName = f.GetFullFieldName().Replace(".", "_").ToUpper();

                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.Translating.Type = TranslatingType.None;
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.Translating.Type = TranslatingType.None;

                ch.Rule.QueryResult.MappingList.Add(ri);
            }

            chs.Add(ch);
            #endregion

            ch.Rule.RuleID = GWDataDBTable.Patient.ToString();
            ch.SPStatement = RuleControl.GetOutboundSP(interfaceName, ch.Rule);

            #endregion

            #region CH_Order
            
            ch = new SQLOutboundChanel();
            ch.ChannelName = "CH_ORDER";
            ch.SPName = Prefix + GWDataDBTable.Order.ToString();
            ch.OperationType = ThrPartyDBOperationType.StorageProcedure;
            ch.Modified = false;

            #region Criteria
            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "10";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "11";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "12";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "13";
            ch.Rule.QueryCriteria.MappingList.Add(ci);
            #endregion

            #region Result

            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Index))
            {
                // Third party application need to use this ID to update process flag
                //if (f.GetFullFieldName() == GWDataDBField.i_IndexGuid.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_DataDateTime.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_PROCESS_FLAG.GetFullFieldName()) continue;

                ri = new SQLOutQueryResultItem();
                ri.GWDataDBField = f;
                ri.SourceField = f.FieldName.ToUpper();
                ri.TargetField = f.GetFullFieldName().Replace(".", "_").ToUpper();
                ri.ThirdPartyDBPatamter.FieldName = f.GetFullFieldName().Replace(".", "_").ToUpper();

                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.Translating.Type = TranslatingType.None;
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

                ch.Rule.QueryResult.MappingList.Add(ri);
            }

            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Patient))
            {
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_DT.GetFullFieldName()) continue;

                ri = new SQLOutQueryResultItem();
                ri.GWDataDBField = f;
                ri.SourceField = f.FieldName.ToUpper();
                ri.TargetField = f.GetFullFieldName().Replace(".", "_").ToUpper();
                ri.ThirdPartyDBPatamter.FieldName = f.GetFullFieldName().Replace(".", "_").ToUpper();

                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.Translating.Type = TranslatingType.None;
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

                ch.Rule.QueryResult.MappingList.Add(ri);
            }

            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Order))
            {
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_DT.GetFullFieldName()) continue;

                ri = new SQLOutQueryResultItem();
                ri.GWDataDBField = f;
                ri.SourceField = f.FieldName.ToUpper();
                ri.TargetField = f.GetFullFieldName().Replace(".", "_").ToUpper();
                ri.ThirdPartyDBPatamter.FieldName = f.GetFullFieldName().Replace(".", "_").ToUpper();

                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.Translating.Type = TranslatingType.None;
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

                ch.Rule.QueryResult.MappingList.Add(ri);
            }

            chs.Add(ch);
            #endregion

            ch.Rule.RuleID = GWDataDBTable.Order.ToString();
            ch.SPStatement = RuleControl.GetOutboundSP(interfaceName, ch.Rule);
            
            #endregion

            #region CH_Report

            ch = new SQLOutboundChanel();
            ch.ChannelName = "CH_REPORT";
            ch.SPName = Prefix + GWDataDBTable.Report.ToString();
            ch.OperationType = ThrPartyDBOperationType.StorageProcedure;
            ch.Modified = false;

            #region Criteria
            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "30";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "31";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "32";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLOutQueryCriteriaItem();
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            ci.GWDataDBField = GWDataDBField.i_EventType;
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = ci.GWDataDBField.GetFullFieldName();
            ci.TargetField = ci.GWDataDBField.GetFullFieldName();
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "33";
            ch.Rule.QueryCriteria.MappingList.Add(ci);
            #endregion

            #region Result

            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Index))
            {
                // Third party application need to use this ID to update process flag
                //if (f.GetFullFieldName() == GWDataDBField.i_IndexGuid.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_DataDateTime.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_PROCESS_FLAG.GetFullFieldName()) continue;

                ri = new SQLOutQueryResultItem();
                ri.GWDataDBField = f;
                ri.SourceField = f.FieldName.ToUpper();
                ri.TargetField = f.GetFullFieldName().Replace(".", "_").ToUpper();
                ri.ThirdPartyDBPatamter.FieldName = f.GetFullFieldName().Replace(".", "_").ToUpper();

                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.Translating.Type = TranslatingType.None;
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

                ch.Rule.QueryResult.MappingList.Add(ri);
            }

            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Patient))
            {
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_DT.GetFullFieldName()) continue;

                ri = new SQLOutQueryResultItem();
                ri.GWDataDBField = f;
                ri.SourceField = f.FieldName.ToUpper();
                ri.TargetField = f.GetFullFieldName().Replace(".", "_").ToUpper();
                ri.ThirdPartyDBPatamter.FieldName = f.GetFullFieldName().Replace(".", "_").ToUpper();

                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.Translating.Type = TranslatingType.None;
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

                ch.Rule.QueryResult.MappingList.Add(ri);
            }

            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Order))
            {
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_DT.GetFullFieldName()) continue;

                ri = new SQLOutQueryResultItem();
                ri.GWDataDBField = f;
                ri.SourceField = f.FieldName.ToUpper();
                ri.TargetField = f.GetFullFieldName().Replace(".", "_").ToUpper();
                ri.ThirdPartyDBPatamter.FieldName = f.GetFullFieldName().Replace(".", "_").ToUpper();

                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.Translating.Type = TranslatingType.None;
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

                ch.Rule.QueryResult.MappingList.Add(ri);
            }

            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Report))
            {
                if (f.GetFullFieldName() == GWDataDBField.r_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.r_DATA_DT.GetFullFieldName()) continue;

                ri = new SQLOutQueryResultItem();
                ri.GWDataDBField = f;
                ri.SourceField = f.FieldName.ToUpper();
                ri.TargetField = f.GetFullFieldName().Replace(".", "_").ToUpper();
                ri.ThirdPartyDBPatamter.FieldName = f.GetFullFieldName().Replace(".", "_").ToUpper();

                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.Translating.Type = TranslatingType.None;
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

                ch.Rule.QueryResult.MappingList.Add(ri);
            }

            chs.Add(ch);
            #endregion

            ch.Rule.RuleID = GWDataDBTable.Report.ToString();
            ch.SPStatement = RuleControl.GetOutboundSP(interfaceName, ch.Rule);

            #endregion

            

            return true;
        }

        #region Exception definition
        private static Exception _lastError;
        public static Exception LastException
        {
            get
            {
                return _lastError;
            }
        }

        public static Exception LastXmlException
        {
            get
            {
                return XObjectManager.LastError;
            }
        }
        #endregion
    }
}
