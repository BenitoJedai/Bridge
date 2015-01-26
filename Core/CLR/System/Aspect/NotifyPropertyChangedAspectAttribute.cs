using System;
using System.ComponentModel;
using Bridge.CLR;

namespace Bridge.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.NotifyPropertyChangedAttribute")]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple=false)]
    public class NotifyPropertyChangedAttribute : MulticastAspectAttribute
    {
        public const string Format = "new {0}({3}).init('{1}', {2});";
        public const string MergeFormat = "Bridge.merge(new {0}({3}), {{{4}}}).init('{1}', {2});";

        public readonly string PropertyName;
        public readonly object Scope;
        public bool RaiseOnChange = true;
        
        public T Getter<T>()
        {
            return default(T);
        }

        public void Setter(object value)
        {
        }

        protected virtual bool RunTimeValidate(string propertyName, object scope)
        {
            return true;
        }

        public static void ResumeEvents()
        {
        }

        public static void ResumeEvents(INotifyPropertyChanged instance)
        {
        }

        public static void SuspendEvents()
        {
        }

        public static void SuspendEvents(INotifyPropertyChanged instance)
        {
        }

        public static void RaiseEvents(INotifyPropertyChanged instance)
        {
        }

        protected virtual void BeforeEvent(NotifyPropertyChangedAspectEventArgs eventArgs)
        {
        }

        public void RaiseEvent()
        {
        }
    }

    [Ignore]
    [Name("Object")]
    public class NotifyPropertyChangedAspectEventArgs
    {
        public readonly string PropertyName;

        public readonly object Scope;

        public readonly object Value;

        public readonly object LastValue;

        public AspectFlow Flow;

        public readonly bool Force;
    }
}