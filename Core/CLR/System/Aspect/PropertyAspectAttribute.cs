using System;

namespace Bridge.CLR
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.PropertyAspectAttribute")]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple=true)]
    public abstract class PropertyAspectAttribute : MulticastAspectAttribute
    {
        public const string Format = "new {0}({3}).init('{1}', {2});";
        public const string MergeFormat = "Bridge.merge(new {0}({3}), {{{4}}}).init('{1}', {2});";

        public readonly string PropertyName;
        public readonly object Scope;
        public readonly Delegate Getter;
        public readonly Delegate Setter;

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