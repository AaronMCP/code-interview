using System;

namespace Hys.CrossCutting.Common.Extensions
{
    public static class StringExtentions
    {
        public static string TrimEnd(this string str, string endString)
        {
            if (!string.IsNullOrWhiteSpace(str) && str.EndsWith(endString))
            {
                return str.Substring(0, str.LastIndexOf(endString, StringComparison.Ordinal));
            }
            return str;
        }
    }
}
