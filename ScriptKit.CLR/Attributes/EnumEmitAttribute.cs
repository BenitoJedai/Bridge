using System;

namespace ScriptKit.CLR
{
    [ScriptKit.CLR.Ignore]
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumEmitAttribute : Attribute
    {
        public EnumEmitAttribute(EnumEmit enumEmit)
        {
        }
    }

    public enum EnumEmit
    {
        Name = 1,
        Value = 2
    }
}