using System;

namespace Bridge.Foundation
{
    [Ignore]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class AdapterAttribute : Attribute
    {
    }
}