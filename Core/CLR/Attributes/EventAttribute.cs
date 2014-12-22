using System;

namespace Bridge.CLR
{
    [Ignore]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public abstract class EventAttribute : Attribute
    {
    }    
}