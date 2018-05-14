using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Objects.Rule;

namespace HYS.Adapter.Monitor.Utility
{
    public class GWDBControl
    {
        #region override GetFullFieldName() menthod
        public static string GetFullFieldName(GWDataDBField field) {
            return GetFullFieldName("", field);
        }

        public static string GetFullFieldName(string interfaceName , GWDataDBField field) {
            if (field.Table == GWDataDBTable.None) return "[NULL]";
            return "[" + field.GetTableName(interfaceName) + "]" + "." + "[" + field.FieldName + "]";
        }
        #endregion

        #region Get Table Field List
        private static object[] _indexField;
        private static object[] _patientField;
        private static object[] _orderField;
        private static object[] _reportField;

        public static object[] GWDataIndexField
        {
            get
            {
                if (_indexField == null)
                {
                    List<object> strList = new List<object>();
                    GWDataDBField[] flist = GWDataDBField.GetFields(GWDataDBTable.Index, HYS.Common.Objects.Device.DirectionType.INBOUND);
                    foreach (GWDataDBField f in flist)
                    {
                        strList.Add(f.FieldName);
                    }
                    _indexField = strList.ToArray();
                }
                return _indexField;
            }
        }

        public static object[] GWPatientField
        {
            get
            {
                List<object> strList = new List<object>();
                GWDataDBField[] flist = GWDataDBField.GetFields(GWDataDBTable.Patient, HYS.Common.Objects.Device.DirectionType.INBOUND);
                foreach (GWDataDBField f in flist)
                {
                    strList.Add(f.FieldName);
                }
                _patientField = strList.ToArray();
                return _patientField;
            }
        }

        public static object[] GWDataOrderField
        {
            get
            {
                List<object> strList = new List<object>();
                GWDataDBField[] flist = GWDataDBField.GetFields(GWDataDBTable.Order, HYS.Common.Objects.Device.DirectionType.INBOUND);
                foreach (GWDataDBField f in flist)
                {
                    strList.Add(f.FieldName);
                }
                _orderField = strList.ToArray();
                return _orderField;
            }
        }

        public static object[] GWDataReportField
        {
            get
            {
                List<object> strList = new List<object>();
                GWDataDBField[] flist = GWDataDBField.GetFields(GWDataDBTable.Report, HYS.Common.Objects.Device.DirectionType.INBOUND);
                foreach (GWDataDBField f in flist)
                {
                    strList.Add(f.FieldName);
                }
                _reportField = strList.ToArray();
                return _reportField;
            }
        }
        #endregion

        #region Get UnNull Field List
        private static object[] _indexUnNullField;
        private static object[] _patientUnNullField;
        private static object[] _orderUnNullField;
        private static object[] _reportUnNullField;

        public static object[] DataIndexUnNullField
        {
            get
            {              
                List<object> strList = new List<object>();                  
                strList.Add(GWDataDBField.i_EventType.FieldName);                                
                //strList.Add(GWDataDBField.i_PROCESS_FLAG.FieldName);                   
                
                _indexUnNullField = strList.ToArray();
                return _indexUnNullField;
            }
        }

        public static object[] PatientUnNullField
        {
            get
            {
                List<object> strList = new List<object>();
                //strList.Add(GWDataDBField.p_PatientID.FieldName);
                //strList.Add(GWDataDBField.p_PatientName.FieldName);
                //strList.Add(GWDataDBField.p_BIRTHDATE.FieldName);
                //strList.Add(GWDataDBField.p_SEX.FieldName);
                //strList.Add(GWDataDBField.p_PATIENT_TYPE.FieldName);

                _patientUnNullField = strList.ToArray();
                return _patientUnNullField;
            }
        }

        public static object[] OrderUnNullField
        {
            get
            {
                List<object> strList = new List<object>();
                //strList.Add(GWDataDBField.o_OrderNo.FieldName);
                //strList.Add(GWDataDBField.o_FILLER_NO.FieldName);
                //strList.Add(GWDataDBField.o_PATIENT_ID.FieldName);
                //strList.Add(GWDataDBField.o_EXAM_STATUS.FieldName);

                _orderUnNullField = strList.ToArray();
                return _orderUnNullField;
            }
        }

        public static object[] ReportUnNullField
        {
            get
            {
                List<object> strList = new List<object>();
                //strList.Add(GWDataDBField.r_REPORT_NO.FieldName);
                //strList.Add(GWDataDBField.r_ACCESSION_NUMBER.FieldName);
                //strList.Add(GWDataDBField.r_PATIENT_ID.FieldName);
                //strList.Add(GWDataDBField.r_REPORT_STATUS.FieldName);
                //strList.Add(GWDataDBField.r_REPORT_TYPE.FieldName);

                _reportUnNullField = strList.ToArray();
                return _reportUnNullField;
            }
        }

        #endregion
    }
}
