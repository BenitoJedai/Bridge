using System;
using Bridge.CLR;

namespace Bridge.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.AbstractMethodAspectAttribute")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple=true)]
    public abstract class AbstractMethodAspectAttribute : MulticastAspectAttribute
    {
        public const string Format = "new {0}({3}).init('{1}', {2});";
        public const string MergeFormat = "Bridge.merge(new {0}({3}), {{{4}}}).init('{1}', {2});";

        public readonly string MethodName;
        public readonly object Scope;
        public readonly Delegate TargetMethod;

        public void Init(string methodName, object scope)
        {
        }

        public void Remove()
        {
        }

        protected virtual bool RunTimeValidate(string methodName, object scope)
        {
            return true;
        }
    }

    [Ignore]
    [Name("Object")]
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