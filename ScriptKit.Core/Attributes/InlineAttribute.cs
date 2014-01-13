using System;

namespace ScriptKit.Core
{
    [ScriptKit.Core.Ignore]
    [AttributeUsage(AttributeTargets.Method)]
    public class InlineAttribute : Attribute
    {
        public InlineAttribute(string value) 
        { 
        }
    }
}