using System;

namespace Bridge.CLR
{
    [Ignore]
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    public sealed class CastAttribute : Attribute
    {
        public CastAttribute(string value) 
        { 
        }
    }
}