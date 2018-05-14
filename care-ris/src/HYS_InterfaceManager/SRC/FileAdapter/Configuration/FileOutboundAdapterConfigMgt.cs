using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Adapter.Base;


namespace HYS.FileAdapter.Configuration
{

    public class FileOutboundAdapterConfigMgt
    {
        static string _FileName = "FileOutboundAdapter.xml";
        public static string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        protected const string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";

        private static FileOutboundAdapterConfig _FileOutAdapterConfig = new FileOutboundAdapterConfig();
        public static FileOutboundAdapterConfig FileOutAdapterConfig
        {
            get { return _FileOutAdapterConfig; }
            set { _FileOutAdapterConfig = value; }
        }

        public static bool Load(string fileName)
        {
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string strXml = sr.ReadToEnd();
                    _FileOutAdapterConfig = XObjectManager.CreateObject(strXml, typeof(FileOutboundAdapterConfig)) as FileOutboundAdapterConfig;
                    return (_FileOutAdapterConfig != null);
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
                if (_FileOutAdapterConfig == null) return false;
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    string strXml = XMLHeader + _FileOutAdapterConfig.ToXMLString();
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
            _FileOutAdapterConfig = new FileOutboundAdapterConfig();

            //General
            _FileOutAdapterConfig.OutGeneralParams.FilePath = "C:\\FILEOUT";
            _FileOutAdapterConfig.OutGeneralParams.FilePrefix = "";
            _FileOutAdapterConfig.OutGeneralParams.FileSuffix = ".ini";

            _FileOutAdapterConfig.OutGeneralParams.TimerEnable = true;
            _FileOutAdapterConfig.OutGeneralParams.TimerInterval = 30000;

            //inbound channel
            _FileOutAdapterConfig.OutboundChanels.Add(BuildPatientChannel());
            _FileOutAdapterConfig.OutboundChanels.Add(BuildOrderChannel());
            _FileOutAdapterConfig.OutboundChanels.Add(BuildReportChannel());

            return true;
        }

        /// <summary>
        /// support [%ChannelName%] token, let channecl.name as the part of filename
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static string BuildOutFileName(FileOutChannel ch, DataRow dr)
        {
            if (_FileOutAdapterConfig == null)
                throw new Exception("System cannot initialized!");

            string sFilePath = _FileOutAdapterConfig.OutGeneralParams.FilePath;
            string sFilePrefix = _FileOutAdapterConfig.OutGeneralParams.FilePrefix;
            string sFileSuffix = _FileOutAdapterConfig.OutGeneralParams.FileSuffix;

            string sDatetime = "";
            if(_FileOutAdapterConfig.OutGeneralParams.FileDtFormat.Trim()!="")
                sDatetime   = DateTime.Now.ToString(_FileOutAdapterConfig.OutGeneralParams.FileDtFormat);
            

            if (sFilePath.Trim() == "")
                sFilePath = Application.StartupPath+"\\data\\";
            if (!Directory.Exists(sFilePath))
                Directory.CreateDirectory(sFilePath);

            sFilePath = sFilePath.Trim();
            if (sFilePath.Substring(sFilePath.Length-1,1) != "\\" )
                sFilePath = sFilePath + "\\";

            #region Replace [%Table_Field%] with pValue
            //if (sFilePrefix.Trim().ToUpper() == ("[%ChannelName%]").ToUpper())
            //    sFilePrefix = ch.ChannelName;
            string pValue ;  //replace value
            foreach (GWDataDBField prefield in FileOutAdapterConfig.OutGeneralParams.PreGWDataDBFields)
            {
                pValue = "";
                foreach (FileOutQueryResultItem resultItem in ch.Rule.QueryResult.MappingList)
                {
                    GWDataDBField resultField =  resultItem.GWDataDBField;
                    if (resultField.GetFullFieldName().Trim().ToUpper() == prefield.GetFullFieldName().Trim().ToUpper())
                    {
                        object o = dr[resultItem.TargetField] ;
                        if (Convert.IsDBNull(o))
                            pValue = "";
                        else
                            pValue = Convert.ToString(o);
                        break;
                    }
                }

                //Replace [%Table_Field%] with pValue
                sFilePrefix = sFilePrefix.Replace("[%" + prefield.GetFullFieldName().Replace(".", "_") + "%]", pValue);
            }

            //Gurant no error file name
            sFilePrefix = sFilePrefix.Replace("[%", "");
            sFilePrefix = sFilePrefix.Replace("%]", "");
            #endregion

            if (sFileSuffix.IndexOf(".") < 0)
                sFileSuffix = "." + sFileSuffix;

            string sFileName;
            for (int i = 0; i < Int32.MaxValue; i++)
            {
                if (i == 0)
                    sFileName = sFilePath + sFilePrefix + sDatetime + sFileSuffix;
                else
                    sFileName = sFilePath + sFilePrefix + sDatetime + i.ToString() + sFileSuffix;

                if (!File.Exists(sFileName))
                    return sFileName;
            }
            throw new Exception("Cannot build outfilename, extract out int32.maxvalue!");
            
        }

