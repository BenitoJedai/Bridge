using System;

namespace Bridge.CLR
{
    [Bridge.CLR.Ignore]
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
        Value = 2,
        StringName = 3,
        StringNamePreserveCase = 4,
        StringNameLowerCase = 5,
        StringNameUpperCase = 6
    }
}