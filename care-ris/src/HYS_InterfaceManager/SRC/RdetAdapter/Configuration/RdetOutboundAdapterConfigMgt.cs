using System;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;


namespace HYS.RdetAdapter.Configuration
{

    public class RdetOutboundAdapterConfigMgt
    {
        static string _FileName = "RdetOutboundAdapter.xml";
        public static string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        protected const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        private static RdetOutboundAdapterConfig _RdetOutAdapterConfig = new RdetOutboundAdapterConfig();
        public static RdetOutboundAdapterConfig RdetOutAdapterConfig
        {
            get { return _RdetOutAdapterConfig; }
            set { _RdetOutAdapterConfig = value; }
        }

        public static bool Load(string fileName)
        {
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string strXml = sr.ReadToEnd();
                    _RdetOutAdapterConfig = XObjectManager.CreateObject(strXml, typeof(RdetOutboundAdapterConfig)) as RdetOutboundAdapterConfig;
                    return (_RdetOutAdapterConfig != null);
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
                if (_RdetOutAdapterConfig == null) return false;
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string strXml = XMLHeader + _RdetOutAdapterConfig.ToXMLString();
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
        public static RdetOutboundAdapterConfig BuildSampleConfig()
        {
            RdetOutboundAdapterConfig Config = new RdetOutboundAdapterConfig();

            #region General Params
            Config.OutGeneralParams.TimerEnable = true;
            Config.OutGeneralParams.TimerInterval = 1000 * 30;
            #endregion


            #region Client Rdet Params
            Config.ClientRdetParams.ServerIP = "127.0.0.1";
            Config.ClientRdetParams.ServerPort = 58431;
            #endregion

            #region Look up Table
            LookupTable ltEventType = new LookupTable();
            ltEventType.DisplayName = "EventType To Command ";
            //ltEventType.Table.Add(new LookupItem("00", "NewPatient"));
            //ltEventType.Table.Add(new LookupItem("01", "UpdatePatient"));
            ltEventType.Table.Add(new LookupItem("10", "NewPatient"));
            ltEventType.Table.Add(new LookupItem("11", "UpdatePatient"));
            ltEventType.Table.Add(new LookupItem("99", "NewImage"));
            Config.LookupTables.Add(ltEventType);



            LookupTable ltSex = new LookupTable();
            ltSex.DisplayName = "Sex To Gender ";
            ltSex.Table.Add(new LookupItem("M", "1"));
            ltSex.Table.Add(new LookupItem("F", "2"));
            ltSex.Table.Add(new LookupItem("O", "3"));
            ltSex.Table.Add(new LookupItem("U", "3"));
            Config.LookupTables.Add(ltSex);
            
            #endregion


            #region Channel List
            RdetOutChannel ch;
            RdetOutQueryResultItem ri;

            #region Channel for NewPatient,UpdatePatient <- NewOrder and updateorder
            // Actuall New Order -> New patient and Update Order -> Update Patient

            ch = new RdetOutChannel();
            ch.ChannelName = "NewPatient/UpdatePatient";
            ch.Enable = true;
            ch.OperationName = "NewPatient/UpdatePatient";
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.AutoUpdateProcessFlag = true;

            #region Query CriterialItems for channel Order
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;

            // Order
            RdetOutQueryCriterialItem ciOrder = new RdetOutQueryCriterialItem();
            ciOrder.Type = QueryCriteriaType.Or;
            ciOrder.SourceField = "Event_Type";
            ciOrder.GWDataDBField.FieldName = "Event_Type";
            ciOrder.GWDataDBField.Table = GWDataDBTable.Index;
            ciOrder.Translating.Type = TranslatingType.FixValue;
            ciOrder.Translating.ConstValue = "10"; // new order
            ciOrder.Operator = QueryCriteriaOperator.Equal;
            ch.Rule.QueryCriteria.MappingList.Add(ciOrder);

            ciOrder = new RdetOutQueryCriterialItem();
            ciOrder.Type = QueryCriteriaType.Or;
            ciOrder.SourceField = "Event_Type";
            ciOrder.GWDataDBField.FieldName = "Event_Type";
            ciOrder.GWDataDBField.Table = GWDataDBTable.Index;
            ciOrder.Translating.Type = TranslatingType.FixValue;
            ciOrder.Translating.ConstValue = "11"; // update order
            ciOrder.Operator = QueryCriteriaOperator.Equal;
            ch.Rule.QueryCriteria.MappingList.Add(ciOrder);

            ciOrder = new RdetOutQueryCriterialItem();
            ciOrder.Type = QueryCriteriaType.Or;
            ciOrder.SourceField = "Event_Type";
            ciOrder.GWDataDBField.FieldName = "Event_Type";
            ciOrder.GWDataDBField.Table = GWDataDBTable.Index;
            ciOrder.Translating.Type = TranslatingType.FixValue;
            ciOrder.Translating.ConstValue = "01"; // update patient
            ciOrder.Operator = QueryCriteriaOperator.Equal;
            ch.Rule.QueryCriteria.MappingList.Add(ciOrder);

            ciOrder = new RdetOutQueryCriterialItem();
            ciOrder.Type = QueryCriteriaType.Or;
            ciOrder.SourceField = "Event_Type";
            ciOrder.GWDataDBField.FieldName = "Event_Type";
            ciOrder.GWDataDBField.Table = GWDataDBTable.Index;
            ciOrder.Translating.Type = TranslatingType.FixValue;
            ciOrder.Translating.ConstValue = "00"; // add patient
            ciOrder.Operator = QueryCriteriaOperator.Equal;
            ch.Rule.QueryCriteria.MappingList.Add(ciOrder);

            #endregion

            #region Query Result for channel Order -> New/Update patient
                        
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Data_ID";
            ri.TargetField = "Data_ID";
            ri.GWDataDBField.FieldName = "Data_ID";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Data_DT";
            ri.TargetField = "Data_DT";
            ri.GWDataDBField.FieldName = "Data_DT";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Event_Type";
            ri.TargetField = "Command";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.GWDataDBField.FieldName = "Event_Type";
            ri.ThirdPartyDBPatamter.FieldName = "Command";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.Type = TranslatingType.LookUpTable;
            ri.Translating.LutName = ltEventType.TableName;
            ch.Rule.QueryResult.MappingList.Add(ri);


            // Order.Study_Instance_UID -> StudyInstanceUID
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Study_Instance_UID";
            ri.TargetField = "StudyInstanceUID";

            ri.GWDataDBField.FieldName = "Study_Instance_UID";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "StudyInstanceUID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);


            // Patient.PatientID  -> PatientID
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "PatientID";
            ri.TargetField = "PatientID";

            ri.GWDataDBField.FieldName = "PatientID";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Patient.PatientName -> PatientName 
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Patient_Name";
            ri.TargetField = "PatientName";

            ri.GWDataDBField.FieldName = "Patient_Name";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientName";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Patient.BirthDate -> BirthDate
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "BirthDate";
            ri.TargetField = "BirthDate";

            ri.GWDataDBField.FieldName = "BirthDate";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "BirthDate";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            //sex -> gender
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "sex";
            ri.TargetField = "Gender";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;
            ri.GWDataDBField.FieldName = "sex";
            ri.ThirdPartyDBPatamter.FieldName = "Gender";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.Type = TranslatingType.LookUpTable;
            ri.Translating.LutName = ltSex.TableName;
            ch.Rule.QueryResult.MappingList.Add(ri);


            // Order.Order_No -> AccessionNumber
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Order_No";
            ri.TargetField = "AccessionNumber";

            ri.GWDataDBField.FieldName = "Order_No";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "AccessionNumber";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);


            // Order.Scheduled_DT -> StudyDate
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Scheduled_DT";
            ri.TargetField = "StudyDate";

            ri.GWDataDBField.FieldName = "Scheduled_DT";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "StudyDate";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);


