using System;

namespace Bridge.CLR
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.MethodAspectAttribute")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Assembly | AttributeTargets.Class)]
    public abstract class MethodAspectAttribute : MulticastAspectAttribute
    {
        public const string Format = "new {0}().init('{1}', {2});";

        public virtual void OnEntry()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void OnSuccess()
        {
        }

        public virtual void OnException()
        {
        }
    }

    public class MethodEventArgs
    {
        public readonly object[] Arguments;

        public readonly string MethodName;

        public readonly object Scope;

        public object ReturnValue;

        public readonly Exception Exception;

        public bool CancelTargetInvocation;

        public bool RethrowException = true;
    }
}