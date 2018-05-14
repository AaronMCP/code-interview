using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace StarCommon
{
    public class StarConvert
    {
        public static string DBValueToString(object value, string defvalue)
        {
            if (Convert.IsDBNull(value))
                return defvalue;
            else
                return Convert.ToString(value);
        }
        public static int DBValueToInt32(object value, int defvalue)
        {
            if (Convert.IsDBNull(value))
                return defvalue;
            else
                return Convert.ToInt32(value);
        }
        public static bool DBValueToBool(object value, bool defvalue)
        {
            if (Convert.IsDBNull(value))
                return defvalue;
            else
                return Convert.ToBoolean(value);
        }
    }
}