            // Order.Scheduled_DT -> StudyTime
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Scheduled_DT";
            ri.TargetField = "StudyTime";

            ri.GWDataDBField.FieldName = "Scheduled_DT";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "StudyTime";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);


            // Order.Scheduled_DT -> StudyTime
            //ri = new RdetOutQueryResultItem();
            //ri.SourceField = "Scheduled_DT";
            //ri.TargetField = "StudyTime";

            //ri.GWDataDBField.FieldName = "Scheduled_DT";
            //ri.GWDataDBField.Table = GWDataDBTable.Order;

            //ri.ThirdPartyDBPatamter.FieldName = "StudyTime";
            //ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            //ch.Rule.QueryResult.MappingList.Add(ri);


            // Order.Customer_1 -> Priority
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Customer_1";
            ri.TargetField = "Priority";

            ri.GWDataDBField.FieldName = "Customer_1";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "Priority";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.Type = TranslatingType.DefaultValue;
            ri.Translating.ConstValue = "1";  //3=STAT,2=Urgent,1=Routine
            ch.Rule.QueryResult.MappingList.Add(ri);

            // Patient.Procedure_Name -> ProcedureName
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Procedure_Name";
            ri.TargetField = "ProcedureName";

            ri.GWDataDBField.FieldName = "Procedure_Name";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "ProcedureName";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Patient.Procedure_Code -> ProcedureCode
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Procedure_Code";
            ri.TargetField = "ProcedureCode";

