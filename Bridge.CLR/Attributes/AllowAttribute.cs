using System;

namespace Bridge.CLR
{
    [Bridge.CLR.Ignore]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface, AllowMultiple = true)]
    public class AllowAttribute : Attribute
    {
    }    
}