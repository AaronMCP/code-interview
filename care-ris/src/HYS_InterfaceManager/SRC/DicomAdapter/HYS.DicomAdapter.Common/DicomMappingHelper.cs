using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Dicom;
using HYS.Common.Objects.Rule;
using Dicom;

namespace HYS.DicomAdapter.Common
{
    public class DicomMappingHelper
    {
        public const string DATAID = "DATAID";

        public static void SetFixValue<TC, TR>(RuleBase<TC, TR> rule)
            where TC : QueryCriteriaItem, IDicomMappingItem, new()
            where TR : QueryResultItem, IDicomMappingItem, new()
        {
            if (rule == null) return;
            // for outbound
            rule.CheckProcessFlag = false;
            rule.QueryCriteria.Type = QueryCriteriaRuleType.DataSet;
        }

        public static void PreperatMappingList<T>(XCollection<T> target, XCollection<T> source)
            where T : XObject, IDicomMappingItem
        {
            if (target == null || source == null) return;

            target.Clear();
            int count = source.Count;
            for (int i = 0; i < source.Count; i++)
            {
                T t = source[i];
                if (t.DPath.Enable == false && t.DPath.Type == DPathType.Normal)
                {
                    if (t.DPath.VR == DVR.SQ) EatDisableSequence(source, ref i);
                    continue;
                }
                target.Add(t);
            }
        }

        private static void EatDisableSequence<T>(XCollection<T> source, ref int i)
            where T : XObject, IDicomMappingItem
        {
            i++;
            for (; i < source.Count; i++)
            {
                if (source[i].DPath.VR == DVR.SQ) EatDisableSequence(source, ref i);
                else if (source[i].DPath.Type == DPathType.EndItem) break;
            }
        }

        public static DataSet CreateQCDataSet<TC>(XCollection<TC> qcList, DicomDataset dcmQC)
            where TC : MappingItem, IDicomMappingItem
        {
            if (dcmQC == null || qcList == null) return null;

            int count = qcList.Count;
            Hashtable table = new Hashtable();
            for (int index = 0; index < count; index++)
            {
                FillDataSet(table, ref index, 0, qcList, dcmQC);
            }

            DataSet ds = new DataSet();
            DataTable dt = ds.Tables.Add();
            foreach (DictionaryEntry de in table)
            {
                string key = de.Key as string;
                dt.Columns.Add(new DataColumn(key, typeof(string)));
            }
            DataRow dr = dt.NewRow();
            foreach (DictionaryEntry de in table)
            {
                string key = de.Key as string;
                string value = de.Value as string;
                dr[key] = value;
            }
            dt.Rows.Add(dr);

            return ds;
        }

        public static void CleanMappingList<TC, TR>(RuleBase<TC, TR> rule)
            where TC : QueryCriteriaItem, IDicomMappingItem, new()
            where TR : QueryResultItem, IDicomMappingItem, new()
        {
            if (rule == null) return;
            CleanQCMappingList<TC>(rule.QueryCriteria.MappingList);
            CleanQRMappingList<TR>(rule.QueryResult.MappingList);
        }

        public static void CleanQRMappingList<TR>(XCollection<TR> qrList)
            where TR : QueryResultItem, IDicomMappingItem, new()
        {
            List<TR> garbageList = new List<TR>();
            foreach (TR item in qrList)
            {
                IDicomMappingItem dcmItem = item as IDicomMappingItem;
                if (dcmItem == null) continue;

                if (!dcmItem.DPath.Enable ||
                    dcmItem.DPath.VR == DVR.SQ ||
                    dcmItem.DPath.VR == DVR.Unknown ||
                    dcmItem.DPath.Type != DPathType.Normal)
                    garbageList.Add(item);
            }
            foreach (TR item in garbageList)
            {
                qrList.Remove(item);
            }
        }

