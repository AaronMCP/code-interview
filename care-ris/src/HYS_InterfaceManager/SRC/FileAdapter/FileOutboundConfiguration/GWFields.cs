using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Objects.Rule;

namespace HYS.FileAdapter.FileOutboundAdapterConfiguration
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
    }
}
