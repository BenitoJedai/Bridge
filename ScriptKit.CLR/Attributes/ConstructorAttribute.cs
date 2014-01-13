using System;

namespace ScriptKit.Core
{
    [ScriptKit.Core.Ignore, AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ConstructorAttribute : Attribute
    {
        public ConstructorAttribute(string value) 
        { 
        }
    }
}