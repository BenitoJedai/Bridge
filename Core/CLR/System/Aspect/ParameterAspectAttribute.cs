using System;
using Bridge.CLR;

namespace Bridge.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.ParametertAspectAttribute")]
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ParameterAspectAttribute : MulticastAspectAttribute
    {
        public const string Format = "new {0}({3}).init('{4}', {5}, {6}, '{1}', {2});";
        public const string MergeFormat = "Bridge.merge(new {0}({3}), {{{4}}}).init('{5}', {6}, {7},'{1}', {2});";

        public readonly string ParameterName;
        public readonly int ParameterIndex;
        public readonly Type ParameterType;
        public readonly string MethodName;
        public readonly object Scope;
        public readonly Delegate TargetMethod;

        public void Remove()
        {
        }

        protected virtual bool RunTimeValidate(string parameterName, int parameterIndex, Type parameterType, string methodName, object scope)
        {
            return true;
        }

        public virtual void ParameterValidate(ParameterAspectEventArgs eventArgs)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.ParameterContractAspectAttribute")]
    public abstract class ParameterContractAspectAttribute : ParameterAspectAttribute
    {
        public string Message;

        public virtual Exception CreateException(ParameterAspectEventArgs eventArgs)
        {
            return null;
        }

        public sealed override void ParameterValidate(ParameterAspectEventArgs eventArgs)
        {
        }

        public abstract bool Validate(ParameterAspectEventArgs eventArgs);
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.CreditCardAttribute")]
    public class CreditCardAttribute : ParameterContractAspectAttribute
    {
        public CreditCardAttribute()
        {
        }

        public CreditCardAttribute(CreditCardType type)
        {
        }

        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.EmailAttribute")]
    public class EmailAttribute : ParameterContractAspectAttribute
    {
        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.UrlAttribute")]
    public class UrlAttribute : ParameterContractAspectAttribute
    {
        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.GreaterThanAttribute")]
    public class GreaterThanAttribute : ParameterContractAspectAttribute
    {
        public GreaterThanAttribute(int min)
        {
        }

        public GreaterThanAttribute(int min, bool strict)
        {
        }

        public GreaterThanAttribute(double min)
        {
        }

        public GreaterThanAttribute(double min, bool strict)
        {
        }

        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.LessThanAttribute")]
    public class LessThanAttribute : ParameterContractAspectAttribute
    {
        public LessThanAttribute(int max)
        {
        }

        public LessThanAttribute(int max, bool strict)
        {
        }

        public LessThanAttribute(double max)
        {
        }

        public LessThanAttribute(double max, bool strict)
        {
        }

        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.NotEmptyAttribute")]
    public class NotEmptyAttribute : ParameterContractAspectAttribute
    {
        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.NotNullAttribute")]
    public class NotNullAttribute : ParameterContractAspectAttribute
    {
        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.PositiveAttribute")]
    public class PositiveAttribute : ParameterContractAspectAttribute
    {
        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.NegativeAttribute")]
    public class NegativeAttribute : ParameterContractAspectAttribute
    {
        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.RangeAttribute")]
    public class RangeAttribute : ParameterContractAspectAttribute
    {
        public RangeAttribute(int min, int max)
        {
        }

        public RangeAttribute(int min, int max, bool strict)
        {
        }

        public RangeAttribute(double min, double max)
        {
        }

        public RangeAttribute(double min, double max, bool strict)
        {
        }

        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.RequiredAttribute")]
    public class RequiredAttribute : ParameterContractAspectAttribute
    {
        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.LengthAttribute")]
    public class LengthAttribute : ParameterContractAspectAttribute
    {
        public LengthAttribute(int max)
        {
        }

        public LengthAttribute(int max, int min)
        {
        }

        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.RegularExpressionAttribute")]
    public class RegularExpressionAttribute : ParameterContractAspectAttribute
    {
        public RegularExpressionAttribute(string pattern)
        {
        }

        public RegularExpressionAttribute(string pattern, string flags)
        {
        }

        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.Aspects.ValidatorAttribute")]
    public class ValidatorAttribute : ParameterContractAspectAttribute
    {
        public ValidatorAttribute(string fn)
        {
        }

        public sealed override bool Validate(ParameterAspectEventArgs eventArgs)
        {
            return false;
        }
    }    

    [Ignore]
    [Name("Object")]
    public class ParameterAspectEventArgs
    {
        public readonly string ParameterName;
        public readonly int ParameterIndex;
        public readonly Type ParameterType;
        public readonly object Parameter;
        public readonly string MethodName;
        public readonly object Scope;
    }
}