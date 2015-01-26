using System;
using Bridge.CLR;

namespace Bridge.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.MethodAspectAttribute")]
    public abstract class MethodAspectAttribute : AbstractMethodAspectAttribute
    {
        public virtual void OnEntry(MethodAspectEventArgs eventArgs)
        {
        }

        public virtual void OnExit(MethodAspectEventArgs eventArgs)
        {
        }

        public virtual void OnSuccess(MethodAspectEventArgs eventArgs)
        {
        }

        public virtual void OnException(MethodAspectEventArgs eventArgs)
        {
        }

        protected virtual Type[] GetExceptionTypes(string methodName, object scope)
        {
            return null;
        }
    }
}