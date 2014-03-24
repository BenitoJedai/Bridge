using System;

namespace ScriptKit.CLR
{
    [ScriptKit.CLR.Ignore]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface, AllowMultiple = true)]
    public sealed class IgnoreAttribute : Attribute
    {
    }
}