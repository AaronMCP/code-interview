using System;
using System.Data;
using System.Text;
using System.Threading;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HYS.Common.Xml;
using HYS.Adapter.Base;
using HYS.Common.Dicom;
using HYS.Common.Dicom.Net;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;
using HYS.DicomAdapter.MWLServer.Adapter;
using HYS.DicomAdapter.MWLServer.Objects;
using HYS.DicomAdapter.Common;
using Dicom;

namespace HYS.DicomAdapter.MWLServer.Dicom
{
    public class WorklistSCPHelper
    {
        private static int num = 0;
        public static string GetRandomNumber()
        {
            return GetRandomNumber(16); // the DICOM limitation of RPID and SPSID

            ////Random rnd = new Random((int)DateTime.Now.Ticks);// Interlocked.Increment(ref num));
            ////return rnd.Next(1, int.MaxValue);
            //////return DateTime.Now.Ticks;

            //string str = unchecked(DateTime.Now.Ticks + Interlocked.Increment(ref num)).ToString();
            //if (str.Length > 16) str = str.Substring(str.Length - 16);
            //return str;
        }

        /// <summary>
        /// 20100227
        /// To avoid generating duplicated number,
        /// the maxLength should better always be longer than 10, see the following implementation logic for details.
        /// Please use the GUI tool of Kodak.GCGateway.DicomAdapter.MWLServer.Forms.FormAutoGenIDs 
        /// to test the possibility of generating duplicated number.
        /// </summary>
        /// <returns></returns>
        public static string GetRandomNumber(int maxLength)
        {
            string str = unchecked(DateTime.Now.Ticks + Interlocked.Increment(ref num)).ToString();
            if (str.Length > maxLength) str = str.Substring(str.Length - maxLength);
            return str;
        }


        private static Exception _lastError;
        public static Exception LastError
        {
            get { return _lastError; }
        }
        private static void SetError(Exception err)
        {
            _lastError = err;
            if (OnError != null) OnError(typeof(DicomMappingHelper), EventArgs.Empty);
        }
        public static event EventHandler OnError;

        private class Tags
        {
            public const int ScheduledProcedureStepSequence = 0x00400100;
            public const int ScheduledProtocolCodeSequence = 0x00400008;
        }

