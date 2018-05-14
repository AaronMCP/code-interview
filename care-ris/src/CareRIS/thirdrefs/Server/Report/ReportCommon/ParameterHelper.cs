using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ReportCommon
{
    public static class ParameterHelper
    {
        public static T toObject<T>(this Dictionary<string, object> values)
            where T : new()
        {
            T obj = new T();

            PropertyInfo[] props = obj.GetType().GetProperties(
                BindingFlags.Instance | BindingFlags.Public |
                BindingFlags.NonPublic | BindingFlags.SetProperty |
                BindingFlags.SetField);

            foreach (PropertyInfo property in props)
            {
                string key = values.Keys.FirstOrDefault(s => s.ToUpper() == property.Name.ToUpper());

                if (string.IsNullOrWhiteSpace(key))
                    continue;

                object val = values[key];

                try
                {
                    string sVal = Convert.ToString(val);

                    if (property.PropertyType == typeof(bool))
                    {
                        int iVal;
                        bool bVal;

                        if (int.TryParse(sVal, out iVal))
                        {
                            property.SetValue(obj, iVal > 0, null);
                        }
                        else if (bool.TryParse(sVal, out bVal))
                        {
                            property.SetValue(obj, bVal, null);
                        }
                        else
                        {
                            property.SetValue(obj, sVal.ToLower() == "yes" || sVal.ToLower() == "y", null);
                        }
                    }
                    else if (property.PropertyType == typeof(System.Single) ||
                        property.PropertyType == typeof(System.Double))
                    {
                        float tmp;

                        if (float.TryParse(sVal, out tmp))
                        {
                            property.SetValue(obj, tmp, null);
                        }
                        else
                        {
                            //Logger.warn(string.Format("Error to set value to {0} of type {1}",
                            //        property.Name, t.FullName));
                        }
                    }
                    else if (val is byte[] && property.PropertyType == typeof(string))
                    {
                        property.SetValue(obj, System.Text.Encoding.Default.GetString(val as byte[]), null);
                    }
                    else if (val != null && property.PropertyType.IsEnum)
                    {
                        int iVal;
                        object val2 = null;

                        if (int.TryParse(Convert.ToString(val), out iVal))
                        {
                            val2 = Enum.ToObject(property.PropertyType, iVal);

                            property.SetValue(obj, val2, null);
                        }
                        else
                        {
                            val2 = Enum.Parse(property.PropertyType, val.ToString(), true);

                            property.SetValue(obj, val2, null);
                        }
                    }
                    else
                    {
                        property.SetValue(obj, val, null);
                    }
                }
                catch (Exception err)
                {
                    System.Diagnostics.Debug.WriteLine(err.Message);
                }
            }

            return obj;
        }
    }
}
