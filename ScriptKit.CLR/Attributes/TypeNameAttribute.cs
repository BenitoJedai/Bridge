using System;

namespace ScriptKit.CLR
{
    [ScriptKit.CLR.Ignore, AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class TypeNameAttribute : Attribute
    {
        public TypeNameAttribute(string value) { }
    }
}