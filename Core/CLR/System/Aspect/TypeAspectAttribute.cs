using System;

namespace Bridge.CLR
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.TypeAspectAttribute")]
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

        protected virtual bool RunTimeValidate(object instance)
        {
            return true;
        }
    }

    public class TypeAspectEventArgs
    {
        public readonly object Instance;
    }
}