        public static void CleanQCMappingList<TC>(XCollection<TC> qcList)
            where TC : QueryCriteriaItem, IDicomMappingItem, new()
        {
            List<TC> garbageList = new List<TC>();
            foreach (TC item in qcList)
            {
                IDicomMappingItem dcmItem = item as IDicomMappingItem;
                if (dcmItem == null) continue;

                if (!dcmItem.DPath.Enable ||
                    dcmItem.DPath.VR == DVR.SQ ||
                    dcmItem.DPath.VR == DVR.Unknown ||
                    dcmItem.DPath.Type != DPathType.Normal ||
                    dcmItem.GWDataDBField.Table == GWDataDBTable.None)
                    garbageList.Add(item);
            }
            foreach (TC item in garbageList)
            {
                qcList.Remove(item);
            }
        }

        public static string SQLMatchChar = "%";
        public static string DicomMatchChar = "*";
        public static string GetSQLString<TC>(TC qc, string dicomString)
            where TC : MappingItem, IDicomMappingItem
        {
            string str = dicomString;
            if (str == null) str = "";

            if (qc == null || !DHelper.IsStringLike(qc.DPath.VR)) return str;

            if (str.Length < 1) return SQLMatchChar;

            if (SQLMatchChar.Length > 0) str = str.Replace(SQLMatchChar, "[" + SQLMatchChar + "]");
            if (DicomMatchChar.Length > 0) str = str.Replace(DicomMatchChar, SQLMatchChar);

            return str;
        }

        public static DVR ConvertDicomVRToDVR(DicomVR vr)
        {
            return DHelper.ConvertToDVR(vr);
        }

        public static DElementListWrapper[] CreateQRElementList<TR>(XCollection<TR> qrList, DataSet dsQR)
    where TR : QueryResultItem, IDicomMappingItem
        {
            if (dsQR == null || qrList == null) return null;

            List<DElementListWrapper> dcmResult = new List<DElementListWrapper>();
            if (dsQR.Tables.Count > 0)
            {
                foreach (DataRow dr in dsQR.Tables[0].Rows)
                {
                    DicomDataset dcmEleList = new DicomDataset();

                    int count = qrList.Count;
                    for (int index = 0; index < count; index++)
                    {
                        IDicomMappingItem qr = qrList[index] as IDicomMappingItem;
                        if (!qr.DPath.Enable) continue;
                        List<string> tagList = qr.DPath.GetTagGE(0);
                        ushort uGroup = DHelper.HexString2ushort(tagList[0]);
                        ushort uElement = DHelper.HexString2ushort(tagList[1]);
                        DicomTag tag = new DicomTag(uGroup, uElement);
                        FillElement<TR>(dcmEleList, tag, qr.DPath.VR, ref index, 0, qrList, dr);
                    }

                    DElementListWrapper w = new DElementListWrapper(dcmEleList);
                    w.GUIDs.Add(DicomMappingHelper.GetGUID(dr));
                    dcmResult.Add(w);
                }
            }

            return dcmResult.ToArray();
        }

