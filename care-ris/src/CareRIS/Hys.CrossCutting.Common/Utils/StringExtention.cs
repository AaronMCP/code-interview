using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CrossCutting.Common.Utils
{
    public static class StringExtention
    {
        public static string ToBrokerString(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
