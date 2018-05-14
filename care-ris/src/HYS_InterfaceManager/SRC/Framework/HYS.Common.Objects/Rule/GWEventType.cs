using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Rule
{
    public class GWEventType : XObject
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

        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set { _enable = value; }
        }

        public GWEventType()
        {
        }
        public GWEventType(string code, string description, bool enable)
        {
            _code = code;
            _enable = enable;
            _description = description;
        }
        public GWEventType(string code, string description)
            : this(code, description, true)
        {
        }

        public GWEventType Clone()
        {
            GWEventType t = new GWEventType();
            t.Description = Description;
            t.Enable = Enable;
            t.Code = Code;
            return t;
        }

        public override string ToString()
        {
            return Description + _valueSeperator + Code + _valueSeperator + Enable + _objectSeperator;
        }

        private const char _valueSeperator = ',';
        private const char _objectSeperator = ';';
        public static GWEventType[] Parse(string strValue)
        {
            if (strValue == null) return null;
            List<GWEventType> list = new List<GWEventType>();
            string[] olist = strValue.Split(_objectSeperator);
            foreach (string o in olist)
            {
                string ostr = o.TrimStart().TrimEnd(_objectSeperator);
                if (ostr.Length < 1) continue;
                
                // parse event type
                GWEventType eventType = new GWEventType();
                string[] vlist = ostr.Split(_valueSeperator);
                if (vlist.Length > 0) eventType.Description = vlist[0];
                if (vlist.Length > 1) eventType.Code = vlist[1];
                if (vlist.Length > 2)
                {
                    try
                    {
                        eventType.Enable = bool.Parse(vlist[2].Trim());
                    }
                    catch
                    {
                        eventType.Enable = false;
                    }
                }

                // check event type
                if (eventType.Code.Trim().Length < 1) continue;
                foreach (GWEventType t in EventTypes)
                {
                    if (t.Code == eventType.Code)
                    {
                        eventType.Description = t.Description;
                        break;
                    }
                }

                list.Add(eventType);
            }
            return list.ToArray();
        }
        public static GWEventType ParseSingle(string strValue)
        {
            GWEventType[] list = Parse(strValue);
            if (list == null || list.Length < 1) return null;
            return list[0];
        }

        private static GWEventType[] GetEventTypes()
        {
            Type t = Empty.GetType();
            List<GWEventType> fieldList = new List<GWEventType>();
            FieldInfo[] flist = t.GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (FieldInfo f in flist)
            {
                object o = t.InvokeMember(f.Name,
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField,
                    null, Empty, new object[] { });

                GWEventType eventType = o as GWEventType;
                if (eventType != null) fieldList.Add(eventType);
            }
            return fieldList.ToArray();
        }
        private static GWEventType[] _eventTypes;
        [XNode(false)]
        public static GWEventType[] EventTypes
        {
            get
            {
                if (_eventTypes == null)
                {
                    _eventTypes = GetEventTypes();
                }
                return _eventTypes;
            }
        }

        public static GWEventType Empty                                       = new GWEventType("-1", "Empty", false);
        public static GWEventType InsertPatient                            = new GWEventType("00", "Insert Patient");
        public static GWEventType UpdatePatient                          = new GWEventType("01", "Update Patient");
        public static GWEventType MergePatient                            = new GWEventType("02", "Merge Patient");
        public static GWEventType DeletePatient                           = new GWEventType("03", "Delete Patient");
        public static GWEventType CreateOrder                             = new GWEventType("10", "Create Order");
        public static GWEventType UpdateOrder                            = new GWEventType("11", "Update Order");
        public static GWEventType OrderStatusChanged               = new GWEventType("12", "Order Status Changed");
        public static GWEventType DeleteOrder                             = new GWEventType("13", "Delete Order");
        public static GWEventType PACSImageArrival                    = new GWEventType("14", "PACS Image Arrival");
        public static GWEventType PACSQCNotification                  = new GWEventType("15", "PACS QC Notification");
        public static GWEventType NewAppointment                     = new GWEventType("20", "New Appointment");
        public static GWEventType RescheduleAppointment          = new GWEventType("21", "Reschedule Appointment");
        public static GWEventType AppointmentStatusChanged   = new GWEventType("22", "Appointment Status Changed");
        public static GWEventType DeleteAppointment                 = new GWEventType("23", "Delete Appointment");
        public static GWEventType CreateReport                          = new GWEventType("30", "Create Report");
        public static GWEventType UpdateReport                         = new GWEventType("31", "Update Report");
        public static GWEventType ReportStatusChanged            = new GWEventType("32", "Report Status Changed");
        public static GWEventType DeleteReport                          = new GWEventType("33", "Delete Report");
    }
}