        private static void FillElement<TR>(DicomDataset ds, DicomTag tagIn, DVR vrIn, ref int index, int depth, XCollection<TR> qrList, DataRow dr)
            where TR : QueryResultItem, IDicomMappingItem
        {
            TR qr = qrList[index] as TR;
            if (!qr.DPath.Enable) return;

            switch (qr.DPath.VR)
            {
                case DVR.Unknown: return;
                case DVR.SQ:
                    {
                        index++;
                        int count = qrList.Count;
                        DicomDataset eleList = null;
                        for (; index < count; index++)
                        {
                            qr = qrList[index] as TR;
                            switch (qr.DPath.Type)
                            {
                                case DPathType.BeginItem:
                                    {
                                        eleList = new DicomDataset();
                                        continue;
                                    }
                                case DPathType.EndItem:
                                    {
                                        DicomSequence dicomSequence = new DicomSequence(tagIn);
                                        if (eleList != null) dicomSequence.Items.Add(eleList);
                                        TR qrNext = ((index + 1) < qrList.Count) ? qrList[index + 1] : null;
                                        if (qrNext == null || qrNext.DPath.Type != DPathType.BeginItem) return;
                                        continue;
                                    }
                                default:
                                    {
                                        if (eleList == null) continue;
                                        if (!qr.DPath.Enable) continue;
                                        //int tag = qr.DPath.GetTag(depth + 1);
                                        List<string> tagList = qr.DPath.GetTagGE(depth + 1);
                                        ushort uGroup = DHelper.HexString2ushort(tagList[0]);
                                        ushort uElement = DHelper.HexString2ushort(tagList[1]);
                                        DicomTag tag = new DicomTag(uGroup, uElement);

                                        //DElement ele = new DElement(tag, qr.DPath.VR);
                                        FillElement<TR>(eleList, tag, qr.DPath.VR, ref index, depth + 1, qrList, dr);
                                        //eleList.Add(ele);
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                default:
                    {
                        string value = null;
                        object obj = dr[qr.TargetField];
                        if (obj != null) value = obj.ToString();
                        if (value == null) value = "";

                        if (DHelper.IsDateTime(qr.DPath.VR))
                        {
                            try
                            {
                                if (value.Length > 0)
                                {
                                    DateTime dt = DateTime.Parse(value);
                                    //DDateTime ddt = DDateTime.FromDateTime(qr.DPath.VR, dt);
                                    DicomDateTime ddt = new DicomDateTime(tagIn, dt);
                                    //rootSQElement.Value = ddt.ToDicomString();
                                    ds.Add(ddt);
                                }
                                else
                                {
                                    //rootSQElement.Value = value;
                                    ds.Add<string>(tagIn, value);
                                }
                            }
                            catch (Exception err)
                            {
                                //SetError(err);
                                //rootSQElement.Value = value;
                                ds.Add<string>(tagIn, value);
                            }
                        }
                        else
                        {
                            //rootSQElement.Value = value;
                            ds.Add<string>(tagIn, value);
                        }


                        break;
                    }
            }
        }

        private static void FillDataSet<TC>(Hashtable dr, ref int index, int depth, XCollection<TC> qcList, DicomDataset eleList)
            where TC : MappingItem, IDicomMappingItem
        {
            int count = qcList.Count;
            for (; index < count; index++)
            {
                TC qc = qcList[index] as TC;
                if (qc.Translating.Type == TranslatingType.FixValue) continue;
                if (qc.DPath.Type == DPathType.BeginItem) continue;
                if (qc.DPath.Type == DPathType.EndItem && depth > 0) break;
                if (!qc.DPath.Enable) continue;

                //int tag = qc.DPath.GetTag(depth);
                //DicomItem ele = eleList(tag);
                List<string> tagList = qc.DPath.GetTagGE(depth);
                if (tagList == null)
                    continue;

                ushort uGroup = DHelper.HexString2ushort(tagList[0]);
                ushort uElement = DHelper.HexString2ushort(tagList[1]);
                DicomItem ele = eleList.Get<DicomItem>(new DicomTag(uGroup, uElement));
                if (ele == null || ConvertDicomVRToDVR(ele.ValueRepresentation) != qc.DPath.VR)
                {
                    if (qc.DPath.VR == DVR.SQ) continue;
                    dr[qc.SourceField] = GetSQLString<TC>(qc, (ele == null) ? "" : eleList.Get<string>(new DicomTag(uGroup, uElement)));
                    continue;
                }

                if (qc.DPath.VR == DVR.SQ)
                {
                    int d = depth + 1;
                    DicomDataset sqList = null;
                    DicomSequence dicomSequence = (DicomSequence)ele;
                    if (dicomSequence.Items.Count > 0)     //support one sequence item only
                    {
                        sqList = dicomSequence.Items[0];
                    }
                    index++;
                    if (sqList == null) sqList = new DicomDataset();
                    FillDataSet<TC>(dr, ref index, d, qcList, sqList);
                    break;
                }
                else
                {
                    string value = eleList.Get<string>(new DicomTag(uGroup, uElement));
                    if (DHelper.IsDateTime(qc.DPath.VR))
                    {
                        DVR realVR = qc.DPath.VR;

                        // ------ merge TM to DT ------
                        if (uGroup == DicomTag.ScheduledProcedureStepStartDate.Group && uElement == DicomTag.ScheduledProcedureStepStartDate.Element)
                        {
                            string eleTM = eleList.Get<string>(DicomTag.ScheduledProcedureStepStartTime);
                            if (eleTM != null)
                            {
                                string strTM = eleTM;
                                string[] strTMList = strTM.Split('-');
                                string[] strDAList = value.Split('-');
                                if (strTMList.Length > 0 && strDAList.Length > 0)
                                {
                                    if (strDAList.Length == 1 && strDAList[0].Length > 0)
                                    {
                                        if (strTMList.Length == 1 && strTMList[0].Length > 0)
                                        {
                                            realVR = DVR.DT;
                                            if (strTM.Length > 6) strTM = strTM.Substring(0, 6);
                                            value += strTM;
                                        }
                                        else if (strTMList.Length == 2)
                                        {
                                            realVR = DVR.DT;
                                            string beginTM = strTMList[0];
                                            string endTM = strTMList[1];
                                            if (beginTM.Length < 1) beginTM = "000000";
                                            if (endTM.Length < 1) endTM = "235959";
                                            if (beginTM.Length > 6) beginTM = beginTM.Substring(0, 6);
                                            if (endTM.Length > 6) endTM = endTM.Substring(0, 6);
                                            value = strDAList[0] + beginTM + "-" + strDAList[0] + endTM;
                                        }
                                    }
                                    else if (strDAList.Length == 2)
                                    {
                                        if (strTMList.Length == 1 && strTMList[0].Length > 0)
                                        {
                                            realVR = DVR.DT;
                                            if (strTM.Length > 6) strTM = strTM.Substring(0, 6);
                                            value = strDAList[0] + strTM + "-" + strDAList[1] + strTM;
                                        }
                                        else if (strTMList.Length == 2)
                                        {
                                            realVR = DVR.DT;
                                            string beginTM = strTMList[0];
                                            string endTM = strTMList[1];
                                            if (beginTM.Length < 1) beginTM = "000000";
                                            if (endTM.Length < 1) endTM = "235959";
                                            if (beginTM.Length > 6) beginTM = beginTM.Substring(0, 6);
                                            if (endTM.Length > 6) endTM = endTM.Substring(0, 6);
                                            value = strDAList[0] + beginTM + "-" + strDAList[1] + endTM;
                                        }
                                    }

                                }
                            }
                        }
                        // ----------------------------

                        if (qc.DPath.Range == DRangeType.None)
                        {
                            DDateTime2 singleddt = DDateTime2.FromDateTime(realVR, value);
                            object singledt = (singleddt != null) ? singleddt.GetDateTime() : null;
                            //value = (singledt != null) ? ((DateTime)singledt).ToString(GWDataDB.DateTimeFormat) : "";
                            value = GetGWDTStartString(singledt, realVR);
                            dr[qc.SourceField] = GetSQLString<TC>(qc, value);
                            continue;
                        }

                        TC qcStart = qc;
                        if (qcStart.DPath.Range != DRangeType.Begin) continue;

                        TC qcEnd = qcList[index + 1];
                        if (qcEnd == null || qcEnd.DPath.Range != DRangeType.End) continue;

                        index++;

                        object dtStart = null, dtEnd = null;
                        DDateTime2 ddt = DDateTime2.FromDateTime(realVR, value);
                        if (ddt != null)
                        {
                            switch (ddt.Type)
                            {
                                case DDateTimeType.SINGLE:
                                    {
                                        dtStart = dtEnd = ddt.GetDateTime();
                                        break;
                                    }
                                case DDateTimeType.START_ONLY:
                                    {
                                        dtStart = ddt.GetStartDateTime();
                                        break;
                                    }
                                case DDateTimeType.END_ONLY:
                                    {
                                        dtEnd = ddt.GetEndDateTime();
                                        break;
                                    }
                                case DDateTimeType.RANGE:
                                    {
                                        dtStart = ddt.GetStartDateTime();
                                        dtEnd = ddt.GetEndDateTime();
                                        break;
                                    }
                            }
                        }

                        string strStart = "", strEnd = "";
                        strStart = GetGWDTStartString(dtStart, realVR);
                        strEnd = GetGWDTEndString(dtEnd, realVR);
                        dr[qcStart.SourceField] = strStart;
                        dr[qcEnd.SourceField] = strEnd;
                    }
                    else if (qc.DPath.VR == DVR.PN)
                    {
                        value = PersonNameRule.Parse(value);
                        dr[qc.SourceField] = GetSQLString<TC>(qc, value);
                    }
                    else
                    {
                        dr[qc.SourceField] = GetSQLString<TC>(qc, value);
                    }
                    break;
                }
            }
        }
        private static PersonNameRule _personNameRule;
        public static PersonNameRule PersonNameRule
        {
            get
            {
                if (_personNameRule == null) _personNameRule = new PersonNameRule();
                return _personNameRule;
            }
            set
            {
                _personNameRule = value;
            }
        }

        public static string GetGWDTStartString(object dto, DVR vr)
        {
            if (dto == null)
            {
                return "";
            }

            if (vr == DVR.DA)
            {
                return ((DateTime)dto).ToString(GWDataDB.DateFormat);
            }
            else if (vr == DVR.TM)
            {
                return ((DateTime)dto).ToString(GWDataDB.TimeFormat);
            }
            else if (vr == DVR.DT)
            {
                return ((DateTime)dto).ToString(GWDataDB.DateTimeFormat);
            }

            return "";
        }
        public static string GetGWDTEndString(object dto, DVR vr)
        {
            if (vr == DVR.DA)
            {
                if (dto == null)
                {
                    return DateTime.MaxValue.ToString(GWDataDB.DateFormat);
                }
                else
                {
                    return ((DateTime)dto).ToString(GWDataDB.DateFormat) + " 23:59:59";
                }
            }
            else if (vr == DVR.TM)
            {
                if (dto == null)
                {
                    return DateTime.MaxValue.ToString(GWDataDB.TimeFormat);
                }
                else
                {
                    return ((DateTime)dto).AddSeconds(1).ToString(GWDataDB.TimeFormat);
                }
            }
            else if (vr == DVR.DT)
            {
                if (dto == null)
                {
                    return DateTime.MaxValue.ToString(GWDataDB.DateTimeFormat);
                }
                else
                {
                    return ((DateTime)dto).AddSeconds(1).ToString(GWDataDB.DateTimeFormat);
                }
            }

            return "";
        }

        public static string DPath2DataColumnName(DPath path)
        {
            if (path == null) return "";
            return DPath2DataColumnName(path.Path);
        }
        public static string DPath2DataColumnName(string path)
        {
            if (path == null || path.Length < 1) return "";
            return DPath.Seperator + path;
        }

        public static string GetGUID(DataRow dr)
        {
            if (dr != null)
            {
                object o = dr[DATAID];
                if (o != null) return o.ToString();
            }
            return "";
        }

        public static void ModifyQCMappingList_DateTime<TC>(XCollection<TC> qcList, bool withBracket)
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
                        dcmItem.DPath.Range == DRangeType.None &&
                        DHelper.IsDateTime(dcmItem.DPath.VR))
                    {
                        qcList.Remove(item);

                        QueryCriteriaItem itemB = dcmItem.Clone() as QueryCriteriaItem;
                        ((IDicomMappingItem)itemB).DPath.Range = DRangeType.Begin;
                        itemB.Operator = QueryCriteriaOperator.EqualLargerThan;
                        itemB.SourceField = itemB.SourceField + "_BEGIN";

                        QueryCriteriaItem itemE = dcmItem.Clone() as QueryCriteriaItem;
                        ((IDicomMappingItem)itemE).DPath.Range = DRangeType.End;
                        itemE.Operator = QueryCriteriaOperator.EqualSmallerThan;
                        itemE.SourceField = itemE.SourceField + "_END";

                        if (withBracket)    // for GetRule()
                        {
                            qcList.Insert(index, GetRightBracket<TC>());
                            qcList.Insert(index, GetRightBracket<TC>());

                            TC itemEE = GetFreeText<TC>("@" + itemE.SourceField + "=''");
                            itemEE.Type = QueryCriteriaType.And;
                            qcList.Insert(index, itemEE);

                            TC itemBB = GetFreeText<TC>("@" + itemB.SourceField + "=''");
                            itemBB.Type = QueryCriteriaType.None;
                            qcList.Insert(index, itemBB);

                            TC itemOr = GetLeftBracket<TC>();
                            itemOr.Type = QueryCriteriaType.Or;
                            qcList.Insert(index, itemOr);

                            qcList.Insert(index, GetRightBracket<TC>());

                            itemE.Type = QueryCriteriaType.And;
                            qcList.Insert(index, itemE);

                            itemB.Type = QueryCriteriaType.None;
                            qcList.Insert(index, itemB);

                            qcList.Insert(index, GetLeftBracket<TC>());

                            TC itemAnd = GetLeftBracket<TC>();
                            itemAnd.Type = QueryCriteriaType.And;
                            qcList.Insert(index, itemAnd);
                        }
                        else                // for NT Service to create QC DataSet
                        {
                            qcList.Insert(index, itemE);
                            qcList.Insert(index, itemB);
                        }

                        found = true;
                        break;
                    }
                }
            }
        }

