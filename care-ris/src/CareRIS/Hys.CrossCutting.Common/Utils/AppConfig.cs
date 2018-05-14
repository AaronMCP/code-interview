using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CrossCutting.Common.Utils
{
    public class AppConfig
    {
        private static string _jsonString;
        public string this[string index]
        {
            get
            {
                return ConfigurationManager.AppSettings[index];
            }
        }
        public static string JsonString
        {
            get
            {
                if (string.IsNullOrEmpty(_jsonString))
                {
                    var sb = new StringBuilder("{");
                    var empty = true;
                    var configSetting = ConfigurationManager.AppSettings;
                    foreach (string key in configSetting)
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\",", key, configSetting[key]);
                        if (empty)
                        {
                            empty = false;
                        }
                    }

                    if (!empty)
                    {
                        sb.Remove(sb.Length - 1, 1);
                    }
                    sb.Append("}");
                    _jsonString = sb.ToString();
                }
                return _jsonString;
            }
        }
    }
}