        public static string BuildOutFieldFileName(ThrPartyDBParamterExOut param)
        {
            if (_FileOutAdapterConfig == null)
                throw new Exception("System cannot initialized!");



            string sFilePath = param.FileFieldParam.FilePath;
            string sFilePrefix = param.FileFieldParam.FilePrefix;
            string sFileSuffix = param.FileFieldParam.FileSuffix;
            string sDatetime = DateTime.Now.ToString(param.FileFieldParam.FileDtFormat);

            if (sFilePath.Trim() == "")
                sFilePath = Application.StartupPath + "\\fielddata\\";
            if (!Directory.Exists(sFilePath))
                Directory.CreateDirectory(sFilePath);

            sFilePath = sFilePath.Trim();
            if (sFilePath.Substring(sFilePath.Length - 1, 1) != "\\")
                sFilePath = sFilePath + "\\";

            if (sFilePrefix.Trim().ToUpper() == ("[%FieldName%]").ToUpper())
                sFilePrefix = param.FieldName;

            if (sFileSuffix.IndexOf(".") < 0)
                sFileSuffix = "." + sFileSuffix;

            string sFileName;
            for (int i = 0; i < Int32.MaxValue; i++)
            {
                if (i == 0)
                    sFileName = sFilePath + sFilePrefix + sDatetime + sFileSuffix;
                else
                    sFileName = sFilePath + sFilePrefix + sDatetime + i.ToString() + sFileSuffix;

                if (!File.Exists(sFileName))
                    return sFileName;
            }
            throw new Exception("Cannot build outfilename, extract out int32.maxvalue!");

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
        public static FileOutChannel BuildPatientChannel()
        {

            #region General
            FileOutChannel ch = new FileOutChannel();
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
            FileOutQueryCriterialItem ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.SourceField  = "EVENT_TYPE";            
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();         
   
            ci.TargetField = "F_INDEX_EVENT_TYPE";

            ci.ThirdPartyDBPatamter.FieldID = 1;
            ci.Translating.ConstValue = "00";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.SourceField = "EVENT_TYPE";
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

            ci.TargetField = "F_INDEX_EVENT_TYPE";

            ci.ThirdPartyDBPatamter.FieldID = 2;
            ci.Translating.ConstValue = "01";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.SourceField = "EVENT_TYPE";
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

            ci.TargetField = "F_INDEX_EVENT_TYPE";

            ci.ThirdPartyDBPatamter.FieldID = 3;
            ci.Translating.ConstValue = "02";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.SourceField = "EVENT_TYPE";
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

            ci.TargetField = "F_INDEX_EVENT_TYPE";

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
                FileOutQueryResultItem ri = new FileOutQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_INDEX";
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.TargetField =  f.GetFullFieldName().ToUpper().Replace(".","_");

                ri.GWDataDBField = f.Clone();
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".","_");
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
                FileOutQueryResultItem ri = new FileOutQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_PATIENT";
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");
                ch.Rule.QueryResult.MappingList.Add(ri);
               
            }
            #endregion

            return ch;

        }

        public static FileOutChannel BuildOrderChannel()
        {
            FileOutChannel ch = new FileOutChannel();

            #region General            
            ch.ChannelName = "CH_ORDER";
            ch.Enable = true;
            ch.OperationName = "";

            ch.Rule.AutoUpdateProcessFlag = true;
            ch.Rule.CheckProcessFlag = true;
            ch.Rule.RuleID = "CH_ORDER";
            ch.Rule.RuleName = "CH_ORDER";
            ch.Rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
            #endregion

            #region Critera
            FileOutQueryCriterialItem ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.TargetField = "F_EVENT_TYPE";
            ci.SourceField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();   
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 1;
            ci.Translating.ConstValue = "10";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.TargetField = "F_EVENT_TYPE";
            ci.SourceField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();   
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 2;
            ci.Translating.ConstValue = "11";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.TargetField = "F_EVENT_TYPE";
            ci.SourceField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();   
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 3;
            ci.Translating.ConstValue = "12";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.TargetField = "F_EVENT_TYPE";
            ci.SourceField = "EVENT_TYPE";
            //ci.GWDataDBField = f.Clone();   
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

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
                FileOutQueryResultItem ri = new FileOutQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_INDEX";
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");
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
                FileOutQueryResultItem ri = new FileOutQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_PATIENT";
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");
                ch.Rule.QueryResult.MappingList.Add(ri);

            }
            #endregion

