using System;
using System.Collections.Generic;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AdapterServiceEntryAttribute : AdapterEntryAttributeBase
    {
        public AdapterServiceEntryAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public AdapterServiceEntryAttribute(string name, DirectionType type, string description)
        {
            Name = name;
            Direction = type;
            Description = description;
        }
    }
}
