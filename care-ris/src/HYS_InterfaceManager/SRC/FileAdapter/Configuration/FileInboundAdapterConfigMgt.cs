using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Adapter.Base;


namespace HYS.FileAdapter.Configuration
{    

    public class FileInboundAdapterConfigMgt
    {
        static string _FileName = "FileInboundAdapter.xml";
        public static string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        protected const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        private static FileInboundAdapterConfig _FileInAdapterConfig = new FileInboundAdapterConfig();
        public static FileInboundAdapterConfig FileInAdapterConfig
        {
            get { return _FileInAdapterConfig; }
            set { _FileInAdapterConfig = value; }
        }

        public static bool Load(string fileName)
        {
            try
            {
            
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string strXml = sr.ReadToEnd();
                    _FileInAdapterConfig = XObjectManager.CreateObject(strXml, typeof(FileInboundAdapterConfig)) as FileInboundAdapterConfig;
                    return (_FileInAdapterConfig != null);
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
                if (_FileInAdapterConfig == null) return false;
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string strXml = XMLHeader + _FileInAdapterConfig.ToXMLString();
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

        public static bool LoadDefault()
        {
            _FileInAdapterConfig = new FileInboundAdapterConfig();

            //General
            _FileInAdapterConfig.InGeneralParams.FilePath = "C:\\FILEIN";
            _FileInAdapterConfig.InGeneralParams.FilePrefix = "";
            _FileInAdapterConfig.InGeneralParams.FileSuffix = ".ini";
            _FileInAdapterConfig.InGeneralParams.FileTreatTypeAfterRead = FileInGeneralParams.InFileTreatTypeAfterRead.Move;
            _FileInAdapterConfig.InGeneralParams.InFileMovePath = "C:\\FILEIN\\MOVE";
            _FileInAdapterConfig.InGeneralParams.TimerEnable = true;
            _FileInAdapterConfig.InGeneralParams.TimerInterval = 30000;

            //inbound channel
            _FileInAdapterConfig.InboundChanels.Add(BuildPatientChannel());
            _FileInAdapterConfig.InboundChanels.Add(BuildOrderChannel());
            _FileInAdapterConfig.InboundChanels.Add(BuildReportChannel());

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

        #region Sample Configuratin
        public static FileInChannel BuildPatientChannel()
        {

            #region General
            FileInChannel ch = new FileInChannel();
            ch.ChannelName = "CH_PATIENT";
            ch.Enable = true;
            ch.OperationName = "";

            ch.Rule.AutoUpdateProcessFlag = true;
            ch.Rule.CheckProcessFlag = true;            
            ch.Rule.RuleID = "CH_PATIENT";
            ch.Rule.RuleName = "CH_PATIENT";
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            #endregion

            #region Critera            
            FileInQueryCriteriaItem ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;            
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 1;
            ci.Translating.ConstValue = "00";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 2;
            ci.Translating.ConstValue = "01";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 3;
            ci.Translating.ConstValue = "02";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 4;
            ci.Translating.ConstValue = "03";
            ch.Rule.QueryCriteria.MappingList.Add(ci);
            #endregion

            #region Result Index            
            int iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Index))
            {
                if (f.GetFullFieldName() == GWDataDBField.i_IndexGuid.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_DataDateTime.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_PROCESS_FLAG.GetFullFieldName()) continue;

                //Result
                FileInQueryResultItem ri = new FileInQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;
                
                ri.ThirdPartyDBPatamter.SectionName = "F_INDEX";
                
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".","_");

                ch.Rule.QueryResult.MappingList.Add(ri);
                               
               
            }
            #endregion

            #region Result Patient            
            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Patient))
            {
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_DT.GetFullFieldName()) continue;

