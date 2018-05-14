using System;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AdapterConfigEntryAttribute : AdapterEntryAttributeBase
    {
        public AdapterConfigEntryAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public AdapterConfigEntryAttribute(string name, DirectionType type, string description)
        {
            Name = name;
            Direction = type;
            Description = description;
        }
    }
}
