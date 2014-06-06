using System;

namespace Bridge.CLR
{
    /// <summary>
    /// InlineAttribute is instruction to replace method calling (in expression) by required code
    /// </summary>
    [Ignore]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field)]
    public sealed class TemplateAttribute : Attribute
    {
        internal TemplateAttribute()
        {
        }

        public TemplateAttribute(string format) 
        {
        }
    }
}