using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Objects.Rule;

namespace HYS.SQLInboundAdapterConfiguration
{
    public class GWFields
    {
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
        public static object[] GetGWDataField_Left(GWDataDBTable table, GWDataDBField[] Fields)
        {

            List<object> strList = new List<object>();

            GWDataDBField[] allFields = GWDataDBField.GetFields(table, HYS.Common.Objects.Device.DirectionType.INBOUND);

            bool bExist;
            foreach (GWDataDBField af in allFields)
            {
                bExist = false;
                foreach (GWDataDBField f in Fields)
                {
                    if (af.GetFullFieldName() == f.GetFullFieldName())
                    {
                        bExist = true;
                        break;
                    }
                }
                if (!bExist)
                    strList.Add(af.FieldName);
            }

            return strList.ToArray();
        }


        //public static object[] GWDataIndexField = new object[] {
        //    "DATA_ID",
        //    "DATA_DT",
        //    "EVENT_TYPE",
        //    "RECORD_INDEX_1",
        //    "RECORD_INDEX_2",
        //    "RECORD_INDEX_3",
        //    "RECORD_INDEX_4",
        //    "DATA_SOURCE",
        //    "PROCESS_FLAG"         
        //};

        //public static object[] GWPatientField = new object[] {
        //    "DATA_ID",
        //    "DATA_DT",
        //    "PATIENT_ID",
        //    "PATIENT_OLDID",
        //    "OTHER_PID",
        //    "PATIENT_NAME",
        //    "MOTHER_MAIDEN_AME",
        //    "BIRTHDATE",
        //    "SEX",
        //    "PATIENT_ALIAS",
        //    "RACE",
        //    "ADDRESS",
        //    "COUNTRY_CODE",
        //    "PHONENUMBER_HOME",
        //    "PHONENUMBER_BUSINESS",
        //    "PRIMARY_LANGUAGE",
        //    "MARTIAL_ATATUS",
        //    "RELIGION",
        //    "ACCOUNT_NUMBER",
        //    "SSN_NUMBER",
        //    "DRIVERLIC_NUMBER",
        //    "ETHNIC_GROUP",
        //    "BIRTH_PLACE",
        //    "CITIZENSHIP",
        //    "VETERANS_MIL_STATUS",
        //    "NATIONALITY",
        //    "PATIENT_TYPE",
        //    "PATIENT_LOCATION",
        //    "PATIENT_STATUS",
        //    "VISIT_NUMBER",
        //    "BED_NUMBER",
        //};

        //public static object[] GWDataOrderField = new object[] {
        //    "DATA_ID",
        //    "DATA_DT",
        //    "ORDER_NO",
        //    "PLACER_NO",
        //    "FILLER_NO",
        //    "SERIES_NO",
        //    "PATIENT_ID",
        //    "EXAM_STATUS",
        //    "PLACER_DEPATMENT",
        //    "PLACER",
        //    "PLACER_CONTACT",
        //    "FILLER_DEPARTMENT",
        //    "FILLER",
        //    "FILLER_CONTACT",
        //    "REF_ORGANIZATION",
        //    "REF_PHYSICIAN",
        //    "REF_CONTACT",
        //    "REQUEST_RESON",
        //    "REUQEST_COMMENTS",
        //    "EXAM_REQUIREMENT",
        //    "SCHEDULED_DT",
        //    "MODALITY",
        //    "STATION_NAME",
        //    "STATION_AETITLE",
        //    "EXAM_LOCATIOM",
        //    "EXAM_VOLUME",
        //    "EXAM_DT",
        //    "DURATION",
        //    "TRANSPORT_ARRANGE",
        //    "TECHNICIAN",
        //    "BODY_PART",
        //    "PROCEDURE_NAME",
        //    "PROCEDURE_CODE",
        //    "PROCEDURE_DESC",
        //    "STUDY_INSTANCE_UID",
        //    "STUDY_ID",
        //    "REF_CLASS_UID",
        //    "EXAM_COMMENT",
        //    "CNT_AGENT",
        //    "CHARGE_STATUS",
        //    "CHARGR_AMOUNT"
        //};

        //public static object[] GWDataReportField = new object[] {
        //    "DATA_ID",
        //    "DATA_DT",
        //    "REPORT_NO",
        //    "ACCESSION_NUMBER",
        //    "PATIENT_ID",
        //    "REPORT_STATUS",
        //    "MODALITY",
        //    "REPORT_TYPE",
        //    "REPORT_FILE",
        //    "DIAGNOSE",
        //    "COMMENTS",
        //    "REPORT_WRTIER",
        //    "REPORT_INTEPRETER",
        //    "REPORT_APPROVER",
        //    "REPORTDT",
        //    "OBSERVATIONMETHOD"                              
        //};
    }
}
