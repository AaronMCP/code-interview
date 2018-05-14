using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;

namespace SQLOutboundAdapter
{
    [Obsolete("Do not use this class in production code", false)]
    class TestCase
    {
        //Build a test configuration file
        static public bool BuildTestConfigFile()
        {
            #region 3rd database connection
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.Database = "GWDataDB";
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.Server = "CNSHW9RSZM1X";
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.User = "sa";
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.Password="123456";

            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerEnable = true;
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerInterval = 1000;
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr = "Provider=SQLNCLI.1;Data Source=CNSHW9RSZM1X;Password=123456;User ID=sa;Initial Catalog=GWDataDB";
            #endregion


            #region channel Patient_test_sp
            SQLOutboundChanel ch = new SQLOutboundChanel();
            ch.Enable = true;
            ch.ChannelName = "Patient_test_SP";
            ch.OperationType = ThrPartyDBOperationType.StorageProcedure;
            ch.OperationName = "dbo.p_Patient_test";
            ch.Rule.AutoUpdateProcessFlag = false;
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.RuleName = "sp_testpatient";

            // column Patient_test.patientid
            SQLOutQueryResultItem map = new SQLOutQueryResultItem();            
            map.SourceField = "patientid";
            map.TargetField = "patientid";
            map.RedundancyFlag = true;
            map.ThirdPartyDBPatamter.FieldID = 0;
            map.ThirdPartyDBPatamter.FieldName = "PatientID";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.Integer;
            ch.Rule.QueryResult.MappingList.Add(map);

            // column Patient_test.patient_name
            map = new SQLOutQueryResultItem();
            map.SourceField = "Patient_name";
            map.TargetField = "Patient_name";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 1;
            map.ThirdPartyDBPatamter.FieldName = "Patient_name";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(map);
            // column Patient_test.address
            map = new SQLOutQueryResultItem();
            map.SourceField = "Address";
            map.TargetField = "Address";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 2;
            map.ThirdPartyDBPatamter.FieldName = "Address";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(map);
            // column patient_test.birthdate
            map = new SQLOutQueryResultItem();
            map.SourceField = "BirthDate";
            map.TargetField = "BirthDate";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 3;
            map.ThirdPartyDBPatamter.FieldName = "BirthDate";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.Date;
            ch.Rule.QueryResult.MappingList.Add(map);
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels.Add(ch);
            #endregion

            #region channel Patient_test_table
            ch = new SQLOutboundChanel();
            ch.ChannelName = "Patient_test_table";
            ch.Enable = false;
            ch.OperationType = ThrPartyDBOperationType.Table;
            ch.OperationName = "dbo.p_Patient_test";
            ch.Rule.AutoUpdateProcessFlag = false;
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.RuleName = "sp_testpatient";

            // column Patient_test.patientid
            map = new SQLOutQueryResultItem();
            map.SourceField = "patientid";
            map.TargetField = "patientid";
            map.RedundancyFlag = true;
            map.ThirdPartyDBPatamter.FieldID = 0;
            map.ThirdPartyDBPatamter.FieldName = "PatientID";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.Integer;
            ch.Rule.QueryResult.MappingList.Add(map);

            // column Patient_test.patient_name
            map = new SQLOutQueryResultItem();
            map.SourceField = "Patient_name";
            map.TargetField = "Patient_name";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 1;
            map.ThirdPartyDBPatamter.FieldName = "Patient_name";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(map);
            // column Patient_test.address
            map = new SQLOutQueryResultItem();
            map.SourceField = "Address";
            map.TargetField = "Address";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 2;
            map.ThirdPartyDBPatamter.FieldName = "Address";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ch.Rule.QueryResult.MappingList.Add(map);
            // column patient_test.birthdate
            map = new SQLOutQueryResultItem();
            map.SourceField = "BirthDate";
            map.TargetField = "BirthDate";
            map.RedundancyFlag = false;
            map.ThirdPartyDBPatamter.FieldID = 3;
            map.ThirdPartyDBPatamter.FieldName = "BirthDate";
            map.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.Date;
            ch.Rule.QueryResult.MappingList.Add(map);
            SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels.Add(ch);
            #endregion
                        
            //save
            return SQLOutAdapterConfigMgt.Save(SQLOutAdapterConfigMgt._FileName);
        }
        //Load data emulated service
        static public DataSet OnDataRequest(IRule rule, DataSet dsCriteria)
        {
            string ConnStr = "Provider=SQLNCLI.1;Data Source=CNSHW9RSZM1X;Password=123456;User ID=sa;Initial Catalog=GWDataDB";
            OleDbConnection conn = new OleDbConnection(ConnStr);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand("", conn);
            cmd.CommandText = "select * from v_patient_test where process_flag=0";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            
            DataSet dsResult = new DataSet();
            da.Fill(dsResult);
            if (dsResult.Tables[0].Rows.Count > 0)
                return dsResult;
            else
                return null;
        }

        static public bool OnDataDischarge(string[] guid)
        {
            string ConnStr = "Provider=SQLNCLI.1;Data Source=CNSHW9RSZM1X;Password=123456;User ID=sa;Initial Catalog=GWDataDB";
            OleDbConnection conn = new OleDbConnection(ConnStr);
            OleDbCommand cmd = new OleDbCommand("", conn);
            conn.Open();
            try
            {
                foreach (string item in guid)
                {
                    cmd.CommandText = "update SOA2_DATAINDEX set process_flag =1 where data_id=" + "'" + item + "'";
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            finally
            {
                conn.Close();
            }
        }
        
    }
}
