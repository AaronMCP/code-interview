using System;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;
using HYS.Adapter.Base;
using HYS.SocketAdapter.Command;

namespace HYS.SocketAdapter.Configuration
{

    public class SocketOutboundAdapterConfigMgt
    {
        static string _FileName = "SocketOutboundAdapter.xml";
        public static string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        protected const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        private static SocketOutboundAdapterConfig _SocketOutAdapterConfig = new SocketOutboundAdapterConfig();
        public static SocketOutboundAdapterConfig SocketOutAdapterConfig
        {
            get { return _SocketOutAdapterConfig; }
            set { _SocketOutAdapterConfig = value; }
        }

        public static bool Load(string fileName)
        {
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string strXml = sr.ReadToEnd();
                    _SocketOutAdapterConfig = XObjectManager.CreateObject(strXml, typeof(SocketOutboundAdapterConfig)) as SocketOutboundAdapterConfig;
                    return (_SocketOutAdapterConfig != null);
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
                if (_SocketOutAdapterConfig == null) return false;
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string strXml = XMLHeader + _SocketOutAdapterConfig.ToXMLString();
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

        #region Sample Configuratin
        public static SocketOutboundAdapterConfig BuildSampleConfig()
        {
            SocketOutboundAdapterConfig Config = new SocketOutboundAdapterConfig();

            #region General Params
            //Config.OutGeneralParams.LogLevel = LogType.Debug;
            Config.OutGeneralParams.TimerEnable = true;
            Config.OutGeneralParams.TimerInterval = 1000 * 30;
            #endregion


            #region Client Socket Params
            Config.ClientSocketParams.ServerIP = "127.0.0.1";
            Config.ClientSocketParams.ServerPort = 6000;
            #endregion

            #region Look up Table
            LookupTable lt = new LookupTable();
            lt.DisplayName = "CommandType to EventType";
            lt.Table.Add(new LookupItem("0", Convert.ToInt32(CommandBase.CommandTypeEnum.PATIENT_ADD).ToString()));
            lt.Table.Add(new LookupItem("1", Convert.ToInt32(CommandBase.CommandTypeEnum.PATIENT_UPDATE).ToString()));
            lt.Table.Add(new LookupItem("3", Convert.ToInt32(CommandBase.CommandTypeEnum.PATIENT_DEL).ToString()));
            lt.Table.Add(new LookupItem("10", Convert.ToInt32(CommandBase.CommandTypeEnum.ORDER_ADD).ToString()));
            lt.Table.Add(new LookupItem("11", Convert.ToInt32(CommandBase.CommandTypeEnum.ORDER_UPDATE).ToString()));
            lt.Table.Add(new LookupItem("13", Convert.ToInt32(CommandBase.CommandTypeEnum.ORDER_DEL).ToString()));

            //IMAGE_ARRIVAL Need not to map            

            Config.LookupTables.Add(lt);

            #endregion


            #region Channel List
            SocketOutChannel ch;

            #region Channel for ImageArrival

            ch = new SocketOutChannel();
            ch.ChannelName = "Out_ImageArrival";
            ch.Enable = true;
            ch.OperationName = "Out_ImageArrival";
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.AutoUpdateProcessFlag = true;

            #region Query CriterialItems for channel ImageArrival
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;

            // ImageArrival
            SocketOutQueryCriterialItem ciImageArrival = new SocketOutQueryCriterialItem();
            ciImageArrival.Type = QueryCriteriaType.And;
            ciImageArrival.SourceField = "Event_Type";
            ciImageArrival.GWDataDBField.FieldName = "Event_Type";
            ciImageArrival.GWDataDBField.Table = GWDataDBTable.Index;
            ciImageArrival.Translating.Type = TranslatingType.FixValue;
            ciImageArrival.Translating.ConstValue = "13"; // del order
            ciImageArrival.Operator = QueryCriteriaOperator.EqualSmallerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciImageArrival);

            ciImageArrival = new SocketOutQueryCriterialItem();
            ciImageArrival.Type = QueryCriteriaType.And;
            ciImageArrival.SourceField = "Event_Type";
            ciImageArrival.GWDataDBField.FieldName = "Event_Type";
            ciImageArrival.GWDataDBField.Table = GWDataDBTable.Index;
            ciImageArrival.Translating.Type = TranslatingType.FixValue;
            ciImageArrival.Translating.ConstValue = "10"; // add order
            ciImageArrival.Operator = QueryCriteriaOperator.EqualLargerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciImageArrival);

