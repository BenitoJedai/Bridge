using System;

namespace Bridge
{
    [Ignore]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ConstructorAttribute : Attribute
    {
        public ConstructorAttribute(string value) 
        { 
        }
    }
}