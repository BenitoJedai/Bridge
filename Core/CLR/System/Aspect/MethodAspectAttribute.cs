using System;

namespace Bridge.CLR
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.MethodAspectAttribute")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple=true)]
    public abstract class MethodAspectAttribute : MulticastAspectAttribute
    {
        public const string Format = "new {0}({3}).init('{1}', {2});";
        public const string MergeFormat = "Bridge.merge(new {0}({3}, {{{4}}}).init('{1}', {2});";

        public virtual void OnEntry(MethodEventArgs eventArgs)
        {
        }

        public virtual void OnExit(MethodEventArgs eventArgs)
        {
        }

        public virtual void OnSuccess(MethodEventArgs eventArgs)
        {
        }

        public virtual void OnException(MethodEventArgs eventArgs)
        {
        }

        public void Init(string methodName, object scope)
        {
        }

        public void Remove()
        {
        }

        protected virtual Type[] GetExceptionTypes(string methodName, object scope)
        {
            return null;
        }

        protected virtual bool RunTimeValidate(string methodName, object scope)
        {
            return true;
        }
    }

    public class MethodEventArgs
    {
        public readonly object[] Arguments;

        public readonly string MethodName;

        public readonly object Scope;

        public object ReturnValue;

        public readonly Exception Exception;

        public AspectFlow Flow;
    }
}