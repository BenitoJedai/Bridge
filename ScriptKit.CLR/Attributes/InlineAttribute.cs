using System;

namespace ScriptKit.Core
{
    [ScriptKit.Core.Ignore, AttributeUsage(AttributeTargets.Method)]
    public sealed class InlineAttribute : Attribute
    {
        public InlineAttribute(string format) { }
    }
}
