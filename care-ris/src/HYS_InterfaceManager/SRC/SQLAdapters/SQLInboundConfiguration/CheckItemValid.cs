using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HYS.SQLInboundAdapterConfiguration
{
    public class CheckItemValid
    {
        public static bool IsValid(string name)
        {
            if (name == null) return false;
            if (name == "") return true;

            string strName = name.Trim();
            if (strName.Length < 1) return false;

            string newName = Regex.Replace(strName, @"[^\w]", "");
            newName = Regex.Replace(newName, @"[^\x00-\xff]", "");
            if (newName.Length != strName.Length) return false;

            char c = newName[0];
            return c >= 'A' && c <= 'z';
        }

        public static bool IsContain(string compareName, List<string> nameSet, StringComparison comparison)
        {
            foreach (string name in nameSet)
            {
                if (compareName.Equals(name, comparison))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
