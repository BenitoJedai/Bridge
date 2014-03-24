using System;

namespace ScriptKit.CLR
{
    [ScriptKit.CLR.Ignore, AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ConstructorAttribute : Attribute
    {
        public ConstructorAttribute(string value) 
        { 
        }
    }
}