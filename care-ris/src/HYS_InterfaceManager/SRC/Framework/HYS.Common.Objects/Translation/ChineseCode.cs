using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Objects.Translation
{
    public class ChineseCode
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int MultiByteToWideChar(int CodePage, int dwFlags, StringBuilder lpMultiByteStr, int cchMultiByte, byte[] lpWideCharStr, int cchWideChar);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int WideCharToMultiByte(int CodePage, int dwFlags, byte[] lpWideCharStr, int cchWideChar, StringBuilder lpMultiByteStr, int cchMultiByte, string lpDefaultChar, StringBuilder lpUsedDefaultChar);

        [DllImport("kernel32.dll", EntryPoint = "LCMapStringA")]
        private static extern int LCMapString(int Locale, int dwMapFlags, byte[] lpSrcStr, int cchSrc, byte[] lpDestStr, int cchDest);

        private const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        private const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

        public static string GB2GBK(string strSource)
        {
            byte[] source = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
            byte[] dest = new byte[source.Length];
            LCMapString(0x0804, LCMAP_TRADITIONAL_CHINESE, source, -1, dest, source.Length);
            return System.Text.Encoding.GetEncoding("GB2312").GetString(dest);
        }

        public static string GBK2GB(string strSource)
        {
            byte[] source = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
            byte[] dest = new byte[source.Length];
            LCMapString(0x0804, LCMAP_SIMPLIFIED_CHINESE, source, -1, dest, source.Length);
            return System.Text.Encoding.GetEncoding("GB2312").GetString(dest);
        }

        public static string BIG52GBK(string strSource)
        {
            byte[] blist = Encoding.GetEncoding("GB18030").GetBytes(strSource);
            string strOut = Encoding.GetEncoding("BIG5").GetString(blist);
            return strOut;

            //if (strSource.Length <= 10) return _BIG52GBK(strSource);
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < strSource.Length; i += 10)
            //{
            //    int length = strSource.Length - i;
            //    if (length > 10) length = 10;
            //    string str = strSource.Substring(i, length);
            //    str = _BIG52GBK(str);
            //    sb.Append(str);
            //}
            //return sb.ToString();
        }

        private static string _BIG52GBK(string strSource)
        {
            StringBuilder sbSource = new StringBuilder(strSource);
            byte[] sbTarget = new byte[strSource.Length * 2];
            int nReturn = MultiByteToWideChar(950, 0, sbSource, strSource.Length * 2, sbTarget, strSource.Length * 2);
            StringBuilder sbOut = new StringBuilder();
            nReturn = WideCharToMultiByte(936, 0, sbTarget, nReturn, sbOut, strSource.Length * 2, "?", null);
            string str = sbOut.ToString();
            return str;
        }

        public static string GBK2BIG5(string strSource)
        {
            byte[] blist = Encoding.GetEncoding("BIG5").GetBytes(strSource);
            string strOut = Encoding.GetEncoding("GB18030").GetString(blist);
            return strOut;

            //if (strSource.Length <= 10) return _GBK2BIG5(strSource);
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < strSource.Length; i += 10)
            //{
            //    int length = strSource.Length - i;
            //    if (length > 10) length = 10;
            //    string str = strSource.Substring(i, length);
            //    str = _GBK2BIG5(str);
            //    sb.Append(str);
            //}
            //return sb.ToString();
        }

        private static string _GBK2BIG5(string strSource)
        {
            StringBuilder sbSource = new StringBuilder(strSource);
            byte[] sbTarget = new byte[strSource.Length * 2];
            int nReturn = MultiByteToWideChar(936, 0, sbSource, strSource.Length * 2, sbTarget, strSource.Length * 2);
            StringBuilder sbOut = new StringBuilder();
            nReturn = WideCharToMultiByte(950, 0, sbTarget, nReturn, sbOut, strSource.Length * 2, "?", null);
            string str = sbOut.ToString();
            return str;
        }

        public static string BIG52GB(string strSource)
        {
            return GBK2GB(BIG52GBK(strSource));
        }

        public static string GB2BIG5(string strSource)
        {
            return GBK2BIG5(GB2GBK(strSource));
        }

        public static string Convert(ChineseCodeConvertType type, string strSource)
        {
            return Convert(type, strSource, null);
        }
        public static string Convert(ChineseCodeConvertType type, string strSource, ILogging log)
        {
            try
            {
                switch (type)
                {
                    default: return strSource;
                    case ChineseCodeConvertType.BIG52GB: return BIG52GB(strSource);
                    case ChineseCodeConvertType.BIG52GBK: return BIG52GBK(strSource);
                    case ChineseCodeConvertType.GB2BIG5: return GB2BIG5(strSource);
                    case ChineseCodeConvertType.GB2GBK: return GB2GBK(strSource);
                    case ChineseCodeConvertType.GBK2BIG5: return GBK2BIG5(strSource);
                    case ChineseCodeConvertType.GBK2GB: return GBK2GB(strSource);
                }
            }
            catch (Exception err)
            {
                if (log != null) log.Write(LogType.Error, err.ToString());
                return "";
            }
        }
    }

    public enum ChineseCodeConvertType
    {
        None,
        GB2GBK,
        GBK2GB,
        BIG52GBK,
        GBK2BIG5,
        BIG52GB,
        GB2BIG5
    }
}
