using System;

namespace Bridge.CLR
{
    [Bridge.CLR.Ignore, AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Delegate)]
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