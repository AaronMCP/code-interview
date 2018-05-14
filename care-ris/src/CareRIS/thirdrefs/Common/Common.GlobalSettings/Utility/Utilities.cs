#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*     Author : Caron Zhao                                                  */
/****************************************************************************/
#endregion

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace CommonGlobalSettings
{
    /// <summary>
    /// Summary description for Utility
    /// </summary>
    public class Utilities
    {
        const char SEPARATOR_0 = '&';
        const char SEPARATOR_1 = '=';

        /// <summary>
        /// Gets the key value in the source string.
        /// </summary>
        /// <param name="key">The key used to find the value of the parameter.</param>
        /// <param name="source">A string that contains the key/value.</param>
        /// <returns>The value that corresponding to the key.</returns>
        /// <ramarks>A sample source may be like "param1=value1&param2=value2"</ramarks>
        public static string GetParameter(string key, string source)
        {
            if (source == null || source.Equals(""))
            {
                return "";
            }

            char[] separator = new char[1];
            separator[0] = '&';

            char[] subSeparator = new char[1];
            subSeparator[0] = '=';

            string[] tempResult = source.Split(separator);
            foreach (string subString in tempResult)
            {
                string[] resultPair = subString.Split(subSeparator);
                if (resultPair.Length != 2)
                {
                    return "";
                }

                if (resultPair[0].Equals(key, StringComparison.CurrentCultureIgnoreCase))
                {
                    return resultPair[1];
                }
            }

            return "";
        }

        public static string GetParameter(char[] separator, char[] subSeparator, string key, string source)
        {
            if (source == null || source.Equals(""))
            {
                return "";
            }


            string[] tempResult = source.Split(separator);
            foreach (string subString in tempResult)
            {
                string[] resultPair = subString.Split(subSeparator);
                if (resultPair.Length != 2)
                {
                    return "";
                }

                if (resultPair[0].Equals(key, StringComparison.CurrentCultureIgnoreCase))
                {
                    return resultPair[1];
                }
            }

            return "";
        }

        /// <summary>
        /// Gets the key values in the source string according to the separator.
        /// </summary>
        /// <param name="separator">The separator used to find the value of the parameters.</param>
        /// <param name="source">The source string.</param>
        /// <returns>The values that corresponding to the separator.</returns>
        public static string[] GetParameters(string separator, string source)
        {
            char[] separators = new char[1];
            CharEnumerator e = separator.GetEnumerator();
            if (e.MoveNext())
            {
                separators[0] = e.Current;
            }

            return source.Split(separators);
        }

        public static string Covert2String(Object obj)
        {
            return (obj == null || obj == DBNull.Value) ? string.Empty : Convert.ToString(obj);
        }

        public static bool ValidString(Object obj)
        {
            return (obj == null || obj == DBNull.Value || obj.ToString().Trim().Length == 0) ? false : true;
        }

        public static string escape(string src)
        {
            src = src.Replace("%", "%25");

            src = src.Replace("&", "%26");
            src = src.Replace("#", "%23");
            src = src.Replace(",", "%2C");
            src = src.Replace(";", "%3B");
            src = src.Replace("|", "%7C");
            src = src.Replace("=", "%3D");

            return src;
        }

        public static string unescape(string src)
        {
            src = src.Replace("%26", "&");
            src = src.Replace("%23", "#");
            src = src.Replace("%2C", ",");
            src = src.Replace("%3B", ";");
            src = src.Replace("%7C", "|");
            src = src.Replace("%3D", "=");

            src = src.Replace("%25", "%");

            return src;
        }

        /// <summary>
        /// GetParameters
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetParameters(string strParam)
        {
            string[] p0 = strParam.Split(SEPARATOR_0);

            Dictionary<string, object> rt = new Dictionary<string, object>();

            for (int i = 0; i < p0.Length; i++)
            {
                string tt0 = p0.GetValue(i) as string;

                if (tt0 == null)
                    continue;

                string[] p1 = tt0.Split(SEPARATOR_1);
                if (p1.Length == 2)
                {
                    string key = p1[0].Trim();
                    string value = p1[1].Trim();
                    value = unescape(value);

                    if (key.Length > 0)
                    {
                        rt.Add(key, value);
                    }
                }
            }

            return rt;
        }


        /// <summary>
        /// MakeParameter using dictionary
        /// </summary>
        /// <param name="paramMap"></param>
        /// <returns></returns>
        public static string MakeDicParameter(Dictionary<string, string> paramMap)
        {
            string rt = "";

            foreach (string key in paramMap.Keys)
            {
                rt += key + SEPARATOR_1 + escape(paramMap[key]) + SEPARATOR_0;
            }

            return rt;
        }

        /// <summary>
        /// GetParameters
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns>return Dictionary<string,string></returns>
        public static Dictionary<string, string> GetDicParameters(string strParam)
        {
            string[] p0 = strParam.Split(SEPARATOR_0);

            Dictionary<string, string> rt = new Dictionary<string, string>();

            for (int i = 0; i < p0.Length; i++)
            {
                string tt0 = p0.GetValue(i) as string;

                if (tt0 == null)
                    continue;

                string[] p1 = tt0.Split(SEPARATOR_1);
                if (p1.Length == 2)
                {
                    string key = p1[0].Trim();
                    string value = p1[1].Trim();
                    value = unescape(value);

                    if (key.Length > 0)
                    {
                        rt.Add(key, value);
                    }
                }
            }

            return rt;
        }
        /// <summary>
        /// Only called by server terminal
        /// </summary>
        /// <returns></returns>
        public static string GetCurDomain()
        {

            return ConfigurationManager.AppSettings["Domain"];
        }

        /// <summary>
        /// For server get site name
        /// </summary>
        /// <returns></returns>
        public static string GetCurSite()
        {
            return ConfigurationManager.AppSettings["Site"];
        }
    }

    public class FollowupInfo
    {

        public String PatientID { get; set; }
        public String PatientName { get; set; }
        public String Gender { get; set; }
        public String Birthday { get; set; }
        public String RemotePID { get; set; }
        public String AccNo { get; set; }
        public String RemoteAccNo { get; set; }
        public String HisID { get; set; }
        public String ReportGuid { get; set; }
        public String Optional1 { get; set; }
        public String Optional2 { get; set; }
        public String Optional3 { get; set; }
        public String Optional4 { get; set; }
        public String Optional5 { get; set; }
        public String Optional6 { get; set; }

    }
}
