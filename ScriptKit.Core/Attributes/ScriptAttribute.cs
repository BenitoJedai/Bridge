using System;

namespace ScriptKit.Core
{
    [ScriptKit.Core.Ignore]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ScriptAttribute : Attribute
    {
        public ScriptAttribute(params string[] lines) 
        { 
        }
    }
}
