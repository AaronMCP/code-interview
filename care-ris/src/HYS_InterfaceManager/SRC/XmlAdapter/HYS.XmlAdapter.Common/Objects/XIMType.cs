using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.XmlAdapter.Common.Objects
{
    public enum XIMType
    {
        STR,    //
        INT,    //
        DEC,    //
        DAT,    //
        TIM,    //
        DTM,    //
        simple_code,
        COD,
        simple_content,
        CON,
        CP,
        DR,
        HD,
        FN,
        UID,
        MSR,    //
        PNM,    //
        ID,     //
        ADR,    //
        LOC,
        INS,
        PHN,
        PHY,
        complex_type,
    }

    public class XIMType_COD
    {
        public static XmlElement CODE = new XmlElement("CODE");
        public static XmlElement TEXT = new XmlElement("TEXT");
        public static XmlElement CODING_SCHEME = new XmlElement("CODING_SCHEME");
        public static XmlElement CODING_SCHEME_VERSION = new XmlElement("CODING_SCHEME_VERSION");
    }

    public class XIMType_UID
    {
        public static XmlElement CLASS_UID = new XmlElement("CLASS_UID");
        public static XmlElement INSTANCE_UID = new XmlElement("INSTANCE_UID");
    }

    public class XIMType_LOC
    {
        public static XmlElement POINT_OF_CARE = new XmlElement("POINT_OF_CARE", XIMType.COD);
        public static XmlElement ROOM = new XmlElement("ROOM", XIMType.COD);
        public static XmlElement BED = new XmlElement("BED", XIMType.COD);
        public static XmlElement BUILDING = new XmlElement("BUILDING", XIMType.COD);
        public static XmlElement FLOOR = new XmlElement("FLOOR", XIMType.COD);
        public static XmlElement FACILITY = new XmlElement("FACILITY", XIMType.HD);
        public static XmlElement TYPE = new XmlElement("TYPE", XIMType.COD);
        public static XmlElement DESCRIPTION = new XmlElement("DESCRIPTION", XIMType.STR);
        public static XmlElement STATUS = new XmlElement("STATUS", XIMType.COD);
        public static XmlElement COMPREHENSIVE_LOCATION_ID = new XmlElement("COMPREHENSIVE_LOCATION_ID", XIMType.HD);
        public static XmlElement ASSIGNING_AUTHORITY = new XmlElement("ASSIGNING_AUTHORITY", XIMType.HD);
    }

    public class XIMType_HD
    {
        public static XmlElement ENTITY_ID = new XmlElement("ENTITY_ID", XIMType.STR);
        public static XmlElement NAMESPACE_ID = new XmlElement("NAMESPACE_ID", XIMType.COD);
        public static XmlElement UNIVERSAL_ID = new XmlElement("UNIVERSAL_ID", XIMType.STR);
        public static XmlElement UNIVERSAL_ID_TYPE = new XmlElement("UNIVERSAL_ID_TYPE", XIMType.COD);
    }
    public class XIMType_DR
    {
        public static XmlElement START_DATE_TIME = new XmlElement("START_DATE_TIME", XIMType.DTM);
        public static XmlElement END_DATE_TIME = new XmlElement("END_DATE_TIME", XIMType.DTM);
    }
}
