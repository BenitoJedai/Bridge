using System;

namespace ScriptKit.CLR
{
    [ScriptKit.CLR.Ignore, AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface)]
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