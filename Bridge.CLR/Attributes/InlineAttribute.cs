using System;

namespace Bridge.CLR
{
    /// <summary>
    /// InlineAttribute is instruction to replace method calling (in expression) by required code
    /// </summary>
    [Bridge.CLR.Ignore, AttributeUsage(AttributeTargets.Method | AttributeTargets.Field)]
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
