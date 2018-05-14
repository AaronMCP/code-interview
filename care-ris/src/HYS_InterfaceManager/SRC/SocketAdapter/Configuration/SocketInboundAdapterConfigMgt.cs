using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Command;

namespace HYS.SocketAdapter.Configuration
{    

    public class SocketInboundAdapterConfigMgt
    {
        static string _FileName = "SocketInboundAdapter.xml";
        public static string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        protected const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        private static SocketInboundAdapterConfig _SocketInAdapterConfig = new SocketInboundAdapterConfig();
        public static SocketInboundAdapterConfig SocketInAdapterConfig
        {
            get { return _SocketInAdapterConfig; }
            set { _SocketInAdapterConfig = value; }
        }

        public static bool Load(string fileName)
        {
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string strXml = sr.ReadToEnd();
                    _SocketInAdapterConfig = XObjectManager.CreateObject(strXml, typeof(SocketInboundAdapterConfig)) as SocketInboundAdapterConfig;
                    return (_SocketInAdapterConfig != null);
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
                if (_SocketInAdapterConfig == null) return false;
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string strXml = XMLHeader + _SocketInAdapterConfig.ToXMLString();
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
        public static SocketInboundAdapterConfig BuildSampleConfig()
        {
            SocketInboundAdapterConfig Config = new SocketInboundAdapterConfig();

            #region General Params
            //Config.InGeneralParams.LogLevel = LogType.Debug;
            #endregion
            
            #region Server Socket Params
            Config.ListenServerSocketParams.ListenIP = "127.0.0.1";
            Config.ListenServerSocketParams.ListenPort = 6000;
            #endregion

            #region Look up Table
            LookupTable lt = new LookupTable();
            lt.DisplayName = "CommandType to EventType";
            lt.Table.Add(new LookupItem(Convert.ToInt32(CommandBase.CommandTypeEnum.PATIENT_ADD).ToString(), "0"));
            lt.Table.Add(new LookupItem(Convert.ToInt32(CommandBase.CommandTypeEnum.PATIENT_UPDATE).ToString(), "1"));
            lt.Table.Add(new LookupItem(Convert.ToInt32(CommandBase.CommandTypeEnum.PATIENT_DEL).ToString(), "3"));
            lt.Table.Add(new LookupItem(Convert.ToInt32(CommandBase.CommandTypeEnum.ORDER_ADD).ToString(), "10"));
            lt.Table.Add(new LookupItem(Convert.ToInt32(CommandBase.CommandTypeEnum.ORDER_UPDATE).ToString(), "11"));
            lt.Table.Add(new LookupItem(Convert.ToInt32(CommandBase.CommandTypeEnum.ORDER_DEL).ToString(), "13"));

            //IMAGE_ARRIVAL map to ORDER_UPDATE, later we use sigle channel to treate IMAGE_ARRVIAL
            lt.Table.Add(new LookupItem(Convert.ToInt32(CommandBase.CommandTypeEnum.IMAGE_ARRIVAL).ToString(), "11"));

            Config.LookupTables.Add(lt);

            #endregion


            #region Channel List
            SocketInChannel ch;

            #region Channel For Add,Edit,Delete Patient
            ch = new SocketInChannel();
            ch.ChannelName = "CRUDPatient";
            ch.Enable = true;
            ch.OperationName = "CRUDPatient";
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.AutoUpdateProcessFlag = true;

            #region CriterialItems
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;

            // Patient Add
            SocketInQueryCriteriaItem ciPatientAdd = new SocketInQueryCriteriaItem();
            ciPatientAdd.Type = QueryCriteriaType.Or;
            ciPatientAdd.SourceField = "Commandtype";
            ciPatientAdd.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ciPatientAdd.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ciPatientAdd.Translating.Type = TranslatingType.FixValue;
            ciPatientAdd.Translating.ConstValue = Convert.ToInt32(CommandBase.CommandTypeEnum.PATIENT_UPDATE).ToString();
            ciPatientAdd.Operator = QueryCriteriaOperator.EqualSmallerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciPatientAdd);

            //// Order
            //SocketInQueryCriteriaItem ciOrderAdd = new SocketInQueryCriteriaItem();
            //ciOrderAdd.Type = QueryCriteriaType.Or;
            //ciOrderAdd.SourceField = "Commandtype";
            //ciPatientAdd.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            //ciOrderAdd.ThirdPartyDBPatamter.FieldName = "Commandtype";
            //ciOrderAdd.Translating.ConstValue = CommandBase.CommandTypeEnum.ORDER_ADD;
            //ch.Rule.QueryCriteria.MappingList.Add(ciOrderAdd);
            #endregion

            #region Query Result
            SocketInQueryResultItem ri;

            // Commantype -> DataIndex.Event_Type
            ri = new SocketInQueryResultItem();
            ri.SourceField = "Commandtype";
            ri.TargetField = "Event_Type";
            ri.GWDataDBField.FieldName = "Event_Type";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.Type = TranslatingType.LookUpTable;
            ri.Translating.LutName = lt.TableName;

            ch.Rule.QueryResult.MappingList.Add(ri);


            // CommandGUID -> DataIndex.Record_Index_1
            ri = new SocketInQueryResultItem();
            ri.SourceField = "CommandGUID";
            ri.TargetField = "Record_Index_1";
            ri.GWDataDBField.FieldName = "Record_Index_1";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "CommandGUID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(ri);

            // PatientID -> Patient.PatientID
            ri = new SocketInQueryResultItem();
            ri.SourceField = "PatientID";
            ri.TargetField = "PatientID";

            ri.GWDataDBField.FieldName = "PatientID";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // PatientName -> Patient_Name
            ri = new SocketInQueryResultItem();
            ri.SourceField = "PatientName";
            ri.TargetField = "Patient_Name";

            ri.GWDataDBField.FieldName = "Patient_Name";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientName";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);
            #endregion

            Config.InboundChanels.Add(ch);
            #endregion


            #region Channel for Add ,Edit, Delete Order
            ch = new SocketInChannel();
            ch.ChannelName = "CRUDOrder";
            ch.Enable = true;
            ch.OperationName = "CRUDOrder";
            ch.Rule.CheckProcessFlag = false;
            ch.Rule.AutoUpdateProcessFlag = false;

            #region CriterialItems
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;

            // Order Add, Del ,Update
            SocketInQueryCriteriaItem ciOrder = new SocketInQueryCriteriaItem();
            ciOrder.Type = QueryCriteriaType.And;
            ciOrder.SourceField = "Commandtype";
            ciOrder.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ciOrder.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ciOrder.Translating.Type = TranslatingType.FixValue;
            ciOrder.Translating.ConstValue = Convert.ToInt32(CommandBase.CommandTypeEnum.ORDER_ADD).ToString();
            ciOrder.Operator = QueryCriteriaOperator.EqualLargerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciOrder);

