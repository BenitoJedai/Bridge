using System;

namespace Bridge.CLR
{
    [Bridge.CLR.Ignore, AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ConstructorAttribute : Attribute
    {
        public ConstructorAttribute(string value) 
        { 
        }
    }
}