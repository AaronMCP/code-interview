using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;

namespace SQLInboundAdapter
{
    [Obsolete("Do not use this class in production code", false)]
    class TestCase
    {
        //Build a test configuration file
        static public bool BuildTestConfigFile()
        {
            #region 3rd database connection
            SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.OracleDriver = false;
            SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.TimerEnable = true;
            SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.TimerInterval = 1000;
            SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr = "Provider=SQLNCLI.1;Data Source=CNSHW9RSZM1X;Password=123456;User ID=sa;Initial Catalog=GWDataDB";
            #endregion


            #region channel StorageProcedure
            SQLInboundChanel ch = new SQLInboundChanel();
            ch.OperationType = ThrPartyDBOperationType.StorageProcedure;
            ch.OperationName = "p_Patient_3rdIN";
            ch.Enable = true;
            //ch.Rule.AutoUpdateProcessFlag = false;
            //ch.Rule.CheckProcessFlag = true;
            ch.Rule.RuleName = "p_patient_3rdIn";

            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.None; //base mapping list

            #region SQLInQueryCriteria
            SQLInQueryCriteriaItem ci = new SQLInQueryCriteriaItem();
            ci.Type = QueryCriteriaType.And;
            ci.Translating.Type = TranslatingType.DefaultValue;
            ci.Translating.ConstValue = "0";
            ci.RedundancyFlag = false;
            ci.SourceField = "process_flag";
            ci.ThirdPartyDBPatamter.FieldID = 0;
            ci.ThirdPartyDBPatamter.FieldName = ci.SourceField;
            ci.ThirdPartyDBPatamter.FieldType = OleDbType.Integer;
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            ci = new SQLInQueryCriteriaItem();
            ci.Type = QueryCriteriaType.And;
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Translating.ConstValue = "1";
            ci.RedundancyFlag = false;
            ci.SourceField = "event_type";
            ci.ThirdPartyDBPatamter.FieldID = 0;
            ci.ThirdPartyDBPatamter.FieldName = ci.SourceField;
            ci.ThirdPartyDBPatamter.FieldType = OleDbType.Integer;
            ch.Rule.QueryCriteria.MappingList.Add(ci);   
            #endregion

            #region SQLInQueryResultItem
            // column Patient_test.patientid
            SQLInQueryResultItem map = new SQLInQueryResultItem();            
            map.SourceField = "patientid";
            map.TargetField = "patientid";
            map.RedundancyFlag = true;
            map.ThirdPartyDBPatamter.FieldID = 0;
            map.ThirdPartyDBPatamter.FieldName = "PatientID";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.Integer;
            ch.Rule.QueryResult.MappingList.Add(map);

            // column Patient_test.patient_name
            map = new SQLInQueryResultItem();
            map.SourceField = "Patient_name";
            map.TargetField = "Patient_name";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 1;
            map.ThirdPartyDBPatamter.FieldName = "Patient_name";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(map);
            // column Patient_test.address
            map = new SQLInQueryResultItem();
            map.SourceField = "Address";
            map.TargetField = "Address";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 2;
            map.ThirdPartyDBPatamter.FieldName = "Address";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(map);
            // column patient_test.birthdate
            map = new SQLInQueryResultItem();
            map.SourceField = "BirthDate";
            map.TargetField = "BirthDate";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 3;
            map.ThirdPartyDBPatamter.FieldName = "BirthDate";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.Date;
            ch.Rule.QueryResult.MappingList.Add(map);
            #endregion

            SQLInAdapterConfigMgt.SQLInAdapterConfig.InboundChanels.Add(ch);
            #endregion


            #region channel Table
            ch = new SQLInboundChanel();
            ch.OperationType = ThrPartyDBOperationType.Table;
            ch.OperationName = "dbo.Patient_3rdIN";
            ch.Enable = false;
            //ch.Rule.AutoUpdateProcessFlag = false;
            //ch.Rule.CheckProcessFlag = true;
            ch.Rule.RuleName = "patient_3rdIn";

            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.SQLStatement;
            ch.Rule.QueryCriteria.SQLStatement = "process_flag=0 and event_type=1";

            
            #region SQLInQueryResultItem
            // column Patient_test.patientid
            map = new SQLInQueryResultItem();
            map.SourceField = "patientid";
            map.TargetField = "patientid";
            map.RedundancyFlag = true;
            map.ThirdPartyDBPatamter.FieldID = 0;
            map.ThirdPartyDBPatamter.FieldName = "PatientID";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.Integer;
            ch.Rule.QueryResult.MappingList.Add(map);

            // column Patient_test.patient_name
            map = new SQLInQueryResultItem();
            map.SourceField = "Patient_name";
            map.TargetField = "Patient_name";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 1;
            map.ThirdPartyDBPatamter.FieldName = "Patient_name";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(map);
            // column Patient_test.address
            map = new SQLInQueryResultItem();
            map.SourceField = "Address";
            map.TargetField = "Address";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 2;
            map.ThirdPartyDBPatamter.FieldName = "Address";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(map);
            // column patient_test.birthdate
            map = new SQLInQueryResultItem();
            map.SourceField = "BirthDate";
            map.TargetField = "BirthDate";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 3;
            map.ThirdPartyDBPatamter.FieldName = "BirthDate";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.Date;
            ch.Rule.QueryResult.MappingList.Add(map);
            #endregion

            SQLInAdapterConfigMgt.SQLInAdapterConfig.InboundChanels.Add(ch);
            #endregion

            //save
            return SQLInAdapterConfigMgt.Save(SQLInAdapterConfigMgt._FileName);
        }
        //Load data emulated service
        static public void OnDataReceived(IRule rule, DataSet dsData)
        {
            dsData.Tables[0].WriteXml(DateTime.Now.ToString("hh-mm-ss") + ".xml");
        }

               
    }
}
