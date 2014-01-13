using System;

namespace ScriptKit.Core
{
    [ScriptKit.Core.Ignore, AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class TypeNameAttribute : Attribute
    {
        public TypeNameAttribute(string value) 
        { 
        }
    }
}