            ciImageArrival = new SocketOutQueryCriterialItem();
            ciImageArrival.Type = QueryCriteriaType.And;
            ciImageArrival.SourceField = "Exam_status";
            ciImageArrival.GWDataDBField.FieldName = "Exam_status";
            ciImageArrival.GWDataDBField.Table = GWDataDBTable.Order;
            ciImageArrival.Translating.Type = TranslatingType.FixValue;
            ciImageArrival.Translating.ConstValue = "18"; // ImageArrival
            ciImageArrival.Operator = QueryCriteriaOperator.Equal;
            ch.Rule.QueryCriteria.MappingList.Add(ciImageArrival);

            #endregion

            #region Query Result for channel ImageArrival

            // Set Commantype = Convert.ToInt32(CommandBase.CommandTypeEnum.IMAGE_ARRIVAL).ToString();
            SocketOutQueryResultItem ri = new SocketOutQueryResultItem();

            // Get Data_ID ,but not send to third database
            ri.SourceField = "Data_ID";
            ri.TargetField = "Data_ID";
            ri.GWDataDBField.FieldName = "Data_ID";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Data_DT";
            ri.TargetField = "Data_DT";
            ri.GWDataDBField.FieldName = "Data_DT";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new SocketOutQueryResultItem();
            ri.SourceField = "";
            ri.TargetField = "Commandtype";
            ri.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.Type = TranslatingType.FixValue;
            ri.Translating.ConstValue = Convert.ToInt32(CommandBase.CommandTypeEnum.IMAGE_ARRIVAL).ToString();
            ch.Rule.QueryResult.MappingList.Add(ri);


            // CommandGUID -> DataIndex.Record_Index_1
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Record_Index_1";
            ri.TargetField = "CommandGUID";
            ri.GWDataDBField.FieldName = "Record_Index_1";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "CommandGUID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(ri);

            // PatientID -> Patient.PatientID
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "PatientID";
            ri.TargetField = "PatientID";

            ri.GWDataDBField.FieldName = "PatientID";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // AccessionNumber -> Order.Order_No
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Order_No";
            ri.TargetField = "AccessionNumber"; 

            ri.GWDataDBField.FieldName = "Order_No";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "AccessionNumber";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // ModalityName -> Order.Modality


            // OperatorName -> Order.???

            //TODO: Identity OperatorName

            // Performed_enddt -> 

            //TODO: Identity Performed_enddt

            // Performed_startdt -> Order.Exam_DT
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Exam_DT";
            ri.TargetField = "Performed_startdt"; 

            ri.GWDataDBField.FieldName = "Exam_DT";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "Performed_startdt";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            #endregion 

            Config.OutboundChanels.Add(ch);

            #endregion//Channel for ImageArrival



            #region Channel for Order

            ch = new SocketOutChannel();
            ch.ChannelName = "crud_Order";
            ch.Enable = true;
            ch.OperationName = "crud_Order";
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.AutoUpdateProcessFlag = true;

            #region Query CriterialItems for channel Order
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;

            // Order
            SocketOutQueryCriterialItem ciOrder = new SocketOutQueryCriterialItem();
            ciOrder.Type = QueryCriteriaType.And;
            ciOrder.SourceField = "Event_Type";
            ciOrder.GWDataDBField.FieldName = "Event_Type";
            ciOrder.GWDataDBField.Table = GWDataDBTable.Index;
            ciOrder.Translating.Type = TranslatingType.FixValue;
            ciOrder.Translating.ConstValue = "13"; // del order
            ciOrder.Operator = QueryCriteriaOperator.EqualSmallerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciOrder);