        public static TC GetFreeText<TC>(string text)
           where TC : QueryCriteriaItem, IDicomMappingItem, new()
        {
            TC item = new TC();
            item.Singal = QueryCriteriaSignal.FreeText;
            item.Type = QueryCriteriaType.None;
            item.Translating.Type = TranslatingType.None;
            item.Translating.ConstValue = text;
            return item;
        }

        public static TC GetLeftBracket<TC>()
    where TC : QueryCriteriaItem, IDicomMappingItem, new()
        {
            TC item = new TC();
            item.Singal = QueryCriteriaSignal.LeftBracket;
            item.Type = QueryCriteriaType.None;
            return item;
        }

        public static string DataColumnName2DPath(string cName)
        {
            if (cName == null) return "";
            return cName.TrimStart(DPath.Seperator);
        }

        public static TC GetRightBracket<TC>()
            where TC : QueryCriteriaItem, IDicomMappingItem, new()
        {
            TC item = new TC();
            item.Singal = QueryCriteriaSignal.RightBracket;
            item.Type = QueryCriteriaType.None;
            return item;
        }

        public static XCollection<T> GetSequence<T>(int sqItemIndex, XCollection<T> list)
            where T : XObject, IDicomMappingItem
        {
            if (list == null) return null;
            if (sqItemIndex < -1 || sqItemIndex >= list.Count) return null;
            // if sqItemIndex==-1 return root item list

            if (sqItemIndex >= 0)
            {
                T t = list[sqItemIndex];
                if (t.DPath.Type == DPathType.Normal && t.DPath.VR == DVR.SQ)
                {
                    if (!HasSequence<T>(sqItemIndex, list)) return new XCollection<T>();
                }
            }

            int beginindex, endindex;
            beginindex = endindex = sqItemIndex >= 0 ? sqItemIndex + 1 : -1;

            XCollection<T> sqlist = new XCollection<T>();
            FindEndDPathIndex<T>(ref endindex, list);

            if (beginindex < 0) beginindex = 0;
            if (endindex >= list.Count) endindex = list.Count - 1;
            for (int i = beginindex; i <= endindex; i++)
            {
                T t = list[i];
                if (t.DPath.Type == DPathType.Normal)
                {
                    sqlist.Add(t);
                    if (t.DPath.VR == DVR.SQ && HasSequence<T>(i, list))
                    {
                        FindEndDPathIndex(ref i, list);
                    }
                }
            }

            return sqlist;
        }

