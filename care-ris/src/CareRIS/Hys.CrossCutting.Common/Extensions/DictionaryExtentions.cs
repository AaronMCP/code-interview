using System.Collections.Generic;
using System.Reflection;

namespace Hys.CrossCutting.Common.Extensions
{
    public static class DictionaryExtentions
    {
        public static void Overwrite(this Dictionary<string, object> obj, object destination)
        {
            if (obj != null)
            {
                var bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;
                foreach (var item in obj)
                {
                    destination.GetType().GetProperty(item.Key, bindingFlags).SetValue(destination, item.Value);
                }
            }
        }
    }
}