            ciOrder = new SocketOutQueryCriterialItem();
            ciOrder.Type = QueryCriteriaType.And;
            ciOrder.SourceField = "Event_Type";
            ciOrder.GWDataDBField.FieldName = "Event_Type";
            ciOrder.GWDataDBField.Table = GWDataDBTable.Index;
            ciOrder.Translating.Type = TranslatingType.FixValue;
            ciOrder.Translating.ConstValue = "10"; // add order
            ciOrder.Operator = QueryCriteriaOperator.EqualLargerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciOrder);

            ciOrder = new SocketOutQueryCriterialItem();
            ciOrder.Type = QueryCriteriaType.And;
            ciOrder.SourceField = "Exam_status";
            ciOrder.GWDataDBField.FieldName = "Exam_status";
            ciOrder.GWDataDBField.Table = GWDataDBTable.Order;
            ciOrder.Translating.Type = TranslatingType.FixValue;
            ciOrder.Translating.ConstValue = "18"; // not imagearrval
            ciOrder.Operator = QueryCriteriaOperator.NotEqual;
            ch.Rule.QueryCriteria.MappingList.Add(ciOrder);

            #endregion

            #region Query Result for channel Order

            
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Data_ID";
            ri.TargetField = "Data_ID";
            ri.GWDataDBField.FieldName = "Data_ID";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Data_DT";
            ri.TargetField = "Data_DT";
            ri.GWDataDBField.FieldName = "Data_DT";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Event_Type";
            ri.TargetField = "Commandtype";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.GWDataDBField.FieldName = "Event_Type";
            ri.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.Type = TranslatingType.LookUpTable;
            ri.Translating.LutName = lt.TableName;
            ch.Rule.QueryResult.MappingList.Add(ri);


            // CommandGUID -> DataIndex.Record_Index_1
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Record_Index_1";
            ri.TargetField = "CommandGUID";
            ri.GWDataDBField.FieldName = "Record_Index_1";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "CommandGUID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(ri);

            // PatientID -> Patient.PatientID
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "PatientID";
            ri.TargetField = "PatientID";

            ri.GWDataDBField.FieldName = "PatientID";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Order.Order_No -> AccessionNumber
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Order_No";
            ri.TargetField = "AccessionNumber";

            ri.GWDataDBField.FieldName = "Order_No";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "AccessionNumber";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // ModalityName -> Order.Modality


            // OperatorName -> Order.???

            //TODO: Identity OperatorName

            // Performed_enddt -> 

            //TODO: Identity Performed_enddt

            // Performed_startdt -> Order.Exam_DT
            //ri = new SocketOutQueryResultItem();
            //ri.SourceField = "Performed_startdt";
            //ri.TargetField = "Exam_DT";

            //ri.GWDataDBField.FieldName = "Exam_DT";
            //ri.GWDataDBField.Table = GWDataDBTable.Order;

            //ri.ThirdPartyDBPatamter.FieldName = "Performed_startdt";
            //ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            #endregion

            Config.OutboundChanels.Add(ch);

            #endregion//Channel for Order


            #region Channel For Add,Edit,Delete Patient
            ch = new SocketOutChannel();
            ch.ChannelName = "Out_CRUDPatient";
            ch.Enable = true;
            ch.OperationName = "Out_CRUDPatient";
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.AutoUpdateProcessFlag = true;

            #region CriterialItems
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;

            // Patient Add
            SocketOutQueryCriterialItem ciPatientAdd = new SocketOutQueryCriterialItem();
            ciPatientAdd.Type = QueryCriteriaType.And;
            ciPatientAdd.SourceField = "Event_Type";
            ciPatientAdd.GWDataDBField.FieldName = "Event_Type";
            ciPatientAdd.GWDataDBField.Table = GWDataDBTable.Index;
            ciPatientAdd.Translating.Type = TranslatingType.FixValue;
            ciPatientAdd.Translating.ConstValue = "03";
            ciPatientAdd.Operator = QueryCriteriaOperator.EqualSmallerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciPatientAdd);

            ciPatientAdd = new SocketOutQueryCriterialItem();
            ciPatientAdd.Type = QueryCriteriaType.And;
            ciPatientAdd.SourceField = "Event_Type";
            ciPatientAdd.GWDataDBField.FieldName = "Event_Type";
            ciPatientAdd.GWDataDBField.Table = GWDataDBTable.Index;
            ciPatientAdd.Translating.Type = TranslatingType.FixValue;
            ciPatientAdd.Translating.ConstValue = "00";
            ciPatientAdd.Operator = QueryCriteriaOperator.EqualLargerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciPatientAdd);
            
            #endregion

            #region Query Result            

            // Commantype -> DataIndex.Event_Type
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Data_ID";
            ri.TargetField = "Data_ID";
            ri.GWDataDBField.FieldName = "Data_ID";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Data_DT";
            ri.TargetField = "Data_DT";
            ri.GWDataDBField.FieldName = "Data_DT";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Event_Type";
            ri.TargetField = "Commandtype";
            ri.GWDataDBField.FieldName = "Event_Type";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.Type = TranslatingType.LookUpTable;
            ri.Translating.LutName = lt.TableName;
            ch.Rule.QueryResult.MappingList.Add(ri);



            // CommandGUID -> DataIndex.Record_Index_1
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Record_Index_1";
            ri.TargetField = "CommandGUID";
            ri.GWDataDBField.FieldName = "Record_Index_1";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "CommandGUID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(ri);

            // PatientID -> Patient.PatientID
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "PatientID";
            ri.TargetField = "PatientID";

            ri.GWDataDBField.FieldName = "PatientID";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // PatientName -> Patient_Name
            ri = new SocketOutQueryResultItem();
            ri.SourceField = "Patient_Name";
            ri.TargetField = "PatientName";

            ri.GWDataDBField.FieldName = "Patient_Name";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientName";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);
            #endregion

            Config.OutboundChanels.Add(ch);
            #endregion

                        

            #endregion


            #region ThrPartDBParameters

            #endregion

            return Config;

        }
        #endregion
    }
}
