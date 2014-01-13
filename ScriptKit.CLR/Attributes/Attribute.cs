namespace System 
{
    [ScriptKit.Core.Ignore]
    public class Attribute 
    {
        internal Attribute() 
        {
        }
    }

    [ScriptKit.Core.Ignore]
    public enum AttributeTargets 
    {
        All = 32767,
        Class = 4,
        Struct = 8,
        Enum = 16,                
        Method = 64,
        Interface = 1024, 
        Delegate = 4096
    }

    [ScriptKit.Core.Ignore]
    public class AttributeUsageAttribute : Attribute 
    {
        public AttributeUsageAttribute(AttributeTargets validOn) 
        { 
        }

        public bool AllowMultiple { get; set; }
    }
}