            ri.GWDataDBField.FieldName = "Procedure_Code";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "ProcedureCode";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Patient.Customer_1 -> PatientComments
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Customer_1";
            ri.TargetField = "PatientComments";

            ri.GWDataDBField.FieldName = "Customer_1";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientComments";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Order.REF_Physician -> ReferringPhysician
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "REF_Physician";
            ri.TargetField = "ReferringPhysician";

            ri.GWDataDBField.FieldName = "REF_Physician";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "ReferringPhysician";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Order.Placer_Department -> Department
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Placer_Department";
            ri.TargetField = "Department";

            ri.GWDataDBField.FieldName = "Placer_Department";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "Department";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Patient.Patient_Location -> PatientLocation
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Patient_Location";
            ri.TargetField = "PatientLocation";

            ri.GWDataDBField.FieldName = "Patient_Location";
            ri.GWDataDBField.Table = GWDataDBTable.Patient;

            ri.ThirdPartyDBPatamter.FieldName = "PatientLocation";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);


            // Order.CNT_Agent -> ContrastAgent
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "CNT_Agent";
            ri.TargetField = "ContrastAgent";

            ri.GWDataDBField.FieldName = "CNT_Agent";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "ContrastAgent";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Order.Customer_2 -> TechID
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Customer_2";
            ri.TargetField = "TechID";

            ri.GWDataDBField.FieldName = "Customer_2";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "TechID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Order.Study_ID -> StudyID
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Study_ID";
            ri.TargetField = "StudyID";

            ri.GWDataDBField.FieldName = "Study_ID";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "StudyID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Order.Station_Name -> StationName
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Station_Name";
            ri.TargetField = "StationName";

            ri.GWDataDBField.FieldName = "Station_Name";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "StationName";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            // Order.Station_AETitle -> AETitle
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Station_AETitle";
            ri.TargetField = "AETitle";

            ri.GWDataDBField.FieldName = "Station_AETitle";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "AETitle";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

                  
            #endregion

            Config.OutboundChanels.Add(ch);

            #endregion//Channel for Order


            #region Channel For UpdatePatient
            //ch = new RdetOutChannel();
            //ch.ChannelName = "Out_CRUDPatient";
            //ch.Enable = true;
            //ch.OperationName = "Out_CRUDPatient";
            //ch.Rule.CheckProcessFlag = true;
            //ch.Rule.AutoUpdateProcessFlag = true;

            //#region CriterialItems
            //ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;

            //// Patient Add
            //RdetOutQueryCriterialItem ciPatientAdd = new RdetOutQueryCriterialItem();
            //ciPatientAdd.Type = QueryCriteriaType.And;
            //ciPatientAdd.SourceField = "Event_Type";
            //ciPatientAdd.GWDataDBField.FieldName = "Event_Type";
            //ciPatientAdd.GWDataDBField.Table = GWDataDBTable.Index;
            //ciPatientAdd.Translating.Type = TranslatingType.FixValue;
            //ciPatientAdd.Translating.ConstValue = "03";
            //ciPatientAdd.Operator = QueryCriteriaOperator.EqualSmallerThan;
            //ch.Rule.QueryCriteria.MappingList.Add(ciPatientAdd);

            //ciPatientAdd = new RdetOutQueryCriterialItem();
            //ciPatientAdd.Type = QueryCriteriaType.And;
            //ciPatientAdd.SourceField = "Event_Type";
            //ciPatientAdd.GWDataDBField.FieldName = "Event_Type";
            //ciPatientAdd.GWDataDBField.Table = GWDataDBTable.Index;
            //ciPatientAdd.Translating.Type = TranslatingType.FixValue;
            //ciPatientAdd.Translating.ConstValue = "00";
            //ciPatientAdd.Operator = QueryCriteriaOperator.EqualLargerThan;
            //ch.Rule.QueryCriteria.MappingList.Add(ciPatientAdd);

            //#endregion

            //#region Query Result

            //// Commantype -> DataIndex.Event_Type
            //ri = new RdetOutQueryResultItem();
            //ri.SourceField = "Data_ID";
            //ri.TargetField = "Data_ID";
            //ri.GWDataDBField.FieldName = "Data_ID";
            //ri.GWDataDBField.Table = GWDataDBTable.Index;
            //ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            //ch.Rule.QueryResult.MappingList.Add(ri);

            //ri = new RdetOutQueryResultItem();
            //ri.SourceField = "Data_DT";
            //ri.TargetField = "Data_DT";
            //ri.GWDataDBField.FieldName = "Data_DT";
            //ri.GWDataDBField.Table = GWDataDBTable.Index;
            //ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            //ch.Rule.QueryResult.MappingList.Add(ri);

            //ri = new RdetOutQueryResultItem();
            //ri.SourceField = "Event_Type";
            //ri.TargetField = "Commandtype";
            //ri.GWDataDBField.FieldName = "Event_Type";
            //ri.GWDataDBField.Table = GWDataDBTable.Index;
            //ri.ThirdPartyDBPatamter.FieldName = "Commandtype";
            //ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            //ri.Translating.Type = TranslatingType.LookUpTable;
            //ri.Translating.LutName = lt.TableName;
            //ch.Rule.QueryResult.MappingList.Add(ri);



            //// CommandGUID -> DataIndex.Record_Index_1
            //ri = new RdetOutQueryResultItem();
            //ri.SourceField = "Record_Index_1";
            //ri.TargetField = "CommandGUID";
            //ri.GWDataDBField.FieldName = "Record_Index_1";
            //ri.GWDataDBField.Table = GWDataDBTable.Index;
            //ri.ThirdPartyDBPatamter.FieldName = "CommandGUID";
            //ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            //ch.Rule.QueryResult.MappingList.Add(ri);

            //// PatientID -> Patient.PatientID
            //ri = new RdetOutQueryResultItem();
            //ri.SourceField = "PatientID";
            //ri.TargetField = "PatientID";

            //ri.GWDataDBField.FieldName = "PatientID";
            //ri.GWDataDBField.Table = GWDataDBTable.Patient;

            //ri.ThirdPartyDBPatamter.FieldName = "PatientID";
            //ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            //ch.Rule.QueryResult.MappingList.Add(ri);

            //// PatientName -> Patient_Name
            //ri = new RdetOutQueryResultItem();
            //ri.SourceField = "PatientName";
            //ri.TargetField = "Patient_Name";

            //ri.GWDataDBField.FieldName = "Patient_Name";
            //ri.GWDataDBField.Table = GWDataDBTable.Patient;

            //ri.ThirdPartyDBPatamter.FieldName = "PatientName";
            //ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            //ch.Rule.QueryResult.MappingList.Add(ri);
            //#endregion

            //Config.OutboundChanels.Add(ch);
            #endregion

            #region Channel for NewImage

            ch = new RdetOutChannel();
            ch.ChannelName = "NewImage";
            ch.Enable = false;
            ch.OperationName = "NewImage";
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.AutoUpdateProcessFlag = true;

            #region Query CriterialItems for channel NewImage
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;

            // ImageArrival
            RdetOutQueryCriterialItem ciImageArrival = new RdetOutQueryCriterialItem();
            ciImageArrival.Type = QueryCriteriaType.And;
            ciImageArrival.SourceField = "Event_Type";
            ciImageArrival.GWDataDBField.FieldName = "Event_Type";
            ciImageArrival.GWDataDBField.Table = GWDataDBTable.Index;
            ciImageArrival.Translating.Type = TranslatingType.FixValue;
            ciImageArrival.Translating.ConstValue = "100"; // New Image
            ciImageArrival.Operator = QueryCriteriaOperator.EqualSmallerThan;
            ch.Rule.QueryCriteria.MappingList.Add(ciImageArrival);
            
            #endregion

            #region Query Result for channel NewImage

            // Set Commantype = Convert.ToInt32(CommandBase.CommandTypeEnum.IMAGE_ARRIVAL).ToString();
            
            // Get Data_ID ,but not send to third database
            ri.SourceField = "Data_ID";
            ri.TargetField = "Data_ID";
            ri.GWDataDBField.FieldName = "Data_ID";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Data_DT";
            ri.TargetField = "Data_DT";
            ri.GWDataDBField.FieldName = "Data_DT";
            ri.GWDataDBField.Table = GWDataDBTable.Index;
            ri.ThirdPartyDBPatamter.FieldName = ""; //flag not send to third database
            ch.Rule.QueryResult.MappingList.Add(ri);

            ri = new RdetOutQueryResultItem();
            ri.SourceField = "";
            ri.TargetField = "Command";
            ri.ThirdPartyDBPatamter.FieldName = "Command";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ri.Translating.Type = TranslatingType.FixValue;
            ri.Translating.ConstValue = "NewImage";
            ch.Rule.QueryResult.MappingList.Add(ri);


            // Order.Study_Instance_UID -> StudyInstanceUID
            ri = new RdetOutQueryResultItem();
            ri.SourceField = "Study_Instance_UID";
            ri.TargetField = "StudyInstanceUID";

            ri.GWDataDBField.FieldName = "Study_Instance_UID";
            ri.GWDataDBField.Table = GWDataDBTable.Order;

            ri.ThirdPartyDBPatamter.FieldName = "StudyInstanceUID";
            ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            ch.Rule.QueryResult.MappingList.Add(ri);

            //// AccessionNumber -> Order.Order_No
            //ri = new RdetOutQueryResultItem();
            //ri.SourceField = "Order_No";
            //ri.TargetField = "AccessionNumber"; 

            //ri.GWDataDBField.FieldName = "Order_No";
            //ri.GWDataDBField.Table = GWDataDBTable.Order;

            //ri.ThirdPartyDBPatamter.FieldName = "AccessionNumber";
            //ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;

            //ch.Rule.QueryResult.MappingList.Add(ri);

            // ModalityName -> Order.Modality


            // OperatorName -> Order.???

            //TODO: Identity OperatorName

            // Performed_enddt -> 

            //TODO: Identity Performed_enddt

            // Performed_startdt -> Order.Exam_DT
            ri = new RdetOutQueryResultItem();
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
                        

            #endregion


            #region ThrPartDBParameters

            #endregion

            return Config;

        }
        #endregion
    }
}