        public static bool FindEndDPathIndex<T>(ref int index, XCollection<T> list)
             where T : XObject, IDicomMappingItem
        {
            if (list == null) return false;
            if (index < -1 || index >= list.Count) return false;
            // if index==-1 search from the begining of the list

            for (index++; index < list.Count; index++)
            {
                T iItem = list[index];
                if (iItem.DPath.Type == DPathType.Normal && iItem.DPath.VR == DVR.SQ && HasSequence<T>(index, list))
                {
                    FindEndDPathIndex<T>(ref index, list);
                }
                else if (iItem.DPath.Type == DPathType.EndItem)
                {
                    return true;
                }
            }

            return false;
        }

        public static void DeleteSequence<T>(int sqItemIndex, XCollection<T> list)
            where T : XObject, IDicomMappingItem
        {
            if (list == null) return;
            if (sqItemIndex < 0 || sqItemIndex >= list.Count) return;

            int beginindex, endindex;
            beginindex = endindex = sqItemIndex;
            if (FindEndDPathIndex<T>(ref endindex, list))
            {
                List<T> removeList = new List<T>();
                for (int i = beginindex + 1; i <= endindex; i++) removeList.Add(list[i]);
                foreach (T ritem in removeList) list.Remove(ritem);
            }
        }
        public static bool HasSequence<T>(int sqItemIndex, XCollection<T> list)
            where T : XObject, IDicomMappingItem
        {
            if (list == null) return false;
            if (sqItemIndex < 0 || sqItemIndex + 1 >= list.Count) return false;
            return list[sqItemIndex + 1].DPath.Type == DPathType.BeginItem;
        }

