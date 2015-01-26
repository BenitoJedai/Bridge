using System;
using Bridge.CLR;

namespace Bridge.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.PropertyAspectAttribute")]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple=true)]
    public abstract class PropertyAspectAttribute : MulticastAspectAttribute
    {
        public const string Format = "new {0}({3}).init('{1}', {2});";
        public const string MergeFormat = "Bridge.merge(new {0}({3}), {{{4}}}).init('{1}', {2});";

        public readonly string PropertyName;
        public readonly object Scope;

        public T Getter<T>()
        {
            return default(T);
        }

        public void Setter(object value)
        {
        }

        public virtual void OnGetValue(PropertyAspectEventArgs eventArgs)
        {
        }

        public virtual void OnSetValue(PropertyAspectEventArgs eventArgs)
        {
        }

        public virtual void OnSuccess(PropertyAspectEventArgs eventArgs)
        {
        }

        public virtual void OnException(PropertyAspectEventArgs eventArgs)
        {
        }

        public void Remove()
        {
        }

        protected virtual Type[] GetExceptionTypes(string propertyName, object scope)
        {
            return null;
        }

        protected virtual bool RunTimeValidate(string propertyName, object scope)
        {
            return true;
        }
    }

    [Ignore]
    [Name("Object")]
    public class PropertyAspectEventArgs
    {
        public readonly string PropertyName;

        public readonly object Scope;

        public object Value;

        public readonly Exception Exception;

        public AspectFlow Flow;

        public readonly bool IsGetter;
    }
}