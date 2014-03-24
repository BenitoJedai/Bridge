using System;

namespace ScriptKit.CLR
{
    /// <summary>
    /// InlineAttribute is unstruction to replace method calling (in expression) by required code
    /// </summary>
    [ScriptKit.CLR.Ignore, AttributeUsage(AttributeTargets.Method)]
    public sealed class InlineAttribute : Attribute
    {
        
        public InlineAttribute(string format) 
        {
            this.Format = format;
        }

        public string Format
        {
            get;
            private set;
        }
    }
}