                //Result
                FileInQueryResultItem ri = new FileInQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_PATIENT";

                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ch.Rule.QueryResult.MappingList.Add(ri);
                              

            }
            #endregion

            return ch;

        }

        public static FileInChannel BuildOrderChannel()
        {

            #region General
            FileInChannel ch = new FileInChannel();
            ch.ChannelName = "CH_ORDER";
            ch.Enable = true;
            ch.OperationName = "";

            ch.Rule.AutoUpdateProcessFlag = true;
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.RuleID = "CH_PORDER";
            ch.Rule.RuleName = "CH_ORDER";
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            #endregion

            #region Critera
            FileInQueryCriteriaItem ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 1;
            ci.Translating.ConstValue = "10";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 2;
            ci.Translating.ConstValue = "11";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 3;
            ci.Translating.ConstValue = "12";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 4;
            ci.Translating.ConstValue = "13";
            ch.Rule.QueryCriteria.MappingList.Add(ci);
            #endregion

            #region Result Index
            int iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Index))
            {
                if (f.GetFullFieldName() == GWDataDBField.i_IndexGuid.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_DataDateTime.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_PROCESS_FLAG.GetFullFieldName()) continue;


                //Result
                FileInQueryResultItem ri = new FileInQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_INDEX";

                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ch.Rule.QueryResult.MappingList.Add(ri);

               

            }
            #endregion

            #region Result Patient
            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Patient))
            {
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_DT.GetFullFieldName()) continue;



                //Result
                FileInQueryResultItem ri = new FileInQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_PATIENT";

                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ch.Rule.QueryResult.MappingList.Add(ri);


                
                               
            }
            #endregion

            #region Result ORDER
            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Order))
            {
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_DT.GetFullFieldName()) continue;

                //Result
                FileInQueryResultItem ri = new FileInQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_ORDER";

                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ch.Rule.QueryResult.MappingList.Add(ri);

                               
            }
            #endregion

            return ch;

        }

        public static FileInChannel BuildReportChannel()
        {
            #region General
            FileInChannel ch = new FileInChannel();
            ch.ChannelName = "CH_REPORT";
            ch.Enable = true;
            ch.OperationName = "";

            ch.Rule.AutoUpdateProcessFlag = true;
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.RuleID = "CH_REPORT";
            ch.Rule.RuleName = "CH_REPORT";
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            #endregion

            #region Critera
            FileInQueryCriteriaItem ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 1;
            ci.Translating.ConstValue = "30";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 2;
            ci.Translating.ConstValue = "31";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 3;
            ci.Translating.ConstValue = "32";
            ch.Rule.QueryCriteria.MappingList.Add(ci);



            //CRITERIA
            ci = new FileInQueryCriteriaItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.SourceField = "F_INDEX_EVENT_TYPE";
            //ci.TargetField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();            
            ci.ThirdPartyDBPatamter.FieldName = "EVENT_TYPE";
            ci.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
            ci.ThirdPartyDBPatamter.SectionName = "F_INDEX";
            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 4;
            ci.Translating.ConstValue = "33";
            ch.Rule.QueryCriteria.MappingList.Add(ci);
            #endregion

            #region Result Index
            int iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Index))
            {
                if (f.GetFullFieldName() == GWDataDBField.i_IndexGuid.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_DataDateTime.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.i_PROCESS_FLAG.GetFullFieldName()) continue;

                //Result
                FileInQueryResultItem ri = new FileInQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_INDEX";

                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ch.Rule.QueryResult.MappingList.Add(ri);

               
                               
            }
            #endregion

            #region Result Patient
            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Patient))
            {
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.p_DATA_DT.GetFullFieldName()) continue;


                //Result
                FileInQueryResultItem ri = new FileInQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_PATIENT";

                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ch.Rule.QueryResult.MappingList.Add(ri);

                                
                               
            }
            #endregion

            #region Result ORDER
            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Order))
            {
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_DT.GetFullFieldName()) continue;


                //Result
                FileInQueryResultItem ri = new FileInQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_ORDER";

                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ch.Rule.QueryResult.MappingList.Add(ri);

              
            }
            #endregion

            #region Result Report
            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Report))
            {
                if (f.GetFullFieldName() == GWDataDBField.r_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.r_DATA_DT.GetFullFieldName()) continue;



                //Result
                FileInQueryResultItem ri = new FileInQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_REPORT";

                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ch.Rule.QueryResult.MappingList.Add(ri);

              
            }
            #endregion

            return ch;          
        }
        #endregion

       
    }
}
