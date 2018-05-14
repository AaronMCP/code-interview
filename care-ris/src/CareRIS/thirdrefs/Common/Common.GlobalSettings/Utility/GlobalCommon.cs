using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings.Utility
{
    public class GlobalCommon
    {
        public static string ToShortDateTime(DateTime dt)
        {
            string strResult = "";
            try
            {
                strResult = dt.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
            }
            return strResult;
        }

        public static string ToLongDateTime(DateTime dt)
        {
            string strResult = "";
            try
            {
                strResult = dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
            }
            return strResult;
        }

        /// <summary>
        /// Convert string to DateTime.
        /// </summary>
        /// <param name="strTime">the format of the string must be "yyyy-MM-dd HH:mm:ss".</param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string strTime)
        {
            DateTime dt;

            dt = DateTime.ParseExact(strTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.DateTimeFormatInfo.CurrentInfo);

            return dt;
        }

        /// <summary>
        /// convert datetime to string using 
        /// fixed format yyyy-MM-dd HH:mm:ss.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateTimeToString(DateTime dt)
        {
            string strResult = "";
            strResult = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return strResult;
        }
    }
}
