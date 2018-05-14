using System;
using System.Reflection;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.XmlAdapter.Common.Objects
{
    public class HL7EventType : XObject
    {
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _qualifier = "";
        public string Qualifier
        {
            get { return _qualifier; }
            set { _qualifier = value; }
        }

        private string _description = "";
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public HL7EventType()
        {
        }
        private HL7EventType(string name, string description)
        {
            _name = name;
            _description = description;
        }
        private HL7EventType(string name, string qualifier, string description)
        {
            _name = name;
            _qualifier = qualifier;
            _description = description;
        }

        public HL7EventType Clone()
        {
            HL7EventType t = new HL7EventType();
            t.Description = Description;
            t.Qualifier = Qualifier;
            t.Name = Name;
            return t;
        }

        public string GetKey()
        {
            if (Qualifier == null || Qualifier.Length < 1) return Name;
            return Name + "(" + Qualifier + ")";
        }

        public override string ToString()
        {
            return GetKey() + " : " + Description;
        }

        public static List<HL7EventType> GetEventTypes()
        {
            Type t = typeof(HL7EventType);
            List<HL7EventType> list = new List<HL7EventType>();
            FieldInfo[] fList = t.GetFields(BindingFlags.Public | BindingFlags.Static);
            if (fList != null)
            {
                foreach (FieldInfo f in fList)
                {
                    HL7EventType ele = t.InvokeMember(f.Name, BindingFlags.Public | BindingFlags.Static | BindingFlags.GetField,
                        null, null, new object[] { }) as HL7EventType;
                    if (ele != null)
                    {
                        list.Add(ele);
                    }
                }
            }
            return list;
        }

        public static HL7EventType Empty = new HL7EventType("EMPTY", "EMPTY");

        public static HL7EventType A01 = new HL7EventType("NOTIFICATION", "PATIENT_ADMIT_VISIT", "ADT A01");
        public static HL7EventType A02 = new HL7EventType("PATIENT_TRANSFER", "ADT A02");
        public static HL7EventType A03 = new HL7EventType("PATIENT_DISCHARGE", "ADT A03");
        public static HL7EventType A04 = new HL7EventType("PATIENT_REGISTER", "ADT A04");
        public static HL7EventType A05 = new HL7EventType("PATIENT_PREADMIT", "ADT A05");
        public static HL7EventType A06 = new HL7EventType("PATIENT_STATUS_UPDATE", "OUT_TO_IN", "ADT A06");
        public static HL7EventType A07 = new HL7EventType("PATIENT_STATUS_UPDATE", "IN_TO_OUT", "ADT A07");
        public static HL7EventType A08 = new HL7EventType("PATIENT_UPDATE", "ADT A08");
        public static HL7EventType A09 = new HL7EventType("NOTIFICATION", "PATIENT_DEPARTING", "ADT A09");
        public static HL7EventType A10 = new HL7EventType("NOTIFICATION", "PATIENT_ARRIVING", "ADT A10");
        public static HL7EventType A11 = new HL7EventType("NOTIFICATION", "PATIENT_ADMIT_VISIT_CANCELLED", "ADT A11");
        public static HL7EventType A12 = new HL7EventType("PATIENT_TRANSFER_CANCELLED", "ADT A12");
        public static HL7EventType A13 = new HL7EventType("PATIENT_DISCHARGE_CANCELLED", "ADT A13");
        public static HL7EventType A18 = new HL7EventType("PATIENT_MERGE", "ADT A18");
        public static HL7EventType A38 = new HL7EventType("PATIENT_PREADMIT_CANCELLED", "ADT A38");
        public static HL7EventType A40 = new HL7EventType("PATIENT_MERGE", "PID_LIST", "ADT A40");

        public static HL7EventType O01_NW = new HL7EventType("PROCEDURE_CREATE", "ORM O01 (NW 每 new order)");
        public static HL7EventType O01_CA = new HL7EventType("PROCEDURE_RESCIND", "CANCEL", "ORM O01 (CA 每 cancel order)");
        public static HL7EventType O01_DC = new HL7EventType("PROCEDURE_RESCIND", "DISCONTINUE", "ORM O01 (DC 每 discontinue order)");
        public static HL7EventType O01_XO = new HL7EventType("PROCEDURE_UPDATE", "MUST_EXIST", "ORM O01 (XO 每 change order)");

        public static HL7EventType R01 = new HL7EventType("OBSERVATION", "ORU R01");
    }
}
