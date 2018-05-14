using System;
using System.Reflection;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Config
{
    public abstract class ConfigBase : XObject
    {
        public bool SetValue(string propertyName, string value)
        {
            return SetValueEx(propertyName, value);
        }
        public object GetValue(string propertyName)
        {
            return GetValueEx(propertyName);
        }
    }
}
