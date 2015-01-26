using System;
using Bridge.CLR;

namespace Bridge.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.TypeAspectAttribute")]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple=true)]
    public abstract class TypeAspectAttribute : MulticastAspectAttribute
    {
        public const string Format = "new {0}({3}).init({2});";
        public const string MergeFormat = "Bridge.merge(new {0}({3}), {{{4}}}).init({2});";

        public readonly string TypeName;
        public readonly object Instance;

        public virtual void OnInstance(TypeAspectEventArgs eventArgs)
        {
        }

        public virtual void OnAfterInstance(TypeAspectEventArgs eventArgs)
        {
        }

        protected virtual bool RunTimeValidate(object instance)
        {
            return true;
        }
    }

    [Ignore]
    [Name("Object")]
    public class TypeAspectEventArgs
    {
        public readonly object Instance;
        public readonly string TypeName;
        public readonly object[] Arguments;
    }
}