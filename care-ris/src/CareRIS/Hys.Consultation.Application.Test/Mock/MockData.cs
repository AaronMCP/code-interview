using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class MockData
{
    static MockData()
    {
        Generators.Add(typeof(string), typeof(MockData).GetMethod("RandomString"));
    }

    private static readonly Dictionary<Type, MethodInfo> Generators = new Dictionary<Type, MethodInfo>();

    public static T Generate<T>() where T : new()
    {
        if (IsPrimitive(typeof(T)))
        {
            return InternalGenerate<T>();
        }

        var result = new T();

        foreach (var property in typeof(T).GetProperties())
        {
            if (property.CanWrite && IsPrimitive(property.PropertyType))
            {
                if (!Generators.ContainsKey(property.PropertyType))
                {
                    var method = typeof(MockData).GetMethod("InternalGenerate", BindingFlags.NonPublic | BindingFlags.Static);
                    var generic = method.MakeGenericMethod(property.PropertyType);
                    Generators.Add(property.PropertyType, generic);
                }
                var generateResult = Generators[property.PropertyType].Invoke(null, null);
                property.SetValue(result, generateResult, null);
            }
        }

        return result;
    }

    public static T Generate<T>(Action<T> callBack = null) where T : new()
    {
        var result = Generate<T>();
        if (callBack != null)
        {
            callBack(result);
        }
        return result;
    }

    public static IEnumerable<T> Generate<T>(int count = 1, Action<T> callBack = null) where T : new()
    {
        return Enumerable.Range(0, count).Select(i => Generate(callBack));
    }

    #region Internal

    private static bool IsPrimitive(Type type)
    {
        return type.IsPrimitive || type.IsEnum || type.IsValueType || type.Name == "String";
    }

    private static TU InternalGenerate<TU>() where TU : new()
    {
        var type = typeof(TU);

        if (type.IsGenericType)
        {
            type = type.GetGenericArguments().First();
        }

        if (type.IsEnum)
        {
            var values = Enum.GetValues(type);
            return (TU)values.GetValue(new Random().Next(values.Length));
        }

        if (type.IsValueType)
        {
            string value;
            if (type == typeof(DateTime))
            {
                value = RandomDateTime().ToString();
            }
            else if (type == typeof(bool))
            {
                value = RandomBoolean().ToString();
            }
            else if (type == typeof(Guid))
            {
                value = Guid.NewGuid().ToString();
            }
            else
            {
                value = GetRandom(1, true, false, false, false);
            }

            return (TU)System.ComponentModel.TypeDescriptor.GetConverter(type).ConvertFromString(value);
        }

        if (type.IsPrimitive)
        {
            var values = GetRandom(10, true, false, false, false);
            return (TU)System.ComponentModel.TypeDescriptor.GetConverter(type).ConvertFromString(values);
        }
        if (type.Name == "String")
        {
            return (TU)System.ComponentModel.TypeDescriptor.GetConverter(type).ConvertFromString(GetRandom());
        }

        return new TU();
    }

    public static string RandomString()
    {
        return GetRandom();
    }

    public static string GetRandom(int length = 5, bool number = true, bool lower = true, bool upper = true, bool special = true, string custom = null)
    {
        var b = new byte[4];
        new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
        var r = new Random(BitConverter.ToInt32(b, 0));
        string result = null, collections = custom ?? String.Empty;
        if (number) { collections += "0123456789"; }
        if (lower) { collections += "abcdefghijklmnopqrstuvwxyz"; }
        if (upper) { collections += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
        if (special) { collections += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
        for (var i = 0; i < length; i++)
        {
            result += collections.Substring(r.Next(0, collections.Length - 1), 1);
        }
        return result;
    }

    public static DateTime RandomDateTime()
    {
        var start = new DateTime(1995, 1, 1);
        var random = new Random();
        var range = (DateTime.Today - start).Days;
        return start.AddDays(random.Next(range));
    }

    public static bool RandomBoolean()
    {
        return new Random().Next(100) % 2 == 0;
    }

    #endregion
}