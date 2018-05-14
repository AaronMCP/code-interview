using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Objects.Translation
{
    public abstract class PinyinBase : IPinyin
    {
        internal abstract string Convert(string chinese);
        internal abstract string Convert(string chinese, ILogging log);

        public string ConvertName(string chineseName)
        {
            return ConvertName(chineseName, null);
        }
        public string ConvertName(string chineseName, ILogging log)
        {
            if (chineseName == null || chineseName.Length < 1) return "";
            char[] chrList = chineseName.ToCharArray();
            StringBuilder sb = new StringBuilder();
            foreach (char chr in chrList)
            {
                if ((int)chr < 128)
                {
                    sb.Append(chr);
                }
                else
                {
                    string py = Convert(chr.ToString(), log);
                    char[] pyList = py.ToCharArray();
                    if (pyList.Length < 1) continue;
                    sb.Append(pyList[0].ToString().ToUpper());
                    for (int i = 1; i < pyList.Length; i++) sb.Append(pyList[i]);
                    sb.Append(' ');
                }
            }
            return sb.ToString().TrimEnd(' ');
        }
        //public string ConvertName(string chineseName, ILogging log)
        //{
        //    if (chineseName == null || chineseName.Length < 1) return "";
        //    char[] chrList = chineseName.ToCharArray();
        //    StringBuilder sb = new StringBuilder();
        //    foreach (char chr in chrList)
        //    {
        //        if ((int)chr < 128)
        //        {
        //            sb.Append(chr);
        //        }
        //        else
        //        {
        //            string py = Convert(chr.ToString(), log);
        //            char[] pyList = py.ToCharArray();
        //            if (pyList.Length < 1) continue;
        //            sb.Append(pyList[0].ToString().ToUpper());
        //            for (int i = 1; i < pyList.Length; i++) sb.Append(pyList[i]);
        //        }
        //    }
        //    return sb.ToString();
        //}
    }
}
