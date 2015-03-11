using System;

namespace Bridge.Foundation
{
    [Ignore]
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Delegate | AttributeTargets.Property)]
    public sealed class NameAttribute : Attribute
    {
        public NameAttribute(string value) 
        { 
        }

        public NameAttribute(bool changeCase)
        {
        }
    }
}