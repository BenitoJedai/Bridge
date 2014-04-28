using System;

namespace ScriptKit.CLR
{
    [ScriptKit.CLR.Ignore, AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Field)]
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