        public static int FindParentSQItemIndex<T>(ref int index, XCollection<T> list)
            where T : XObject, IDicomMappingItem
        {
            if (list == null) return -1;
            if (index < 0 || index >= list.Count) return -1;
            //return -1 if not found

            for (index--; index >= 0; index--)
            {
                T t = list[index];
                if (t.DPath.Type == DPathType.EndItem)
                {
                    FindParentSQItemIndex(ref index, list);
                }
                else if (t.DPath.Type == DPathType.BeginItem)
                {
                    int sqIndex = index - 1;
                    return sqIndex;
                }
            }

            return -1;
        }

        public static DPath[] CreateDPath(DElementList eleList)
        {
            if (eleList == null) return null;
            List<DPath> list = new List<DPath>();
            FillItemArray(list, eleList, null, -1);
            return list.ToArray();
        }

        private static void FillItemArray(List<DPath> list, DElementList eleList, string sqRootPath, int sqIndex)
        {
            foreach (DElement ele in eleList)
            {
                DicomTag tag = ele.DicomTag;
                DPath path = DPath.GetDPath(tag, ele.getVR(), "", sqRootPath, sqIndex);
                path.Enable = (ele.Type != DValueType.Type3 && ele.Type != DValueType.Unknown);
                path.Catagory = ele.Catagory;
                list.Add(path);

                if (ele.VR == DVR.SQ)
                {
                    int index = -1;
                    foreach (DElementList subList in ele.Sequence)
                    {
                        index++;
                        DPath pathBegin = DPath.GetItemGroupPathBegin(index);
                        DPath pathEnd = DPath.GetItemGroupPathEnd(index);
                        list.Add(pathBegin);
                        FillItemArray(list, subList, path.Path, index);
                        list.Add(pathEnd);
                    }
                }
            }
        }

