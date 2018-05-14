using System;

namespace AutoMapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoMapAttribute : Attribute
    {
        public Type TargetType { get; set; }
        public AutoMapAttribute(Type types)
        {
            TargetType = types;
        }
    }
}