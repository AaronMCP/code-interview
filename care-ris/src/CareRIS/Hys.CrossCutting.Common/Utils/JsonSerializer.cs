using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CrossCutting.Common.Utils
{
    public class JsonSerializer<T>
    {
        public static T FromJson(string jsonString)
        {
            if (String.IsNullOrEmpty(jsonString))
            {
                return default(T);
            }

            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                ms.Seek(0, SeekOrigin.Begin);
                return (T)serializer.ReadObject(ms);
            }
        }

        public static string ToJson(T data)
        {
            if (data == null)
            {
                return String.Empty;
            }

            var serializer = new DataContractJsonSerializer(typeof(T), null, int.MaxValue, false,
                new DateTimeSurrogate(), false);

            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, data);
                ms.Seek(0, SeekOrigin.Begin);
                string result;
                using (var sr = new StreamReader(ms))
                {
                    result = sr.ReadToEnd();
                }
                return result;
            }
        }
    }
}
