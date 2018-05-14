using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Hys.CommonControls
{
    public interface ITranslator
    {
        string GetLanguage(string src, string wParam, string lParam);
    }

    public static class CSCommonFunctions
    {
        public static void log(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg + "\t\t at " + System.DateTime.Now.ToString("mm:ss fff"));
        }

        public static void assert(bool check, string msg)
        {
            System.Diagnostics.Debug.Assert(check, msg);
        }

        public static EnumType toEnumType<EnumType>(object value)
        {
            return toEnumType<EnumType>(System.Convert.ToString(value));
        }

        public static EnumType toEnumType<EnumType>(string value)
        {
            System.Type typ = typeof(EnumType);
            int nValue = 0;

            if (Enum.IsDefined(typ, value))
            {
                return (EnumType)Enum.Parse(typ, value);
            }
            else if (typ.IsEnum && int.TryParse(value, out nValue))
            {
                return (EnumType)Enum.ToObject(typ, nValue);
            }

            return default(EnumType);
        }

        /// <summary>
        /// Convert2Color
        /// </summary>
        /// <param name="szColor"></param>
        /// <param name="retColor"></param>
        /// <returns></returns>
        public static bool Convert2Color(object szColor, ref Color retColor)
        {
            string tmp = szColor as string;
            Color val = default(Color);

            if (tmp == null)
            {
                //
            }
            else if (tmp.StartsWith("#"))
            {
                string tmp2 = "FF" + tmp.Substring(1);
                int tmp3 = int.Parse(tmp2, System.Globalization.NumberStyles.HexNumber);

                val = Color.FromArgb(tmp3);
            }
            else if (tmp.IndexOf(',') > 0)
            {
                string[] arr0 = tmp.Split(",".ToCharArray());
                if (arr0.Length == 3)
                {
                    int r, g, b;

                    int.TryParse(arr0[0], out r);
                    int.TryParse(arr0[1], out g);
                    int.TryParse(arr0[2], out b);

                    val = Color.FromArgb(r, g, b);
                }
                else if (arr0.Length == 4)
                {
                    int a, r, g, b;

                    int.TryParse(arr0[0], out a);
                    int.TryParse(arr0[1], out r);
                    int.TryParse(arr0[2], out g);
                    int.TryParse(arr0[3], out b);

                    val = Color.FromArgb(a, r, g, b);
                }
            }
            else
            {
                val = Color.FromName(tmp);
            }

            if (val != default(Color))
            {
                retColor = val;
            }

            return val != default(Color);
        }
    }
}