            ciOrder = new SocketInQueryCriteriaItem();
            ciOrder.Type = QueryCriteriaType.And;
            ciOrder.SourceField = "Commandtype";
            ciOrder.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ciOrder.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ciOrder.Translating.Type = TranslatingType.FixValue;
            ciOrder.Translating.ConstValue = Convert.ToInt32(CommandBase.CommandTypeEnum.ORDER_UPDATE).ToString();
            ciOrder.Operator = QueryCriteriaOperator.EqualSmallerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciOrder);
            
            #endregion

            #region Query Result
           
            // Commantype -> DataIndex.Event_Type
            ri = new SocketInQueryResultItem();
            ri.SourceField = "Commandtype";
            ri.TargetField = "Event_Type";
            ri.GWDataDBField.FieldName = "Event_Type";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.Type = TranslatingType.LookUpTable;
            ri.Translating.LutName = lt.TableName;

            ch.Rule.QueryResult.MappingList.Add(ri);


            // CommandGUID -> DataIndex.Record_Index_1
            ri = new SocketInQueryResultItem();
            ri.SourceField = "CommandGUID";
            ri.TargetField = "Record_Index_1";
            ri.GWDataDBField.FieldName = "Record_Index_1";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "CommandGUID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(ri);

            // OrderID -> Order.OrderID
            ri = new SocketInQueryResultItem();
            ri.SourceField = "PatientID";
            ri.TargetField = "PatientID";

