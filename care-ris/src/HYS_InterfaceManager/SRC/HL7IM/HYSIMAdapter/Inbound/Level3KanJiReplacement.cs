using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Controler
{
    public class Level3KanJiReplacement
    {
        public static string Replacement(string sourceString, string replacementChar)
        {
            if (sourceString == null || string.IsNullOrEmpty(sourceString) || string.IsNullOrEmpty(replacementChar))
                return sourceString;

            return ConvIR87Name(sourceString, replacementChar);
            
        }

        private static string ConvIR87Name(string SourceValue, string replacementChar)
        {
            string ir87Name = String.Empty;
            List<string> SourceValueArray = new List<string>();

            for (int i = 0; i < SourceValue.Length; i++)
            {
                SourceValueArray.Add(SourceValue[i].ToString());
            }
            foreach (string oneStr in SourceValueArray)
            {
                bool one = false;
                bool twe = false;
                bool not = false;
                bool hanNum = false;
                bool hanKana = false;
                bool hanSpace = false;
                bool hanEigo = false;

                byte[] byteName = System.Text.Encoding.GetEncoding("shift-jis").GetBytes(oneStr);

                //第一水准
                if (byteName[0] >= 0x88 && byteName[0] < 0x98)
                {
                    one = true;
                }
                else if (byteName[0] == 0x98 && byteName[1] <= 0x72)
                {
                    one = true;
                }
                // 第二水准
                else if (byteName[0] >= 0x98 && byteName[0] < 0xEA)
                {
                    twe = true;
                }
                else if (byteName[0] == 0xEA && byteName[1] <= 0xA4)
                {
                    twe = true;
                }
                // 非汉字
                else if (byteName[0] >= 0x81 && byteName[0] < 0x84)
                {
                    not = true;
                }
                else if (byteName[0] == 0x84 && byteName[1] <= 0xBE)
                {
                    not = true;
                }

                // single byte
                else if (System.Text.RegularExpressions.Regex.IsMatch(oneStr, @"[\uFF61-\uFF9F]") || System.Text.RegularExpressions.Regex.IsMatch(oneStr,  @"[-|!|@|#|$|%|^|&|*|(|)|_|+|{|}|:|<|>|?|=|,|.]"))
                {
                    hanKana = true;
                }
                // 半角space
                else if (byteName[0] == 0x20)
                {
                    hanSpace = true;
                }
                // 半角数字
                else if (System.Text.RegularExpressions.Regex.IsMatch(oneStr, @"^[0-9]+$"))
                {
                    hanNum = true;
                }
                // 半角英文
                else if (System.Text.RegularExpressions.Regex.IsMatch(oneStr, @"^[a-zA-Z]+$"))
                {
                    hanEigo = true;
                }
                // 忋婰偺偳傟偐偵偁偰偼傑傟偽丄偦偺傑傑偱OK
                if (one || twe || not || hanNum || hanKana || hanSpace || hanNum || hanEigo)
                {
                    ir87Name += oneStr;
                }
                // 偁偰偼傑傜側偗傟偽暁帤曄姺
                else
                {

                    ir87Name += replacementChar;
                }
            }
            return ir87Name;
        } 
    }
}
