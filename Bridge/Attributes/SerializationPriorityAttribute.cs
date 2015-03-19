using System;

namespace Bridge
{
    [Ignore]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface, AllowMultiple = true)]
    public class SerializationPriorityAttribute : Attribute
    {
        public SerializationPriorityAttribute(int priority)
        {
        }
    }    
}