        public static void SetCatagory(DElementList eleList, string catagoryName)
        {
            if (eleList == null) return;
            foreach (DElement ele in eleList)
            {
                if (ele.Catagory == null || ele.Catagory.Length < 1)
                {
                    ele.Catagory = catagoryName;
                    if (ele.VR == DVR.SQ)
                    {
                        foreach (DElementList subEleList in ele.Sequence)
                        {
                            SetCatagory(subEleList, catagoryName);
                        }
                    }
                }
            }
        }

        public static void ModifyQCMappingList_FixValue<TC>(XCollection<TC> qcList)
            where TC : QueryCriteriaItem, IDicomMappingItem, new()
        {
            if (qcList == null) return;
            foreach (TC qc in qcList)
            {
                if (qc.Translating.Type == TranslatingType.FixValue)
                {
                    qc.Translating.ConstValue = GetSQLString<TC>(qc, qc.Translating.ConstValue);
                }
            }
        }

        public static void SetDataIDMapping<TR>(XCollection<TR> mappingList)
            where TR : QueryResultItem, new()
        {
            if (mappingList == null) return;
            TR item = new TR();
            item.GWDataDBField = GWDataDBField.i_IndexGuid;
            item.TargetField = DATAID;
            mappingList.Add(item);
        }

        public class Tags
        {
            public const int ScheduledProcedureStepSequence = 0x00400100;
            public const int ScheduledProcedureStepStartDate = 0x00400002;
            public const int ScheduledProcedureStepStartTime = 0x00400003;
        }

    }

    public class DElementListWrapper
    {
        public DElementListWrapper(DicomDataset list)
        {
            List = list;
        }
        public readonly List<string> GUIDs = new List<string>();
        public readonly DicomDataset List;
    }
}