        public class DataColumns
        {
            private static string _scheduledProtocolCodeSequence;
            public static string ScheduledProtocolCodeSequence
            {
                get
                {
                    if (_scheduledProtocolCodeSequence == null)
                    {
                        //DElement eleSQ = new DElement(0x0040, 0x0100, DVR.SQ, DValueType.Type1);
                        //DElement ele = new DElement(0x0040, 0x0008, DVR.SQ, DValueType.Type1);

                        DicomTag eleSQ = new DicomTag(0x0040, 0x0100);
                        string desc = eleSQ.DictionaryEntry.Keyword;
                        DPath pathSQ = DPath.GetDPath(eleSQ, DVR.SQ, desc, "", -1);

                        DicomTag ele = new DicomTag(0x0040, 0x0008);
                        desc = ele.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(ele, DVR.SQ, desc, pathSQ.Path, 0);

                        _scheduledProtocolCodeSequence = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _scheduledProtocolCodeSequence;
                }
            }

            private static string _scheduledProcedureStepSequence;
            public static string ScheduledProcedureStepSequence
            {
                get
                {
                    if (_scheduledProcedureStepSequence == null)
                    {
                        //DElement eleSQ = new DElement(0x0040, 0x0100, DVR.SQ, DValueType.Type1);
                        //DPath path = DPath.GetDPath(eleSQ);
                        DicomTag eleSQ = new DicomTag(0x0040, 0x0100);
                        string desc = eleSQ.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(eleSQ, DVR.SQ, desc, "", -1);

                        _scheduledProcedureStepSequence = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _scheduledProcedureStepSequence;
                }
            }

            private static string _requestedProcedureCodeSequence;
            public static string RequestedProcedureCodeSequence
            {
                get
                {
                    if (_requestedProcedureCodeSequence == null)
                    {
                        //DElement eleSQ = new DElement(0x0032, 0x1064, DVR.SQ, DValueType.Type1);
                        //DPath path = DPath.GetDPath(eleSQ);
                        DicomTag eleSQ = new DicomTag(0x0032, 0x1064);
                        string desc = eleSQ.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(eleSQ, DVR.SQ, desc, "", -1);

                        _requestedProcedureCodeSequence = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _requestedProcedureCodeSequence;
                }
            }

            private static string _codeValueOfScheduledProtocolCodeSequence;
            public static string CodeValueOfScheduledProtocolCodeSequence
            {
                get
                {
                    if (_codeValueOfScheduledProtocolCodeSequence == null)
                    {
                        //DElement eleRoot = new DElement(0x0040, 0x0100, DVR.SQ, DValueType.Type1);
                        //DElement eleSQ = new DElement(0x0040, 0x0008, DVR.SQ, DValueType.Type1);
                        //DElement ele = new DElement(0x0008, 0x0100, DVR.SH, DValueType.Type1);

                        DicomTag eleRoot = new DicomTag(0x0040, 0x0100);
                        string desc = eleRoot.DictionaryEntry.Keyword;
                        DPath pathRoot = DPath.GetDPath(eleRoot, DVR.SQ, desc, "", -1);

                        DicomTag eleSQ = new DicomTag(0x0040, 0x0008);
                        desc = eleSQ.DictionaryEntry.Keyword;
                        DPath pathSQ = DPath.GetDPath(eleSQ, DVR.SQ, desc, pathRoot.Path, 0);

                        DicomTag ele = new DicomTag(0x0008, 0x0100);
                        desc = ele.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(ele, DVR.SH, desc, pathSQ.Path, 0);

                        _codeValueOfScheduledProtocolCodeSequence = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _codeValueOfScheduledProtocolCodeSequence;
                }
            }

            private static string _codeValueOfRequestedProcedureCodeSequence;
            public static string CodeValueOfRequestedProcedureCodeSequence
            {
                get
                {
                    if (_codeValueOfRequestedProcedureCodeSequence == null)
                    {
                        //DElement eleSQ = new DElement(0x0032, 0x1064, DVR.SQ, DValueType.Type1);
                        //DElement ele = new DElement(0x0008, 0x0100, DVR.SH, DValueType.Type1);

                        //DPath pathSQ = DPath.GetDPath(eleSQ);
                        //DPath path = DPath.GetDPath(ele, pathSQ.Path, 0);
                        DicomTag eleSQ = new DicomTag(0x0032, 0x1064);
                        string desc = eleSQ.DictionaryEntry.Keyword;
                        DPath pathSQ = DPath.GetDPath(eleSQ, DVR.SQ, desc, "", -1);

                        DicomTag ele = new DicomTag(0x0008, 0x0100);
                        desc = ele.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(ele, DVR.SQ, desc, pathSQ.Path, 0);

                        _codeValueOfRequestedProcedureCodeSequence = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _codeValueOfRequestedProcedureCodeSequence;
                }
            }

            private static string _scheduledProcedureStepID;
            public static string ScheduledProcedureStepID
            {
                get
                {
                    if (_scheduledProcedureStepID == null)
                    {
                        //DElement eleSQ = new DElement(0x0040, 0x0100, DVR.SQ, DValueType.Type1);
                        //DElement ele = new DElement(0x0040, 0x0009, DVR.SH, DValueType.Type1);

                        //DPath pathSQ = DPath.GetDPath(eleSQ);
                        //DPath path = DPath.GetDPath(ele, pathSQ.Path, 0);

                        DicomTag eleSQ = new DicomTag(0x0040, 0x0100);
                        string desc = eleSQ.DictionaryEntry.Keyword;
                        DPath pathSQ = DPath.GetDPath(eleSQ, DVR.SQ, desc, "", -1);

                        DicomTag ele = new DicomTag(0x0040, 0x0009);
                        desc = ele.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(ele, DVR.SQ, desc, pathSQ.Path, 0);

                        _scheduledProcedureStepID = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _scheduledProcedureStepID;
                }
            }

            private static string _requestedProcedureID;
            public static string RequestedProcedureID
            {
                get
                {
                    if (_requestedProcedureID == null)
                    {
                        //DElement ele = new DElement(0x0040, 0x1001, DVR.SH, DValueType.Type1);

                        DicomTag eleSQ = new DicomTag(0x0040, 0x1001);
                        string desc = eleSQ.DictionaryEntry.Keyword;
                        DPath pathSQ = DPath.GetDPath(eleSQ, DVR.SQ, desc, "", -1);

                        _requestedProcedureID = DicomMappingHelper.DPath2DataColumnName(pathSQ);
                    }
                    return _requestedProcedureID;
                }
            }

            private static string _studyInstanceUID;
            public static string StudyInstanceUID
            {
                get
                {
                    if (_studyInstanceUID == null)
                    {
                        DicomTag ele = new DicomTag(0x0020, 0x000D);
                        string desc = ele.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(ele, DVR.UI, desc, "", -1);
                        _studyInstanceUID = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _studyInstanceUID;
                }
            }

            private static string _accessionNumber;
            public static string AccessionNumber
            {
                get
                {
                    if (_accessionNumber == null)
                    {
                        DicomTag ele = new DicomTag(0x0008, 0x0050);
                        string desc = ele.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(ele, DVR.SH, desc, "", -1);

                        _accessionNumber = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _accessionNumber;
                }
            }

            private static string _patientID;
            public static string PatientID
            {
                get
                {
                    if (_patientID == null)
                    {
                        DicomTag ele = new DicomTag(0x0010, 0x0020);
                        string desc = ele.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(ele, DVR.LO, desc, "", -1);
                        _patientID = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _patientID;
                }
            }

            private static string _scheduledStationAETitle;
            public static string ScheduledStationAETitle
            {
                get
                {
                    if (_scheduledStationAETitle == null)
                    {
                        //DElement eleSQ = new DElement(0x0040, 0x0100, DVR.SQ, DValueType.Type1);
                        //DElement ele = new DElement(0x0040, 0x0001, DVR.AE, DValueType.Type1);

                        //DPath pathSQ = DPath.GetDPath(eleSQ);
                        //DPath path = DPath.GetDPath(ele, pathSQ.Path, 0);
                        DicomTag eleSQ = new DicomTag(0x0040, 0x0100);
                        string desc = eleSQ.DictionaryEntry.Keyword;
                        DPath pathSQ = DPath.GetDPath(eleSQ, DVR.SQ, desc, "", -1);

                        DicomTag ele = new DicomTag(0x0040, 0x0001);
                        desc = ele.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(ele, DVR.AE, desc, pathSQ.Path, -1);

                        _scheduledStationAETitle = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _scheduledStationAETitle;
                }
            }

            private static string _scheduledStationName;
            public static string ScheduledStationName
            {
                get
                {
                    if (_scheduledStationName == null)
                    {
                        //DElement eleSQ = new DElement(0x0040, 0x0100, DVR.SQ, DValueType.Type1);
                        //DElement ele = new DElement(0x0040, 0x0010, DVR.SH, DValueType.Type1);

                        //DPath pathSQ = DPath.GetDPath(eleSQ);
                        //DPath path = DPath.GetDPath(ele, pathSQ.Path, 0);

                        DicomTag eleSQ = new DicomTag(0x0040, 0x0100);
                        string desc = eleSQ.DictionaryEntry.Keyword;
                        DPath pathSQ = DPath.GetDPath(eleSQ, DVR.SQ, desc, "", -1);

                        DicomTag ele = new DicomTag(0x0040, 0x0100);
                        desc = ele.DictionaryEntry.Keyword;
                        DPath path = DPath.GetDPath(ele, DVR.SH, desc, pathSQ.Path, -1);

                        _scheduledStationName = DicomMappingHelper.DPath2DataColumnName(path);
                    }
                    return _scheduledStationName;
                }
            }
        }

        private static void UpdateID(DataTable dt, DataColumn dc, string columnName, GWDataDBField field)
        {
            if (dc.ColumnName == columnName)
            {
                Dictionary<string, string> rpIDs = new Dictionary<string, string>();
                foreach (DataRow dr in dt.Rows)
                {
                    object o = dr[dc];
                    string rpID = (o == null) ? "" : o.ToString();
                    if (rpID.Length < 1)
                    {
                        string guid = DicomMappingHelper.GetGUID(dr);
                        if (rpIDs.ContainsKey(guid))
                        {
                            // 20100222
                            // please see the function UpdateGUID() for details

                            rpID = rpIDs[guid];
                        }
                        else
                        {
                            rpID = GetRandomNumber(Program.ConfigMgt.Config.MaxAutoGeneratedLengthOfRPIDAndSPSID).ToString();
                            rpIDs.Add(guid, rpID);
                        }
                        dr[dc] = rpID;
                    }
                }
                if (rpIDs.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (KeyValuePair<string, string> pair in rpIDs)
                    {
                        string tableName = GWDataDB.GetTableName(Program.InterfaceName, GWDataDBTable.Order);
                        string pkName = GWDataDBField.o_DATA_ID.FieldName;
                        string fieldName = field.FieldName;
                        sb.Append("UPDATE ").Append(tableName)
                            .Append(" SET ").Append(fieldName).Append(" = '").Append(pair.Value).Append("'")
                            .Append(" WHERE ").Append(pkName).Append(" = '").Append(pair.Key).Append("';\r\n");
                    }
                    //Program.Log.Write(sb.ToString());
                    if (Program.Database.DoQuery(sb.ToString()) == null)
                    {
                        Program.Log.Write(LogType.Error, "Update " + columnName + " into database failed.");
                    }
                    else
                    {
                        Program.Log.Write("Update " + columnName + " into database succeeded.");
                    }
                }
            }
        }
        private static void UpdateGUID(DataTable dt, DataColumn dc, string columnName, GWDataDBField field)
        {
            if (dc.ColumnName == columnName)
            {
                Dictionary<string, string> rpIDs = new Dictionary<string, string>();
                foreach (DataRow dr in dt.Rows)
                {
                    object o = dr[dc];
                    string rpID = (o == null) ? "" : o.ToString();
                    if (rpID.Length < 1)    // need to generate GUID
                    {
                        string guid = DicomMappingHelper.GetGUID(dr);
                        if (rpIDs.ContainsKey(guid))
                        {
                            // 20100222
                            // has already generated GUID in this loop, 
                            // for example there are two records with same guid and splitted according to multiple procedure codes

                            // 20100227
                            // as we move the auto generating STDUID before the procedure code spliting in the class Kodak.GCGateway.DicomAdapter.MWLServer.Dicom.WorklistSCP,
                            // this (different records with the same guid/data_id) will not happen any more.

                            rpID = rpIDs[guid];
                        }
                        else
                        {
                            rpID = DHelper.GetDicomGUID(Program.DeviceMgt.DeviceDirInfor.Header.ID, Program.ConfigMgt.Config.MaxAutoGeneratedLengthOfSTDUID);
                            rpIDs.Add(guid, rpID);
                        }
                        dr[dc] = rpID;
                    }
                }
                if (rpIDs.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (KeyValuePair<string, string> pair in rpIDs)
                    {
                        string tableName = GWDataDB.GetTableName(Program.InterfaceName, GWDataDBTable.Order);
                        string pkName = GWDataDBField.o_DATA_ID.FieldName;
                        string fieldName = field.FieldName;
                        sb.Append("UPDATE ").Append(tableName)
                            .Append(" SET ").Append(fieldName).Append(" = '").Append(pair.Value).Append("'")
                            .Append(" WHERE ").Append(pkName).Append(" = '").Append(pair.Key).Append("';\r\n");
                    }
                    if (Program.Database.DoQuery(sb.ToString()) == null)
                    {
                        Program.Log.Write(LogType.Error, "Update " + columnName + " into database failed.");
                    }
                    else
                    {
                        Program.Log.Write("Update " + columnName + " into database succeeded.");
                    }
                }
            }
        }
        private static void LookupAETitle(DataTable dt, DataColumn dc, string columnName, string callingIP)
        {
            if (dc.ColumnName != columnName) return;
            if (Program.ConfigMgt.Config.SCPConfig.KnownModalities.Count < 1)
            {
                Program.Log.Write(LogType.Warning, "There is no modality in the list to look up for AE title.");
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                object o = dr[dc];
                string aeTitle = (o == null) ? "" : o.ToString();
                if (aeTitle.Length < 1)    // need to look up AE title
                {
                    aeTitle = Program.ConfigMgt.Config.SCPConfig.FindAETitleByCallingIP(callingIP);
                    Program.Log.Write(string.Format("Find AE title: {0} by calling IP.", aeTitle));
                    dr[dc] = aeTitle;
                }
            }
        }
        private static void LookupDescription(DataTable dt, DataColumn dc, string columnName, string callingIP)
        {
            if (dc.ColumnName != columnName) return;
            if (Program.ConfigMgt.Config.SCPConfig.KnownModalities.Count < 1)
            {
                Program.Log.Write(LogType.Warning, "There is no modality in the list to look up for description.");
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                object o = dr[dc];
                string aeTitle = (o == null) ? "" : o.ToString();
                if (aeTitle.Length < 1)    // need to look up description
                {
                    aeTitle = Program.ConfigMgt.Config.SCPConfig.FindDescriptionByCallingIP(callingIP);
                    Program.Log.Write(string.Format("Find description: {0} by calling IP.", aeTitle));
                    dr[dc] = aeTitle;
                }
            }
        }
        private static GWDataDBField GetGWField(string targetField)
        {
            foreach (MWLQueryResultItem item in Program.ConfigMgt.Config.Rule.QueryResult.MappingList)
            {
                if (item.TargetField == targetField) return item.GWDataDBField;
            }
            return null;
        }
        public static void GenerateRequestedProcedureID(DataSet ds, Session session)
        {
            if (ds == null || ds.Tables.Count < 1) return;

            DataTable dt = ds.Tables[0];
            foreach (DataColumn dc in dt.Columns)
            {
                if (Program.ConfigMgt.Config.AutoGenerateRPID)
                {
                    GWDataDBField field = GetGWField(DataColumns.RequestedProcedureID);
                    if (field != null) UpdateID(dt, dc, DataColumns.RequestedProcedureID, field);
                }
                if (Program.ConfigMgt.Config.AutoGenerateSPSPID)
                {
                    GWDataDBField field = GetGWField(DataColumns.ScheduledProcedureStepID);
                    if (field != null) UpdateID(dt, dc, DataColumns.ScheduledProcedureStepID, field);
                }
                if (Program.ConfigMgt.Config.AutoGenerateSTDUID)
                {
                    GWDataDBField field = GetGWField(DataColumns.StudyInstanceUID);
                    if (field != null) UpdateGUID(dt, dc, DataColumns.StudyInstanceUID, field);
                }
                if (Program.ConfigMgt.Config.LookupAETitleByIPInModalityListForScheduledStationAETitle)
                {
                    GWDataDBField field = GetGWField(DataColumns.ScheduledStationAETitle);
                    if (field != null && session != null)
                        LookupAETitle(dt, dc, DataColumns.ScheduledStationAETitle, session.CallingIP);
                }
                if (Program.ConfigMgt.Config.LookupDescriptionByIPInModalityListForScheduledStationName)
                {
                    GWDataDBField field = GetGWField(DataColumns.ScheduledStationName);
                    if (field != null && session != null)
                        LookupDescription(dt, dc, DataColumns.ScheduledStationName, session.CallingIP);
                }
            }
        }

        private static bool SplitDataRow_old(DataSet ds)
        {
            bool res = false;
            if (!Program.ConfigMgt.Config.SplitCodeValue) return res;
            if (ds == null || ds.Tables.Count < 1) return res;

            string cName = Program.ConfigMgt.Config.CodeValueColumnName;
            char seperator = Program.ConfigMgt.Config.CodeValueSeperator;
            if (cName == null || cName.Length < 1 || seperator == '\0') return res;

            DataTable dt = ds.Tables[0];
            if (!dt.Columns.Contains(cName)) return res;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string str = dr[cName] as string;
                if (str == null) continue;

                string[] strList = str.Trim(seperator).Split(seperator);
                if (strList.Length < 2) continue;

                res = true;
                dr[cName] = strList[strList.Length - 1];
                for (int j = strList.Length - 2; j >= 0; j--)
                {
                    DataRow newRow = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName == cName)
                        {
                            newRow[dc] = strList[j];
                        }
                        else
                        {
                            newRow[dc] = dr[dc];
                        }
                    }
                    dt.Rows.InsertAt(newRow, i);
                }
            }

            return res;
        }

        /// <summary>
        /// 20090901
        /// - fix the hidden bug of iterating the row collection while adding new row into it.
        /// - support splitting value in all columns selected from cs broker database.
        /// 20100222
        /// - fix bug in 20090901 about splitting values in any columns (e.g. code meaning columns) other than the code value columns) and release patch formally
        /// 20100227
        /// - append an ascending number to a specific field when spliting procedure codes 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static bool SplitDataRow(DataSet ds)
        {
            if (!Program.ConfigMgt.Config.SplitCodeValue) return false;
            if (ds == null || ds.Tables.Count < 1) return false;

            string cName = Program.ConfigMgt.Config.CodeValueColumnName;
            char seperator = Program.ConfigMgt.Config.CodeValueSeperator;
            if (cName == null || cName.Length < 1 || seperator == '\0') return false;

            DataTable dt = ds.Tables[0];
            if (!dt.Columns.Contains(cName)) return false;

            Dictionary<DataRow, List<DataRow>> dicNewRows = new Dictionary<DataRow, List<DataRow>>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string str = dr[cName] as string;
                if (str == null) continue;

                string[] strList = str.Split(seperator);
                if (strList.Length < 2) continue;               // check wether this record has multiple values in the procedure code field

                for (int j = strList.Length - 1; j >= 0; j--)
                {
                    DataRow newRow = dt.NewRow();               // generating new record, each for one procedure code
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.ColumnName == cName)                         // splitting the values in the procedure code field
                        {
                            newRow[dc] = strList[j];
                        }
                        else
                        {
                            string orgValue = dr[dc] as string;
                            if (orgValue == null) newRow[dc] = dr[dc];      // this is a protection
                            else
                            {
                                string[] orgValueList = orgValue.Split(seperator);
                                if (orgValueList.Length < 2) newRow[dc] = orgValue;     // copying the value of other fields without multiple values
                                else                                                    // splitting the value of other fields with mulitple values
                                {
                                    if (j >= 0 && j <= orgValueList.Length - 1) newRow[dc] = orgValueList[j];
                                    else newRow[dc] = "";
                                }
                            }
                        }

                        if (Program.ConfigMgt.Config.AppendNumberToColumnAccordingToCodeValueSplitting)
                        {
                            if (dc.ColumnName == Program.ConfigMgt.Config.TheColumndAppendedNumberToAccordingToCodeValueSplitting)
                            {
                                string strValue = newRow[dc] as string;         // append an ascending number to the specific field
                                strValue += "-" + (j + 1).ToString();
                                newRow[dc] = strValue;
                            }
                        }
                    }

                    if (!dicNewRows.ContainsKey(dr)) dicNewRows.Add(dr, new List<DataRow>());
                    dicNewRows[dr].Add(newRow);
                }
            }

            if (dicNewRows.Count > 0)
            {
                foreach (KeyValuePair<DataRow, List<DataRow>> p in dicNewRows)
                {
                    DataRow oldRow = p.Key;
                    int index = dt.Rows.IndexOf(oldRow);
                    if (index < 0) continue;

                    dt.Rows.RemoveAt(index);
                    foreach (DataRow newRow in p.Value)
                    {
                        dt.Rows.InsertAt(newRow, index);
                    }
                }
            }

            return dicNewRows.Count > 0;
        }

        private static string GetValueSafe(DicomDataset list, DicomTag tag)
        {
            if (list == null) return null;

            string ele = list.Get<string>(tag);
            if (ele == null) return null;

            string value = ele;
            if (value == null || value.Length < 1) return null;

            return value;
        }

        #region process query result

        public static DElementListWrapper[] CreateQRElementList<TR>(XCollection<TR> qrList, DataSet dsQR)
            where TR : QueryResultItem, IDicomMappingItem
        {
            if (qrList == null || dsQR == null || dsQR.Tables.Count < 1) return null;

            DataTable dt = dsQR.Tables[0];
            string pkColumn = Program.ConfigMgt.Config.PrimaryKeyColumnName;
            if (!dt.Columns.Contains(pkColumn)) return null;

            Dictionary<string, List<DataRow>> drDict = new Dictionary<string, List<DataRow>>();
            foreach (DataRow dr in dt.Rows)
            {
                string key = null;
                string pkValue = dr[pkColumn] as string;
                //if (pkValue != null || pkValue.Length > 0)
                if (pkValue != null && pkValue.Length > 0)
                {
                    key = pkValue;
                }
                else
                {
                    key = "_" + dt.Rows.IndexOf(dr).ToString();
                }

                if (drDict.ContainsKey(key))
                {
                    drDict[key].Add(dr);
                }
                else
                {
                    List<DataRow> drList = new List<DataRow>();
                    drDict.Add(key, drList);
                    drList.Add(dr);
                }
            }

            List<DElementListWrapper> dcmResult = new List<DElementListWrapper>();
            foreach (KeyValuePair<string, List<DataRow>> pair in drDict)
            {
                int count = qrList.Count;
                DicomDataset dcmEleList = new DicomDataset();
                for (int index = 0; index < count; index++)
                {
                    IDicomMappingItem qr = qrList[index] as IDicomMappingItem;
                    if (!qr.DPath.Enable) continue;
                    //int tag = qr.DPath.GetTag(0);
                    List<string> tagList = qr.DPath.GetTagGE(0);
                    ushort uGroup = DHelper.HexString2ushort(tagList[0]);
                    ushort uElement = DHelper.HexString2ushort(tagList[1]);
                    DicomTag tag = new DicomTag(uGroup, uElement);
                    FillElement<TR>(dcmEleList, tag, qr.DPath.VR, ref index, 0, qrList, pair.Value.ToArray());
                    //dcmEleList.Add(ele);
                }
                DElementListWrapper w = new DElementListWrapper(dcmEleList);
                foreach (DataRow dr in pair.Value) w.GUIDs.Add(DicomMappingHelper.GetGUID(dr));
                dcmResult.Add(w);
            }

            return dcmResult.ToArray();
        }

        private static void FillElement<TR>(DicomDataset ds, DicomTag tagIn, DVR vrIn, ref int index, int depth, XCollection<TR> qrList, DataRow[] drList)
            where TR : QueryResultItem, IDicomMappingItem
        {
            if (drList.Length < 1) return;
            DataRow dr = drList[0];

            TR qr = qrList[index] as TR;
            if (!qr.DPath.Enable) return;

            if (qr.DPath.VR == DVR.Unknown) return;
            else if (qr.DPath.VR == DVR.SQ)
            {
                index++;
                int count = qrList.Count;
                List<DicomDataset> eleListSequence = null;
                bool isProctolCodeSQ = (qr.DPath.GetTag() == Tags.ScheduledProtocolCodeSequence); //0x00400008

                // use this to create a temporary patch to Japan according to requirement of sending multiple items in Requested Procedure Sequence, which is not allow in DICOM standard, 20090403
                //bool isProctolCodeSQ = (qr.DPath.GetTag() == 0x00321064); 

                for (; index < count; index++)
                {
                    qr = qrList[index] as TR;
                    switch (qr.DPath.Type)
                    {
                        case DPathType.BeginItem:
                            {
                                eleListSequence = new List<DicomDataset>();
                                if (isProctolCodeSQ)
                                {
                                    foreach (DataRow d in drList)
                                    {
                                        DicomDataset eleList = new DicomDataset();
                                        eleListSequence.Add(eleList);
                                    }

                                    //Program.Log.Write("--- Begin Protocol Code Sequence ---" + eleListSequence.Count.ToString());
                                }
                                else
                                {
                                    //Program.Log.Write("--- Begin Sequence ---" + DHelper.GetTagName((uint)rootSQElement.Tag));

                                    DicomDataset eleList = new DicomDataset();
                                    eleListSequence.Add(eleList);
                                }
                                continue;
                            }
                        case DPathType.EndItem:
                            {
                                if (eleListSequence == null) continue;

                                DicomSequence dicomSequence = new DicomSequence(tagIn);
                                if (isProctolCodeSQ)
                                {
                                    foreach (DicomDataset eleList in eleListSequence)
                                    {
                                        dicomSequence.Items.Add(eleList);
                                    }

                                    //Program.Log.Write("--- End Protocol Code Sequence ---" + rootSQElement.Sequence.Count.ToString());
                                }
                                else
                                {
                                    //Program.Log.Write("--- End Sequence ---" + DHelper.GetTagName((uint)rootSQElement.Tag));

                                    if (eleListSequence.Count > 0)
                                        dicomSequence.Items.Add(eleListSequence[0]);
                                }

                                TR qrNext = qrList[index + 1];
                                if (qrNext == null || qrNext.DPath.Type != DPathType.BeginItem) return;
                                continue;
                            }
                        default:
                            {
                                if (eleListSequence == null) continue;
                                if (!qr.DPath.Enable) continue;
                                //int tag = qr.DPath.GetTag(depth + 1);
                                List<string> tagList = qr.DPath.GetTagGE(depth);
                                ushort uGroup = DHelper.HexString2ushort(tagList[0]);
                                ushort uElement = DHelper.HexString2ushort(tagList[1]);
                                DicomTag tag = new DicomTag(uGroup, uElement);
                                if (isProctolCodeSQ)
                                {
                                    int beginIndex = index;
                                    for (int i = 0; i < eleListSequence.Count; i++)
                                    {
                                        if (i == 0)
                                        {
                                            FillElement<TR>(eleListSequence[i], tag, qr.DPath.VR, ref index, depth + 1, qrList, new DataRow[] { drList[i] });
                                        }
                                        else
                                        {
                                            FillElement<TR>(eleListSequence[i], tag, qr.DPath.VR, ref beginIndex, depth + 1, qrList, new DataRow[] { drList[i] });
                                        }

                                        //Program.Log.Write("--- Add Protocol Code Sequence Item ---" 
                                        //    + i.ToString() + "  "
                                        //    + DHelper.GetTagName((uint)tag));
                                    }
                                }
                                else
                                {
                                    //Program.Log.Write("--- Add Sequence Item ---" + DHelper.GetTagName((uint)tag));

                                    if (eleListSequence.Count > 0)
                                    {
                                        FillElement<TR>(eleListSequence[0], tag, qr.DPath.VR, ref index, depth + 1, qrList, drList);
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            else
            {
                string value = null;
                object obj = dr[qr.TargetField];
                if (obj != null) value = obj.ToString();
                if (value == null) value = "";

                ds.Add(tagIn, value);

            }
        }

        #endregion

        #region process query criteria

        public static void ModifyQCMappingList_CS<TC>(XCollection<TC> qcList, bool withBracket)
            where TC : QueryCriteriaItem, IDicomMappingItem, new()
        {
            if (qcList == null) return;
            bool found = true;
            while (found)
            {
                found = false;
                int count = qcList.Count;
                for (int index = 0; index < count; index++)
                {
                    TC item = qcList[index];

                    IDicomMappingItem dcmItem = item as IDicomMappingItem;
                    if (dcmItem == null) continue;

                    if (dcmItem.DPath != null &&
                        dcmItem.DPath.VR == DVR.CS &&
                        dcmItem.DPath.Path == DicomMappingHelper.DataColumnName2DPath(item.SourceField))
                    {
                        qcList.Remove(item);

                        if (withBracket)    // for GetRule()
                        {
                            qcList.Insert(index, DicomMappingHelper.GetRightBracket<TC>());
                        }

                        for (int i = Program.ConfigMgt.Config.CSDivisionMAXCount - 1; i >= 0; i--)  // for GetRule() and for NT Service to create QC DataSet
                        {
                            QueryCriteriaItem itemSub = dcmItem.Clone() as QueryCriteriaItem;
                            itemSub.SourceField = itemSub.SourceField + "_" + i.ToString();
                            itemSub.Operator = QueryCriteriaOperator.Like;
                            if (i == 0)
                            {
                                itemSub.Type = QueryCriteriaType.None;
                            }
                            else
                            {
                                itemSub.Type = QueryCriteriaType.Or;
                            }
                            qcList.Insert(index, itemSub);
                        }

                        if (withBracket)    // for GetRule()
                        {
                            TC itemAnd = DicomMappingHelper.GetLeftBracket<TC>();
                            itemAnd.Type = QueryCriteriaType.And;
                            qcList.Insert(index, itemAnd);
                        }

                        found = true;
                        break;
                    }
                }
            }
        }

        public static void ModifyQCDataSet<TC>(XCollection<TC> qcList, DataSet ds)
            where TC : QueryCriteriaItem, IDicomMappingItem, new()
        {
            if (qcList == null || ds == null || ds.Tables.Count < 1) return;
            DataTable dt = ds.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                int count = qcList.Count;
                for (int i = 0; i < count; i++)
                {
                    TC item = qcList[i];
                    if (item.DPath.VR != DVR.CS) continue;

                    if (item.Translating.Type == TranslatingType.FixValue) continue;

                    string value = dr[item.SourceField] as string;
                    if (value == null) value = "";
                    string[] valueList = value.Split(Program.ConfigMgt.Config.CSDivisionSeperator);
                    string path = item.DPath.Path;

                    int index = 0;
                    for (; i < count; i++)
                    {
                        TC csItem = qcList[i];
                        if (csItem.DPath.Path != path)
                        {
                            break;
                        }
                        else
                        {
                            if (index < valueList.Length)
                            {
                                dr[csItem.SourceField] = valueList[index++];
                            }
                            else
                            {
                                dr[csItem.SourceField] = "NULL";
                            }
                        }
                    }
                }
            }
        }

        public static void SetAdditionalQueryCriteria<TC>(XCollection<TC> qcList, XCollection<TC> additionalQCList, QueryCriteriaType additionalQCJoinType)
            where TC : QueryCriteriaItem, IDicomMappingItem, new()
        {
            if (qcList == null || additionalQCList == null) return;

            if (qcList.Count < 1)
            {
                foreach (TC qcItem in additionalQCList) qcList.Add(qcItem);
            }
            else if (additionalQCList.Count > 0)
            {
                qcList[0].Type = QueryCriteriaType.None;
                additionalQCList[0].Type = QueryCriteriaType.None;

                TC qcLeft = new TC();
                qcLeft.Singal = QueryCriteriaSignal.LeftBracket;
                qcLeft.Type = QueryCriteriaType.None;
                qcList.Insert(0, qcLeft);

                TC qcRight = new TC();
                qcRight.Singal = QueryCriteriaSignal.RightBracket;
                qcRight.Type = QueryCriteriaType.None;
                qcList.Add(qcRight);

                TC qcAddLeft = new TC();
                qcAddLeft.Singal = QueryCriteriaSignal.LeftBracket;
                qcAddLeft.Type = additionalQCJoinType;
                qcList.Add(qcAddLeft);

                foreach (TC qcItem in additionalQCList) qcList.Add(qcItem);

                TC qcAddRight = new TC();
                qcAddRight.Singal = QueryCriteriaSignal.RightBracket;
                qcAddRight.Type = QueryCriteriaType.None;
                qcList.Add(qcAddRight);
            }
        }

        #endregion
    }
}
