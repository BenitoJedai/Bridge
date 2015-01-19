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
        public const string MergeFormat = "Bridge.merge(new {0}({3}), {{{4}}}).init('{1}', {2});";

        public readonly string MethodName;
        public readonly object Scope;
        public readonly Delegate TargetMethod;

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

    public class MethodAspectEventArgs
    {
        public readonly object[] Arguments;

        public readonly string MethodName;

        public readonly object Scope;

        public object ReturnValue;

        public readonly Exception Exception;

        public AspectFlow Flow;
    }
}