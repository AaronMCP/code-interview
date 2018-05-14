using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Objects.Rule;
using HYS.Common.Xml;

namespace HYS.FileAdapter.FileInboundAdapterConfiguration
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
                    GWDataDBField[] flist = GWDataDBField.GetFields(GWDataDBTable.Index);
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
                GWDataDBField[] flist = GWDataDBField.GetFields(GWDataDBTable.Patient);
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
                GWDataDBField[] flist = GWDataDBField.GetFields(GWDataDBTable.Order);
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
                GWDataDBField[] flist = GWDataDBField.GetFields(GWDataDBTable.Report);
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


    }
}
