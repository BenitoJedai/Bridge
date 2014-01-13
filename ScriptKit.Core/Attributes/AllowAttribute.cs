using System;

namespace ScriptKit.Core
{
    [ScriptKit.Core.Ignore]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface, AllowMultiple = true)]
    public class AllowAttribute : Attribute
    {
    }    
}