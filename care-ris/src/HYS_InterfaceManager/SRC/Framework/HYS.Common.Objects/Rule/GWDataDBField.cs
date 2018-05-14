using System;
using System.Reflection;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.Rule
{
    public class GWDataDBField : XObject
    {
        [XNode(false)]
        public readonly bool IsAuto = false;

        private GWDataDBTable _table = GWDataDBTable.None;
        public GWDataDBTable Table
        {
            get { return _table; }
            set { _table = value; }
        }

        private string _fieldName;
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        public string GetFullFieldName()
        {
            return GetFullFieldName("");
        }
        public string GetFullFieldName(string interfaceName)
        {
            if (Table == GWDataDBTable.None) return "[NULL]";
            return GetTableName(interfaceName) + "." + FieldName;
        }

        public string GetTableName()
        {
            return GetTableName("");
        }
        public string GetTableName(string interfaceName)
        {
            return GWDataDB.GetTableName(interfaceName, Table);
        }

        public GWDataDBField Clone()
        {
            GWDataDBField field = new GWDataDBField(FieldName, Table, IsAuto);
            return field;
        }
        public override string ToString()
        {
            return GetFullFieldName();
        }

        public GWDataDBField()
        {
        }
        public GWDataDBField(string field, GWDataDBTable table, bool isAuto)
        {
            IsAuto = isAuto;
            _fieldName = field;
            _table = table;
        }
        public GWDataDBField(string field, GWDataDBTable table)
            : this( field, table, false)
        {
        }

        #region static members

        private class Comparer : IComparer<string>
        {
            public static Comparer Instance = new Comparer();

            #region IComparer<string> Members

            public int Compare(string x, string y)
            {
                if (x == null) x = "";
                if (y == null) y = "";
                return x.CompareTo(y);
            }

            #endregion
        }
        
        private static GWDataDBField[] _indexFields;
        private static GWDataDBField[] _indexFieldsO;
        private static GWDataDBField[] _indexFieldsI;
        private static GWDataDBField[] _orderFields;
        private static GWDataDBField[] _orderFieldsO;
        private static GWDataDBField[] _orderFieldsI;
        private static GWDataDBField[] _reportFields;
        private static GWDataDBField[] _reportFieldsO;
        private static GWDataDBField[] _reportFieldsI;
        private static GWDataDBField[] _patientFields;
        private static GWDataDBField[] _patientFieldsO;
        private static GWDataDBField[] _patientFieldsI;
        private static GWDataDBField[] _GetFields(GWDataDBTable table, DirectionType direction)
        {
            Type t = Null.GetType();
            SortedList<string, GWDataDBField> fieldList = new SortedList<string, GWDataDBField>(Comparer.Instance);
            FieldInfo[] flist = t.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo f in flist)
            {
                object o = t.InvokeMember(f.Name,
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField,
                    null, Null, new object[] { });

                GWDataDBField field = o as GWDataDBField;
                if (field != null && field.Table == table)
                {
                    if (direction == DirectionType.INBOUND && field.IsAuto) continue;
                    fieldList.Add(field.FieldName, field);
                }
            }
            List<GWDataDBField> fList = new List<GWDataDBField>();
            foreach (GWDataDBField f in fieldList.Values) fList.Add(f);
            return fList.ToArray();
        }

        private static GWDataDBField[] GetFieldsAll(GWDataDBTable table)
        {
            switch (table)
            {
                case GWDataDBTable.Index:
                    {
                        if (_indexFields == null) _indexFields = _GetFields(table, DirectionType.UNKNOWN);
                        return _indexFields;
                    }
                case GWDataDBTable.Patient:
                    {
                        if (_patientFields == null) _patientFields = _GetFields(table, DirectionType.UNKNOWN);
                        return _patientFields;
                    }
                case GWDataDBTable.Order:
                    {
                        if (_orderFields == null) _orderFields = _GetFields(table, DirectionType.UNKNOWN);
                        return _orderFields;
                    }
                case GWDataDBTable.Report:
                    {
                        if (_reportFields == null) _reportFields = _GetFields(table, DirectionType.UNKNOWN);
                        return _reportFields;
                    }
                default: return null;
            }
        }
        private static GWDataDBField[] GetFieldsInbound(GWDataDBTable table)
        {
            switch (table)
            {
                case GWDataDBTable.Index:
                    {
                        if (_indexFieldsI == null) _indexFieldsI = _GetFields(table, DirectionType.INBOUND);
                        return _indexFieldsI;
                    }
                case GWDataDBTable.Patient:
                    {
                        if (_patientFieldsI == null) _patientFieldsI = _GetFields(table, DirectionType.INBOUND);
                        return _patientFieldsI;
                    }
                case GWDataDBTable.Order:
                    {
                        if (_orderFieldsI == null) _orderFieldsI = _GetFields(table, DirectionType.INBOUND);
                        return _orderFieldsI;
                    }
                case GWDataDBTable.Report:
                    {
                        if (_reportFieldsI == null) _reportFieldsI = _GetFields(table, DirectionType.INBOUND);
                        return _reportFieldsI;
                    }
                default: return null;
            }
        }
        private static GWDataDBField[] GetFieldsOutbound(GWDataDBTable table)
        {
            switch (table)
            {
                case GWDataDBTable.Index:
                    {
                        if (_indexFieldsO == null) _indexFieldsO = _GetFields(table, DirectionType.OUTBOUND);
                        return _indexFieldsO;
                    }
                case GWDataDBTable.Patient:
                    {
                        if (_patientFieldsO == null) _patientFieldsO = _GetFields(table, DirectionType.OUTBOUND);
                        return _patientFieldsO;
                    }
                case GWDataDBTable.Order:
                    {
                        if (_orderFieldsO == null) _orderFieldsO = _GetFields(table, DirectionType.OUTBOUND);
                        return _orderFieldsO;
                    }
                case GWDataDBTable.Report:
                    {
                        if (_reportFieldsO == null) _reportFieldsO = _GetFields(table, DirectionType.OUTBOUND);
                        return _reportFieldsO;
                    }
                default: return null;
            }
        }

        public static GWDataDBField[] GetFields(GWDataDBTable table)
        {
            return GetFieldsAll(table);
        }
        public static GWDataDBField[] GetFields(GWDataDBTable table, DirectionType direction)
        {
            switch (direction)
            {
                case DirectionType.INBOUND: return GetFieldsInbound(table);
                case DirectionType.OUTBOUND: return GetFieldsOutbound(table);
                default: return GetFieldsAll(table);
            }
        }
        internal static GWDataDBField[] GetFieldsWithoutSort(GWDataDBTable table)
        {
            Type t = Null.GetType();
            List<GWDataDBField> fieldList = new List<GWDataDBField>();
            FieldInfo[] flist = t.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo f in flist)
            {
                object o = t.InvokeMember(f.Name,
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField,
                    null, Null, new object[] { });

                GWDataDBField field = o as GWDataDBField;
                if (field != null && field.Table == table)
                {
                    fieldList.Add(field);
                }
            }
            return fieldList.ToArray();
        }

        [Obsolete("This method is expired, please use GWDataDB.GetTableName() instead.", false)]
        public static string GetTableName(string interfaceName, GWDataDBTable table)
        {
            switch (table)
            {
                case GWDataDBTable.Index: return interfaceName + "_DataIndex";
                case GWDataDBTable.Patient: return interfaceName + "_PATIENT";
                case GWDataDBTable.Order: return interfaceName + "_ORDER";
                case GWDataDBTable.Report: return interfaceName + "_REPORT";
                default: return "";
            }
        }

        #endregion

        #region fields definition

        public static GWDataDBField i_IndexGuid = new GWDataDBField("DATA_ID", GWDataDBTable.Index, true);
        public static GWDataDBField i_DataDateTime = new GWDataDBField("DATA_DT", GWDataDBTable.Index, true);
        public static GWDataDBField i_EventType = new GWDataDBField("EVENT_TYPE", GWDataDBTable.Index);
        public static GWDataDBField i_RecordIndex1 = new GWDataDBField("RECORD_INDEX_1", GWDataDBTable.Index);
        public static GWDataDBField i_RecordIndex2 = new GWDataDBField("RECORD_INDEX_2", GWDataDBTable.Index);
        public static GWDataDBField i_RecordIndex3 = new GWDataDBField("RECORD_INDEX_3", GWDataDBTable.Index);
        public static GWDataDBField i_RecordIndex4 = new GWDataDBField("RECORD_INDEX_4", GWDataDBTable.Index);
        public static GWDataDBField i_DATA_SOURCE = new GWDataDBField("DATA_SOURCE", GWDataDBTable.Index);
        public static GWDataDBField i_PROCESS_FLAG = new GWDataDBField("PROCESS_FLAG", GWDataDBTable.Index);

        public static GWDataDBField p_DATA_ID = new GWDataDBField("DATA_ID", GWDataDBTable.Patient, true);
        public static GWDataDBField p_DATA_DT = new GWDataDBField("DATA_DT", GWDataDBTable.Patient, true);
        public static GWDataDBField p_PatientID = new GWDataDBField("PATIENTID", GWDataDBTable.Patient);
        public static GWDataDBField p_PatientName = new GWDataDBField("PATIENT_NAME", GWDataDBTable.Patient);
        public static GWDataDBField p_PatientLocalName = new GWDataDBField("PATIENT_LOCAL_NAME", GWDataDBTable.Patient);
        public static GWDataDBField p_PriorPatientID = new GWDataDBField("PRIOR_PATIENT_ID", GWDataDBTable.Patient);
        public static GWDataDBField p_PriorPatientName = new GWDataDBField("PRIOR_PATIENT_NAME", GWDataDBTable.Patient);
        //public static GWDataDBField p_PATIENT_OLDID = new GWDataDBField("PATIENT_OLDID", GWDataDBTable.Patient);
        public static GWDataDBField p_OTHER_PID = new GWDataDBField("OTHER_PID", GWDataDBTable.Patient);
        public static GWDataDBField p_MOTHER_MAIDEN_NAME = new GWDataDBField("MOTHER_MAIDEN_NAME", GWDataDBTable.Patient);
        public static GWDataDBField p_BIRTHDATE = new GWDataDBField("BIRTHDATE", GWDataDBTable.Patient);
        public static GWDataDBField p_SEX = new GWDataDBField("SEX", GWDataDBTable.Patient);
        public static GWDataDBField p_PATIENT_ALIAS = new GWDataDBField("PATIENT_ALIAS", GWDataDBTable.Patient);
        public static GWDataDBField p_RACE = new GWDataDBField("RACE", GWDataDBTable.Patient);
        public static GWDataDBField p_ADDRESS = new GWDataDBField("ADDRESS", GWDataDBTable.Patient);
        public static GWDataDBField p_COUNTRY_CODE = new GWDataDBField("COUNTRY_CODE", GWDataDBTable.Patient);
        public static GWDataDBField p_PHONENUMBER_HOME = new GWDataDBField("PHONENUMBER_HOME", GWDataDBTable.Patient);
        public static GWDataDBField p_PHONENUMBER_BUSINESS = new GWDataDBField("PHONENUMBER_BUSINESS", GWDataDBTable.Patient);
        public static GWDataDBField p_PRIMARY_LANGUAGE = new GWDataDBField("PRIMARY_LANGUAGE", GWDataDBTable.Patient);
        public static GWDataDBField p_MARITAL_STATUS = new GWDataDBField("MARITAL_STATUS", GWDataDBTable.Patient);
        public static GWDataDBField p_RELIGION = new GWDataDBField("RELIGION", GWDataDBTable.Patient);
        public static GWDataDBField p_ACCOUNT_NUMBER = new GWDataDBField("ACCOUNT_NUMBER", GWDataDBTable.Patient);
        public static GWDataDBField p_SSN_NUMBER = new GWDataDBField("SSN_NUMBER", GWDataDBTable.Patient);
        public static GWDataDBField p_DRIVERLIC_NUMBER = new GWDataDBField("DRIVERLIC_NUMBER", GWDataDBTable.Patient);
        public static GWDataDBField p_ETHNIC_GROUP = new GWDataDBField("ETHNIC_GROUP", GWDataDBTable.Patient);
        public static GWDataDBField p_BIRTH_PLACE = new GWDataDBField("BIRTH_PLACE", GWDataDBTable.Patient);
        public static GWDataDBField p_CITIZENSHIP = new GWDataDBField("CITIZENSHIP", GWDataDBTable.Patient);
        public static GWDataDBField p_VETERANS_MIL_STATUS = new GWDataDBField("VETERANS_MIL_STATUS", GWDataDBTable.Patient);
        public static GWDataDBField p_NATIONALITY = new GWDataDBField("NATIONALITY", GWDataDBTable.Patient);
        public static GWDataDBField p_PATIENT_TYPE = new GWDataDBField("PATIENT_TYPE", GWDataDBTable.Patient);
        public static GWDataDBField p_PATIENT_LOCATION = new GWDataDBField("PATIENT_LOCATION", GWDataDBTable.Patient);
        public static GWDataDBField p_PATIENT_STATUS = new GWDataDBField("PATIENT_STATUS", GWDataDBTable.Patient);
        public static GWDataDBField p_VISIT_NUMBER = new GWDataDBField("VISIT_NUMBER", GWDataDBTable.Patient);
        public static GWDataDBField p_PRIOR_VISIT_NUMBER = new GWDataDBField("PRIOR_VISIT_NUMBER", GWDataDBTable.Patient);
        public static GWDataDBField p_BED_NUMBER = new GWDataDBField("BED_NUMBER", GWDataDBTable.Patient);
        public static GWDataDBField p_CUSTOMER_1 = new GWDataDBField("CUSTOMER_1", GWDataDBTable.Patient);
        public static GWDataDBField p_CUSTOMER_2 = new GWDataDBField("CUSTOMER_2", GWDataDBTable.Patient);
        public static GWDataDBField p_CUSTOMER_3 = new GWDataDBField("CUSTOMER_3", GWDataDBTable.Patient);
        public static GWDataDBField p_CUSTOMER_4 = new GWDataDBField("CUSTOMER_4", GWDataDBTable.Patient);

        public static GWDataDBField o_DATA_ID = new GWDataDBField("DATA_ID", GWDataDBTable.Order, true);
        public static GWDataDBField o_DATA_DT = new GWDataDBField("DATA_DT", GWDataDBTable.Order, true);
        public static GWDataDBField o_OrderNo = new GWDataDBField("ORDER_NO", GWDataDBTable.Order);
        public static GWDataDBField o_PLACER_NO = new GWDataDBField("PLACER_NO", GWDataDBTable.Order);
        public static GWDataDBField o_FILLER_NO = new GWDataDBField("FILLER_NO", GWDataDBTable.Order);
        public static GWDataDBField o_SERIES_NO = new GWDataDBField("SERIES_NO", GWDataDBTable.Order);
        public static GWDataDBField o_PATIENT_ID = new GWDataDBField("PATIENT_ID", GWDataDBTable.Order);
        public static GWDataDBField o_EXAM_STATUS = new GWDataDBField("EXAM_STATUS", GWDataDBTable.Order);
        public static GWDataDBField o_PLACER_DEPARTMENT = new GWDataDBField("PLACER_DEPARTMENT", GWDataDBTable.Order);
        public static GWDataDBField o_PLACER = new GWDataDBField("PLACER", GWDataDBTable.Order);
        public static GWDataDBField o_PLACER_CONTACT = new GWDataDBField("PLACER_CONTACT", GWDataDBTable.Order);
        public static GWDataDBField o_FILLER_DEPARTMENT = new GWDataDBField("FILLER_DEPARTMENT", GWDataDBTable.Order);
        public static GWDataDBField o_FILLER = new GWDataDBField("FILLER", GWDataDBTable.Order);
        public static GWDataDBField o_FILLER_CONTACT = new GWDataDBField("FILLER_CONTACT", GWDataDBTable.Order);
        public static GWDataDBField o_REF_ORGANIZATION = new GWDataDBField("REF_ORGANIZATION", GWDataDBTable.Order);
        public static GWDataDBField o_REF_PHYSICIAN = new GWDataDBField("REF_PHYSICIAN", GWDataDBTable.Order);
        public static GWDataDBField o_REF_CONTACT = new GWDataDBField("REF_CONTACT", GWDataDBTable.Order);
        public static GWDataDBField o_REQUEST_REASON = new GWDataDBField("REQUEST_REASON", GWDataDBTable.Order);
        public static GWDataDBField o_REUQEST_COMMENTS = new GWDataDBField("REUQEST_COMMENTS", GWDataDBTable.Order);
        public static GWDataDBField o_EXAM_REQUIREMENT = new GWDataDBField("EXAM_REQUIREMENT", GWDataDBTable.Order);
        public static GWDataDBField o_SCHEDULED_DT = new GWDataDBField("SCHEDULED_DT", GWDataDBTable.Order);
        public static GWDataDBField o_MODALITY = new GWDataDBField("MODALITY", GWDataDBTable.Order);
        public static GWDataDBField o_STATION_NAME = new GWDataDBField("STATION_NAME", GWDataDBTable.Order);
        public static GWDataDBField o_STATION_AETITLE = new GWDataDBField("STATION_AETITLE", GWDataDBTable.Order);
        public static GWDataDBField o_EXAM_LOCATION = new GWDataDBField("EXAM_LOCATION", GWDataDBTable.Order);
        public static GWDataDBField o_EXAM_VOLUME = new GWDataDBField("EXAM_VOLUME", GWDataDBTable.Order);
        public static GWDataDBField o_EXAM_DT = new GWDataDBField("EXAM_DT", GWDataDBTable.Order);
        public static GWDataDBField o_DURATION = new GWDataDBField("DURATION", GWDataDBTable.Order);
        public static GWDataDBField o_TRANSPORT_ARRANGE = new GWDataDBField("TRANSPORT_ARRANGE", GWDataDBTable.Order);
        public static GWDataDBField o_TECHNICIAN = new GWDataDBField("TECHNICIAN", GWDataDBTable.Order);
        public static GWDataDBField o_BODY_PART = new GWDataDBField("BODY_PART", GWDataDBTable.Order);
        public static GWDataDBField o_PROCEDURE_NAME = new GWDataDBField("PROCEDURE_NAME", GWDataDBTable.Order);
        public static GWDataDBField o_PROCEDURE_CODE = new GWDataDBField("PROCEDURE_CODE", GWDataDBTable.Order);
        public static GWDataDBField o_PROCEDURE_DESC = new GWDataDBField("PROCEDURE_DESC", GWDataDBTable.Order);
        public static GWDataDBField o_STUDY_INSTANCE_UID = new GWDataDBField("STUDY_INSTANCE_UID", GWDataDBTable.Order);
        public static GWDataDBField o_STUDY_ID = new GWDataDBField("STUDY_ID", GWDataDBTable.Order);
        public static GWDataDBField o_REF_CLASS_UID = new GWDataDBField("REF_CLASS_UID", GWDataDBTable.Order);
        public static GWDataDBField o_EXAM_COMMENT = new GWDataDBField("EXAM_COMMENT", GWDataDBTable.Order);
        public static GWDataDBField o_CNT_AGENT = new GWDataDBField("CNT_AGENT", GWDataDBTable.Order);
        public static GWDataDBField o_CHARGE_STATUS = new GWDataDBField("CHARGE_STATUS", GWDataDBTable.Order);
        public static GWDataDBField o_CHARGE_AMOUNT = new GWDataDBField("CHARGE_AMOUNT", GWDataDBTable.Order);
        public static GWDataDBField o_CUSTOMER_1 = new GWDataDBField("CUSTOMER_1", GWDataDBTable.Order);
        public static GWDataDBField o_CUSTOMER_2 = new GWDataDBField("CUSTOMER_2", GWDataDBTable.Order);
        public static GWDataDBField o_CUSTOMER_3 = new GWDataDBField("CUSTOMER_3", GWDataDBTable.Order);
        public static GWDataDBField o_CUSTOMER_4 = new GWDataDBField("CUSTOMER_4", GWDataDBTable.Order);

        public static GWDataDBField r_DATA_ID = new GWDataDBField("DATA_ID", GWDataDBTable.Report, true);
        public static GWDataDBField r_DATA_DT = new GWDataDBField("DATA_DT", GWDataDBTable.Report, true);
        public static GWDataDBField r_REPORT_NO = new GWDataDBField("REPORT_NO", GWDataDBTable.Report);
        public static GWDataDBField r_ACCESSION_NUMBER = new GWDataDBField("ACCESSION_NUMBER", GWDataDBTable.Report);
        public static GWDataDBField r_PATIENT_ID = new GWDataDBField("PATIENT_ID", GWDataDBTable.Report);
        public static GWDataDBField r_REPORT_STATUS = new GWDataDBField("REPORT_STATUS", GWDataDBTable.Report);
        public static GWDataDBField r_MODALITY = new GWDataDBField("MODALITY", GWDataDBTable.Report);
        public static GWDataDBField r_REPORT_TYPE = new GWDataDBField("REPORT_TYPE", GWDataDBTable.Report);
        public static GWDataDBField r_REPORT_FILE = new GWDataDBField("REPORT_FILE", GWDataDBTable.Report);
        public static GWDataDBField r_DIAGNOSE = new GWDataDBField("DIAGNOSE", GWDataDBTable.Report);
        public static GWDataDBField r_COMMENTS = new GWDataDBField("COMMENTS", GWDataDBTable.Report);
        public static GWDataDBField r_REPORT_WRITER = new GWDataDBField("REPORT_WRITER", GWDataDBTable.Report);
        public static GWDataDBField r_REPORT_INTEPRETER = new GWDataDBField("REPORT_INTEPRETER", GWDataDBTable.Report);
        public static GWDataDBField r_REPORT_APPROVER = new GWDataDBField("REPORT_APPROVER", GWDataDBTable.Report);
        public static GWDataDBField r_REPORTDT = new GWDataDBField("REPORTDT", GWDataDBTable.Report);
        public static GWDataDBField r_OBSERVATIONMETHOD = new GWDataDBField("OBSERVATIONMETHOD", GWDataDBTable.Report);
        public static GWDataDBField r_CUSTOMER_1 = new GWDataDBField("CUSTOMER_1", GWDataDBTable.Report);
        public static GWDataDBField r_CUSTOMER_2 = new GWDataDBField("CUSTOMER_2", GWDataDBTable.Report);
        public static GWDataDBField r_CUSTOMER_3 = new GWDataDBField("CUSTOMER_3", GWDataDBTable.Report);
        public static GWDataDBField r_CUSTOMER_4 = new GWDataDBField("CUSTOMER_4", GWDataDBTable.Report);

        public static GWDataDBField Null
        {
            get
            {
                return new GWDataDBField();
            }
        }

        #endregion
    }
}
