using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class GWExamStatus : XObject
    {
        private string _code = "";
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public GWExamStatus()
        {
        }
        public GWExamStatus(string code, string description)
        {
            _code = code;
            _description = description;
        }

        public override string ToString()
        {
            return Description + _valueSeperator + Code + _objectSeperator;
        }

        private const char _valueSeperator = ',';
        private const char _objectSeperator = ';';
        public static GWExamStatus[] Parse(string strValue)
        {
            if (strValue == null) return null;
            List<GWExamStatus> list = new List<GWExamStatus>();
            string[] olist = strValue.Split(_objectSeperator);
            foreach (string o in olist)
            {
                string ostr = o.TrimStart().TrimEnd(_objectSeperator);
                if (ostr.Length < 1) continue;

                // parse exam status
                GWExamStatus examStatus = new GWExamStatus();
                string[] vlist = ostr.Split(_valueSeperator);
                if (vlist.Length > 0) examStatus.Description = vlist[0];
                if (vlist.Length > 1) examStatus.Code = vlist[1];

                // check exam status
                if (examStatus.Code.Trim().Length < 1) continue;
                foreach (GWExamStatus s in ExamStatus)
                {
                    if (s.Code == examStatus.Code)
                    {
                        examStatus.Description = s.Description;
                        break;
                    }
                }

                list.Add(examStatus);
            }
            return list.ToArray();
        }
        public static GWExamStatus ParseSingle(string strValue)
        {
            GWExamStatus[] list = Parse(strValue);
            if (list == null || list.Length < 1) return null;
            return list[0];
        }

        private static GWExamStatus[] GetExamStatus()
        {
            Type t = Empty.GetType();
            List<GWExamStatus> fieldList = new List<GWExamStatus>();
            FieldInfo[] flist = t.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo f in flist)
            {
                object o = t.InvokeMember(f.Name,
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField,
                    null, Empty, new object[] { });

                GWExamStatus examStatus = o as GWExamStatus;
                if (examStatus != null) fieldList.Add(examStatus);
            }
            return fieldList.ToArray();
        }
        private static GWExamStatus[] _examStatus;
        [XNode(false)]
        public static GWExamStatus[] ExamStatus
        {
            get
            {
                if (_examStatus == null)
                {
                    _examStatus = GetExamStatus();
                }
                return _examStatus;
            }
        }

        public static GWExamStatus Empty                = new GWExamStatus("-1", "Empty");
        public static GWExamStatus NewOrder             = new GWExamStatus("10", "New Order");
        public static GWExamStatus UpdateOrder          = new GWExamStatus("11", "Update Order");
        public static GWExamStatus MergeOrder           = new GWExamStatus("12", "Merge Order");
        public static GWExamStatus CancelOrder          = new GWExamStatus("13", "Cancel Order");
        public static GWExamStatus OrderScheduled       = new GWExamStatus("14", "Order Scheduled");
        public static GWExamStatus OrderInProgress      = new GWExamStatus("15", "Order In Progress");
        public static GWExamStatus OrderCompleted       = new GWExamStatus("16", "Order Completed");
        public static GWExamStatus OrderRejected        = new GWExamStatus("17", "Order Rejected");
        public static GWExamStatus ImageArrivalAtPACS   = new GWExamStatus("18", "Image Arrival At PACS");
        public static GWExamStatus NewAppointment       = new GWExamStatus("100", "New Appointment");
        public static GWExamStatus UpdateAppointment    = new GWExamStatus("101", "Update Appointment");
        public static GWExamStatus MergeAppointment     = new GWExamStatus("102", "Merge Appointment");
        public static GWExamStatus CancelAppointment    = new GWExamStatus("103", "Cancel Appointment");
        public static GWExamStatus AppointmentConfirmed = new GWExamStatus("104", "Appointment Confirmed");
        public static GWExamStatus AppointmentRejected  = new GWExamStatus("105", "Appointment Rejected");
    }
}