            #region Result Order
            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Order))
            {
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_DT.GetFullFieldName()) continue;

                //Result
                FileOutQueryResultItem ri = new FileOutQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.ThirdPartyDBPatamter.SectionName = "F_ORDER";
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");
                ch.Rule.QueryResult.MappingList.Add(ri);
                
                
            }
            #endregion

            return ch;
        }

        public static FileOutChannel BuildReportChannel()
        {
            FileOutChannel ch = new FileOutChannel();

            #region General
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
            FileOutQueryCriterialItem ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.TargetField = "F_INDEX_EVENT_TYPE";
            ci.SourceField = "EVENT_TYPE";             
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 1;
            ci.Translating.ConstValue = "30";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.TargetField = "F_INDEX_EVENT_TYPE";
            ci.SourceField = "EVENT_TYPE";
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 2;
            ci.Translating.ConstValue = "31";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.TargetField = "F_INDEX_EVENT_TYPE";
            ci.SourceField = "EVENT_TYPE";
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

            ci.Translating.Type = TranslatingType.FixValue;
            ci.Singal = QueryCriteriaSignal.None;
            ci.RedundancyFlag = false;

            ci.ThirdPartyDBPatamter.FieldID = 3;
            ci.Translating.ConstValue = "32";
            ch.Rule.QueryCriteria.MappingList.Add(ci);

            //CRITERIA
            ci = new FileOutQueryCriterialItem();
            ci.Operator = QueryCriteriaOperator.Equal;
            ci.Type = QueryCriteriaType.Or;
            ci.TargetField = "F_INDEX_EVENT_TYPE";
            ci.SourceField = "EVENT_TYPE";
            ci.GWDataDBField = GWDataDBField.i_EventType.Clone();

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
                FileOutQueryResultItem ri = new FileOutQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;               

                ri.ThirdPartyDBPatamter.SectionName = "F_INDEX";
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");
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
                FileOutQueryResultItem ri = new FileOutQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_"); 
                
                ri.ThirdPartyDBPatamter.SectionName = "F_PATIENT";
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");
                ch.Rule.QueryResult.MappingList.Add(ri);
            }
            #endregion

            #region Result Order
            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Order))
            {
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.o_DATA_DT.GetFullFieldName()) continue;

                //Result
                FileOutQueryResultItem ri = new FileOutQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".","_");
                ri.ThirdPartyDBPatamter.SectionName = "F_ORDER";
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;                
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");
                ch.Rule.QueryResult.MappingList.Add(ri);
            }
            #endregion

            #region Result Order
            iFieldID = 0;
            foreach (GWDataDBField f in GWDataDBField.GetFields(GWDataDBTable.Report))
            {
                if (f.GetFullFieldName() == GWDataDBField.r_DATA_ID.GetFullFieldName()) continue;
                if (f.GetFullFieldName() == GWDataDBField.r_DATA_DT.GetFullFieldName()) continue;

                //Result
                FileOutQueryResultItem ri = new FileOutQueryResultItem();
                ri.Translating.Type = TranslatingType.None;
                ri.RedundancyFlag = false;

                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".","_");
                ri.ThirdPartyDBPatamter.SectionName = "F_REPORT";
                ri.ThirdPartyDBPatamter.FieldID = ++iFieldID;
                ri.ThirdPartyDBPatamter.FieldName = f.FieldName.ToUpper();
                ri.ThirdPartyDBPatamter.FieldType = System.Data.OleDb.OleDbType.VarChar;
                ri.ThirdPartyDBPatamter.FileFieldFlag = false;
                ri.TargetField = f.GetFullFieldName().ToUpper().Replace(".", "_");

                ri.GWDataDBField = f.Clone();
                ri.SourceField = f.GetFullFieldName().ToUpper().Replace(".", "_");
                ch.Rule.QueryResult.MappingList.Add(ri);
            }
            #endregion

            return ch;
        }

        #endregion
    }
}
