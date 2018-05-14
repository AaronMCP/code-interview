using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Common.ParserUtil
{
    public class CommonFun
    {
        /// <summary>
        /// compare strings, like "a,b,c" and "c,d,e" , whether contain the same substring on split by "separator"
        /// ignoring case and blank.
        /// </summary>
        /// <param name="src1"></param>
        /// <param name="src2"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static bool ContainSameSubString(string src1, string src2, char separator)
        {
            if (src1 == null || src2 == null || separator == null)
                return false;

            string[] a1 = src1.Split(separator);
            string[] a2 = src2.Split(separator);

            for (int i = 0; i < a1.Length; i++)
            {
                for (int m = 0; m < a2.Length; m++)
                {
                    string s1 = a1.GetValue(i) as string;
                    string s2 = a2.GetValue(m) as string;

                    if (s1 == null) s1 = "";
                    if (s2 == null) s2 = "";

                    s1 = s1.Trim().ToUpper();
                    s2 = s2.Trim().ToUpper();

                    if ((s1.Length + s2.Length) > 0 && s1.CompareTo(s2) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetTimeString(DateTime dtime)
        {
            return dtime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetTimeString(object obj, string formatString)
        {
            try
            {
                DateTime dt = System.Convert.ToDateTime(obj);
                return dt.ToString(formatString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetTimeString, MSG=" + ex.Message);
            }

            return string.Empty;
        }

        public static int getSafeInt(string src)
        {
            int ret = 0;

            try
            {
                int.TryParse(src, out ret);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR, getSafeInt, SRC=" + src + ", MSG=" + ex.Message);
            }

            return ret;
        }

        public static float getSafeFloat(object src)
        {
            float ret = 0;

            try
            {
                float.TryParse(System.Convert.ToString(src), out ret);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR, getSafeFloat, SRC=" + System.Convert.ToString(src) + ", MSG=" + ex.Message);
            }

            return ret;
        }

        public static DateTime getSafeDateTime(object src)
        {
            DateTime ret = System.DateTime.Now;

            try
            {
                DateTime.TryParse(System.Convert.ToString(src), out ret);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR, getSafeDateTime, SRC=" + System.Convert.ToString(src) + ", MSG=" + ex.Message);
            }

            return ret;
        }
    }

    public static class PinYin
    {
        /// <summary> 
        /// 在指定的字符串列表CnStr中检索符合拼音索引字符串 
        /// </summary> 
        /// <param name="CnStr">汉字字符串</param> 
        /// <returns>相对应的汉语拼音首字母串</returns> 
        public static string GetSpellCode(string CnStr)
        {
            string strTemp = "";
            int iLen = CnStr.Length;
            int i = 0;

            for (i = 0; i <= iLen - 1; i++)
            {
                strTemp += GetCharSpellCode(CnStr.Substring(i, 1));
            }

            return strTemp;
        }

        /// <summary> 
        /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母 
        /// </summary> 
        /// <param name="CnChar">单个汉字</param> 
        /// <returns>单个大写字母</returns> 
        private static string GetCharSpellCode(string CnChar)
        {
            long iCnChar;

            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);

            //如果是字母，则直接返回 
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                // get the array of byte from the single char 
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }

            //expresstion 
            //table of the constant list 
            // 'A'; //45217..45252 
            // 'B'; //45253..45760 
            // 'C'; //45761..46317 
            // 'D'; //46318..46825 
            // 'E'; //46826..47009 
            // 'F'; //47010..47296 
            // 'G'; //47297..47613 

            // 'H'; //47614..48118 
            // 'J'; //48119..49061 
            // 'K'; //49062..49323 
            // 'L'; //49324..49895 
            // 'M'; //49896..50370 
            // 'N'; //50371..50613 
            // 'O'; //50614..50621 
            // 'P'; //50622..50905 
            // 'Q'; //50906..51386 

            // 'R'; //51387..51445 
            // 'S'; //51446..52217 
            // 'T'; //52218..52697 
            //没有U,V 
            // 'W'; //52698..52979 
            // 'X'; //52980..53640 
            // 'Y'; //53689..54480 
            // 'Z'; //54481..55289 

            // iCnChar match the constant 
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }

            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53640))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else return ("?");
        }
    }
}
