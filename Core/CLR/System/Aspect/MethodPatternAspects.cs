using System;
using Bridge.CLR;

namespace Bridge.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.BufferedMethodAttribute")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BufferedMethodAttribute : AbstractMethodAspectAttribute
    {
        public BufferedMethodAttribute(int buffer)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.BarrierMethodAttribute")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class BarrierMethodAttribute : AbstractMethodAspectAttribute
    {
        public BarrierMethodAttribute(int count)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.DelayedMethodAttribute")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class DelayedMethodAttribute : AbstractMethodAspectAttribute
    {
        public DelayedMethodAttribute(int delay)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.ThrottledMethodAttribute")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThrottledMethodAttribute : AbstractMethodAspectAttribute
    {
        public ThrottledMethodAttribute(int interval)
        {
        }
    }
}