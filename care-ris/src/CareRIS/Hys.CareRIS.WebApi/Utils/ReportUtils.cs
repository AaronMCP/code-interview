using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Hys.CareRIS.WebApi.Utils
{
    /// <summary>
    /// for report
    /// </summary>
    public class ReportUtils
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string LocalizeCurrentAge(string src, IEnumerable<DictionaryDto> dictionaries)
        {
            if (src == null)
                return "";

            try
            {
                string strTemp = src;
                string[] split = strTemp.Split(new Char[] { ' ' });
                if (split.Length < 2)
                {
                    throw new Exception("Invalid current age");
                }

                //src = split[0] + GetLanguage(split[1]);
                //src = split[0] + DictionaryManager.Instance.GetText((int)DictionaryTag.AgeUnit, split[1]);
                src = split[0] + ReportUtils.GetDictionaryText(dictionaries, (int)DictionaryTag.AgeUnit, split[1]); ;
            }
            catch (Exception ex)
            {
                //  logger.Error((long)ModuleEnum.Register_Client, ModuleInstanceName.Registration, 53, ex.Message, Application.StartupPath.ToString(), (new System.Diagnostics.StackFrame(true)).GetFileName(),

                //   (new System.Diagnostics.StackFrame(true)).GetFileLineNumber());

            }

            return src;
        }

        public static bool isNeedLocalizationAsUserName(string fieldName)
        {
            fieldName = fieldName.ToUpper();

            if (fieldName.Equals("PREVIOUSOWNER") || fieldName.Equals("CURRENTOWNER"))
            {
                return false;
            }

            return fieldName.EndsWith("ER")
                || fieldName.EndsWith("OR")
                || fieldName.EndsWith("CIAN")
                || fieldName.EndsWith("NURSE")
                || fieldName.EndsWith("REJECTTOOBJECT")
                || fieldName.EndsWith("REGISTRAR");
        }

        public static string GetStringFromDataTable(DataTable dt, string colName)
        {
            //
            //may be multi-rows
            if (dt == null || dt.Rows.Count < 1)
            {
                return "";
            }

            if (!dt.Columns.Contains(colName))
            {
                return "";
            }

            string ret = "";

            System.Collections.Specialized.StringCollection strCol = new System.Collections.Specialized.StringCollection();
            foreach (DataRow dr in dt.Rows)
            {
                string tmp = dr[colName].ToString();

                if (isNeedLocalizationAsUserName(colName))
                {
                    //tmp = DictionaryManager.Instance.GetUserLocalName(tmp);
                }

                if (!strCol.Contains(tmp))
                {
                    strCol.Add(tmp);

                    ret += tmp + ",";
                }
            }

            ret = ret.Trim(", ".ToCharArray());

            return ret;
        }

        public static string GetStringFromBytes(object buff)
        {
            try
            {
                if (buff != null && !(buff is DBNull))
                    return System.Text.Encoding.Default.GetString(buff as byte[]);
            }
            catch (Exception)
            {
            }

            return "";
        }

        public static string GetUnicodeStringFromBytes(object buff)
        {
            try
            {
                if (buff != null && !(buff is DBNull))
                    return System.Text.Encoding.Unicode.GetString(buff as byte[]);
            }
            catch (Exception)
            {
            }

            return "";
        }

        public static void DeleteOutdatedFolder(string rootPath)
        {
            try
            {
                string checkFolder = System.DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(rootPath);

                DirectoryInfo[] subs = dir.GetDirectories();

                for (int i = subs.Length - 1; i >= 0; --i)
                {
                    try
                    {
                        if (string.Compare(subs[i].Name, checkFolder) < 0)
                            subs[i].Delete(true);
                    }
                    catch (System.Exception ex)
                    {
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        public static string StringRight(string src, int count)
        {
            return src.Length >= count ? src.Substring(src.Length - count) : src;
        }

        public static string GetDictionaryText(IEnumerable<DictionaryDto> dictionaries, int tag, string value)
        {
            string ret = "";
            DictionaryDto dictionaryDto = dictionaries.Where(d => d.Tag == tag).FirstOrDefault();
            if (dictionaryDto != null)
            {
                DictionaryValueDto dictionaryValueDto = dictionaryDto.Values.Where(d => d.Value == value).FirstOrDefault();
                if (dictionaryValueDto != null)
                {
                    ret = dictionaryValueDto.Text;
                }
            }
            return ret;
        }

        public static string GetMyIP()
        {
            // ::1 is localhost
            return HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1");
        }
    }
}