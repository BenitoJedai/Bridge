using System;

namespace Bridge
{
    [Ignore]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class IgnoreAttribute : Attribute
    {
    }
}