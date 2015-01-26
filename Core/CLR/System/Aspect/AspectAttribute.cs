using System;
using Bridge.CLR;

namespace Bridge.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.AspectAttribute")]
    [SerializationPriority(10000)]
    public abstract class AspectAttribute : Attribute
    {
    }
}