            ri.GWDataDBField.FieldName = "PatientID";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "PatientID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // AccessionNumber -> Order_No
            ri = new SocketInQueryResultItem();
            ri.SourceField = "AccessionNumber";
            ri.TargetField = "Order_No";

            ri.GWDataDBField.FieldName = "Order_No";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "AccessionNumber";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);
            #endregion

            Config.InboundChanels.Add(ch);

            #endregion

            #region Channel for ImageArrival

            ch = new SocketInChannel();
            ch.ChannelName = "ImageArrival";
            ch.Enable = true;
            ch.OperationName = "ImageArrival";
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.AutoUpdateProcessFlag = true;

            #region Query CriterialItems for channel ImageArrival
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;

            // ImageArrival
            SocketInQueryCriteriaItem ciImageArrival = new SocketInQueryCriteriaItem();
            ciImageArrival.Type = QueryCriteriaType.Or;
            ciImageArrival.SourceField = "Commandtype";
            ciImageArrival.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ciImageArrival.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ciImageArrival.Translating.ConstValue = Convert.ToInt32(CommandBase.CommandTypeEnum.IMAGE_ARRIVAL).ToString();
            ciImageArrival.Operator = QueryCriteriaOperator.Equal;
            ch.Rule.QueryCriteria.MappingList.Add(ciImageArrival);
            #endregion

            #region Query Result for channel ImageArrival

            // Commantype -> DataIndex.Event_Type
            ri = new SocketInQueryResultItem();
            ri.SourceField = "Commandtype";
            ri.TargetField = "Event_Type";
            ri.GWDataDBField.FieldName = "Event_Type";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "Commandtype";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.LutName = lt.TableName;
            ri.Translating.Type = TranslatingType.LookUpTable;
            ch.Rule.QueryResult.MappingList.Add(ri);


            // CommandGUID -> DataIndex.Record_Index_1
            ri = new SocketInQueryResultItem();
            ri.SourceField = "CommandGUID";
            ri.TargetField = "Record_Index_1";
            ri.GWDataDBField.FieldName = "Record_Index_1";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = "CommandGUID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(ri);

            // PatientID -> Patient.PatientID
            ri = new SocketInQueryResultItem();
            ri.SourceField = "PatientID";
            ri.TargetField = "PatientID";

            ri.GWDataDBField.FieldName = "PatientID";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // AccessionNumber -> Order.Order_No
            ri = new SocketInQueryResultItem();
            ri.SourceField = "AccessionNumber";
            ri.TargetField = "Order_No";

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
            ri = new SocketInQueryResultItem();
            ri.SourceField = "Performed_startdt";
            ri.TargetField = "Exam_DT";

            ri.GWDataDBField.FieldName = "Exam_DT";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "Performed_startdt";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Important: CommandType=ImageArrival -> Exam_Status = 18
            ri = new SocketInQueryResultItem();
            ri.SourceField = "";
            ri.TargetField = "Exam_Status";

            ri.GWDataDBField.FieldName = "Exam_Status";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.Translating.Type = TranslatingType.FixValue;
            ri.Translating.ConstValue = "18";


            ch.Rule.QueryResult.MappingList.Add(ri);


            #endregion //Channel for ImageArrival

            Config.InboundChanels.Add(ch);

            #endregion

            #endregion


            #region ThrPartDBParameters

            #endregion



            return Config;

        }
        #endregion

       
    }
}
