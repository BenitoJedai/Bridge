using System;

namespace ScriptKit.Core
{
    [ScriptKit.Core.Ignore]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ConstructorAttribute : Attribute
    {
        public ConstructorAttribute(string value)
        {
        }
    }
}