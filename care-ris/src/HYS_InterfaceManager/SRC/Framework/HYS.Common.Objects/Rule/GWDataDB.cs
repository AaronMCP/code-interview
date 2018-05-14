using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Rule
{
    public class GWDataDB
    {
        public const string TimeFormat = "HH:mm:ss";
        public const string DateFormat = "yyyy-MM-dd";
        public const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        //public const string TimeFormat = "hhmmss";
        //public const string DateFormat = "yyyyMMdd";
        //public const string DateTimeFormat = "yyyyMMddhhmmss";

        //public static string DateTime2GWTimeString(DateTime dt)
        //{
        //    return dt.ToString(TimeFormat);
        //}
        //public static string DateTime2GWDateString(DateTime dt)
        //{
        //    return dt.ToString(DateFormat);
        //}
        //public static string DateTime2GWDateTimeString(DateTime dt)
        //{
        //    return dt.ToString(DateTimeFormat);
        //}
        //public static DateTime GWTimeString2DateTime(string str)
        //{
        //    return DateTime.ParseExact(str, TimeFormat, null);
        //}
        //public static DateTime GWDateString2DateTime(string str)
        //{
        //    return DateTime.ParseExact(str, DateFormat, null);
        //}
        //public static DateTime GWDateTimeString2DateTime(string str)
        //{
        //    return DateTime.ParseExact(str, DateTimeFormat, null);
        //}
        //public static bool GWDTString2DateTime(string str, ref DateTime dt)
        //{
        //    bool res = false;
        //    if (str != null)
        //    {
        //        if (str.Length == DateTimeFormat.Length)
        //        {
        //            dt = GWDateTimeString2DateTime(str);
        //            res = true;
        //        }
        //        else if (str.Length == DateFormat.Length)
        //        {
        //            dt = GWDateString2DateTime(str);
        //            res = true;
        //        }
        //        else if (str.Length == TimeFormat.Length)
        //        {
        //            dt = GWTimeString2DateTime(str);
        //            res = true;
        //        }
        //    }
        //    return res;
        //}

        public const string DataBaseName = "GWDataDB";
        public static string GetUseDataBaseSql()
        {
            return "USE " + DataBaseName;
        }

        public static string[] GetTableNames()
        {
            return Enum.GetNames(typeof(GWDataDBTable));
        }
        public static GWDataDBTable GetTable(string name)
        {
            if (name == null || name.Length < 1) return GWDataDBTable.None;
            return (GWDataDBTable)Enum.Parse(typeof(GWDataDBTable), name);
        }

        public static string GetTableName(GWDataDBTable table)
        {
            return GetTableName("", table);
        }
        public static string GetTableName(string interfaceName, GWDataDBTable table)
        {
            if (interfaceName != null && interfaceName.Length > 0)
                interfaceName += "_";

            switch (table)
            {
                case GWDataDBTable.Index: return interfaceName + "DATAINDEX";
                case GWDataDBTable.Patient: return interfaceName + "PATIENT";
                case GWDataDBTable.Order: return interfaceName + "ORDER";
                case GWDataDBTable.Report: return interfaceName + "REPORT";
                default: return "";
            }
        }

        public static string GetSPName(string interfaceName, IRule rule)
        {
            return "sp_" + interfaceName + "_" + rule.RuleID;
        }
        public static string GetFunctionName(string interfaceName, IRule rule, string fieldName)
        {
            return "fn_" + GetSPName(interfaceName, rule) + "_" + fieldName;
        }

        public static string GetPrivateLutName()
        {
            return "lut_p_" + RuleControl.GetRandomNumber();
        }
    }
}
