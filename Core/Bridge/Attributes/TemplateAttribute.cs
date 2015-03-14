using System;

namespace Bridge
{
    /// <summary>
    /// TemplateAttribute is instruction to replace method calling (in expression) by required code
    /// </summary>
    [Ignore]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Constructor)]
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