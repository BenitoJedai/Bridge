using System;

namespace Bridge
{
    [Ignore]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldPropertyAttribute : Attribute
    {
    }
}