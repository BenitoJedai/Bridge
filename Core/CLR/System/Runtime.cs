using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Bridge.CLR;

namespace System
{
    [Ignore]
    public class Attribute
    {
        internal Attribute()
        {
        }
    }

    [Ignore]
    public enum AttributeTargets
    {
        Assembly = 0x0001,
        Module = 0x0002,
        Class = 0x0004,
        Struct = 0x0008,
        Enum = 0x0010,
        Constructor = 0x0020,
        Method = 0x0040,
        Property = 0x0080,
        Field = 0x0100,
        Event = 0x0200,
        Interface = 0x0400,
        Parameter = 0x0800,
        Delegate = 0x1000,
        ReturnValue = 0x2000,
        GenericParameter = 0x4000,
        Type = Class | Struct | Enum | Interface | Delegate,
        All = Assembly | Module | Class | Struct | Enum | Constructor |
              Method | Property | Field | Event | Interface | Parameter |
              Delegate | ReturnValue | GenericParameter
    }

    [Ignore]
    public class AttributeUsageAttribute : Attribute
    {
        public AttributeUsageAttribute(AttributeTargets validOn)
        {
        }

        public bool AllowMultiple { get; set; }
    }

    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    public class FlagsAttribute : Attribute
    {
    }
}

namespace System.Runtime.InteropServices
{
    [ComVisible(true)]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
    [Ignore]
    public sealed class ComVisibleAttribute : Attribute
    {
        public ComVisibleAttribute(bool visibility)
        {
            this.Value = visibility;
        }

        public bool Value
        {
            get;
            private set;
        }
    }

    [AttributeUsageAttribute(AttributeTargets.Parameter)]
    [Ignore]
    public sealed class OutAttribute : Attribute
    {
    }
}

namespace System.Runtime.InteropServices
{
    [ComVisible(true)]
    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class GuidAttribute : Attribute
    {
        public GuidAttribute(string guid)
        {
            this.Value = guid;
        }

        public string Value
        {
            get;
            private set;
        }
    }
}

namespace System.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyTitleAttribute : Attribute
    {
        public AssemblyTitleAttribute(string title)
        {
            this.Title = title;
        }

        public string Title
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyDescriptionAttribute : Attribute
    {
        public AssemblyDescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public string Description
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyConfigurationAttribute : Attribute
    {
        public AssemblyConfigurationAttribute(string configuration)
        {
            this.Configuration = configuration;
        }

        public string Configuration
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyCompanyAttribute : Attribute
    {
        public AssemblyCompanyAttribute(string company)
        {
            this.Company = company;
        }

        public string Company
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyProductAttribute : Attribute
    {
        public AssemblyProductAttribute(string product)
        {
            this.Product = product;
        }

        public string Product
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyCopyrightAttribute : Attribute
    {
        public AssemblyCopyrightAttribute(string copyright)
        {
            this.Copyright = copyright;
        }

        public string Copyright
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyTrademarkAttribute : Attribute
    {
        public AssemblyTrademarkAttribute(string trademark)
        {
            this.Trademark = trademark;
        }

        public string Trademark
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyCultureAttribute : Attribute
    {
        public AssemblyCultureAttribute(string culture)
        {
            this.Culture = culture;
        }

        public string Culture
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyVersionAttribute : Attribute
    {
        public AssemblyVersionAttribute(string version)
        {
            this.Version = version;
        }

        public string Version
        {
            get;
            private set;
        }
    }

    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class AssemblyFileVersionAttribute : Attribute
    {
        public AssemblyFileVersionAttribute(string version)
        {
            this.Version = version;
        }

        public string Version
        {
            get;
            private set;
        }
    }

    [Ignore]
    public sealed class DefaultMemberAttribute : Attribute
    {
        public DefaultMemberAttribute(string memberName)
        {
            this.MemberName = memberName;
        }

        public string MemberName
        {
            get;
            private set;
        }
    }
}

namespace System.Runtime.Versioning
{
    [AttributeUsage(AttributeTargets.Assembly)]
    [Ignore]
    public sealed class TargetFrameworkAttribute : Attribute
    {
        public TargetFrameworkAttribute()
        {
        }

        public TargetFrameworkAttribute(string frameworkName)
        {
            this.FrameworkName = frameworkName;
        }

        public string FrameworkDisplayName
        {
            get;
            set;
        }

        public string FrameworkName
        {
            get;
            private set;
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field)]
    public sealed class DecimalConstantAttribute : Attribute
    {
        public DecimalConstantAttribute(byte scale, byte sign, int hi, int mid, int low)
        {
        }

        public DecimalConstantAttribute(byte scale, byte sign, uint hi, uint mid, uint low)
        {
        }

        public decimal Value
        {
            get
            {
                return 0m;
            }
        }
    }

    [Ignore]
    public static class RuntimeHelpers
    {
        public static void InitializeArray(Array array, RuntimeFieldHandle handle)
        {
        }

        public static int OffsetToStringData 
        { 
            get 
            { 
                return 0; 
            } 
        }

        public static int GetHashCode(object obj) 
        { 
            return 0; 
        }
    }
    
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
    [Ignore]
    public sealed class ExtensionAttribute : Attribute
    {
        public ExtensionAttribute()
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Ignore]
    public sealed class DynamicAttribute : Attribute
    {        
        public DynamicAttribute() 
        { 
        }

        public DynamicAttribute(bool[] transformFlags) 
        { 
        }
        public List<bool> TransformFlags 
        { 
            get 
            { 
                return null; 
            } 
        }
    }

    [Ignore]
    public class CallSite
    {
        public CallSiteBinder Binder 
        { 
            get 
            { 
                return null; 
            } 
        }

        public static CallSite Create(Type delegateType, CallSiteBinder binder)
        {
            return null;
        }
    }

    [Ignore]
    public sealed class CallSite<T> : CallSite where T : class
    {
        public T Update 
        { 
            get 
            { 
                return null; 
            } 
        }

        public T Target;

        public static CallSite<T> Create(CallSiteBinder binder)
        {
            return null;
        }
    }

    [Ignore]
    public abstract class CallSiteBinder
    {
        public static LabelTarget UpdateLabel 
        { 
            get 
            { 
                return null; 
            } 
        }

        public T BindDelegate<T>(CallSite<T> site, object[] args) where T : class
        {
            return null;
        }
    }

    [Ignore]
    public struct AsyncVoidMethodBuilder
    {
        public static AsyncVoidMethodBuilder Create()
        {
            return default(AsyncVoidMethodBuilder);
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        public void SetResult()
        {
        }

        public void SetException(Exception exception)
        {
        }
    }

    [Ignore]
    public struct AsyncTaskMethodBuilder
    {
        public Task Task 
        { 
            get 
            { 
                return null; 
            } 
        }

        public static AsyncTaskMethodBuilder Create()
        {
            return default(AsyncTaskMethodBuilder);
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        public void SetResult()
        {
        }

        public void SetException(Exception exception)
        {
        }
    }

    [Ignore]
    public struct AsyncTaskMethodBuilder<TResult>
    {
        public Task<TResult> Task 
        { 
            get 
            { 
                return null; 
            } 
        }

        public static AsyncTaskMethodBuilder<TResult> Create()
        {
            return default(AsyncTaskMethodBuilder<TResult>);
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
        }

        public void SetResult(TResult result)
        {
        }

        public void SetException(Exception exception)
        {
        }
    }

    [Ignore]
    public interface IAsyncStateMachine
    {
        void MoveNext();
        void SetStateMachine(IAsyncStateMachine stateMachine);
    }

    [Ignore]
    public interface INotifyCompletion
    {
        void OnCompleted(Action continuation);
    }

    [Ignore]
    public interface ICriticalNotifyCompletion : INotifyCompletion
    {
        void UnsafeOnCompleted(Action continuation);
    }
}

namespace Microsoft.CSharp.RuntimeBinder
{
    [Ignore]
    public static class Binder
    {
        public static CallSiteBinder BinaryOperation(CSharpBinderFlags flags, ExpressionType operation, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            return null;
        }

        public static CallSiteBinder Convert(CSharpBinderFlags flags, Type type, Type context)
        {
            return null;
        }

        public static CallSiteBinder GetIndex(CSharpBinderFlags flags, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            return null;
        }

        public static CallSiteBinder GetMember(CSharpBinderFlags flags, string name, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            return null;
        }

        public static CallSiteBinder Invoke(CSharpBinderFlags flags, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            return null;
        }

        public static CallSiteBinder InvokeMember(CSharpBinderFlags flags, string name, IEnumerable<Type> typeArguments, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            return null;
        }

        public static CallSiteBinder InvokeConstructor(CSharpBinderFlags flags, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            return null;
        }

        public static CallSiteBinder IsEvent(CSharpBinderFlags flags, string name, Type context)
        {
            return null;
        }

        public static CallSiteBinder SetIndex(CSharpBinderFlags flags, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            return null;
        }

        public static CallSiteBinder SetMember(CSharpBinderFlags flags, string name, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            return null;
        }

        public static CallSiteBinder UnaryOperation(CSharpBinderFlags flags, ExpressionType operation, Type context, IEnumerable<CSharpArgumentInfo> argumentInfo)
        {
            return null;
        }
    }

    [Ignore]
    public enum CSharpBinderFlags
    {
        None = 0,
        CheckedContext = 1,
        InvokeSimpleName = 2,
        InvokeSpecialName = 4,
        BinaryOperationLogical = 8,
        ConvertExplicit = 16,
        ConvertArrayIndex = 32,
        ResultIndexed = 64,
        ValueFromCompoundAssignment = 128,
        ResultDiscarded = 256,
    }

    [Ignore]
    public sealed class CSharpArgumentInfo
    {
        public static CSharpArgumentInfo Create(CSharpArgumentInfoFlags flags, string name)
        {
            return null;
        }
    }

    [Ignore]
    public enum CSharpArgumentInfoFlags
    {
        None = 0,
        UseCompileTimeType = 1,
        Constant = 2,
        NamedArgument = 4,
        IsRef = 8,
        IsOut = 16,
        IsStaticType = 32,
    }
}

namespace System.Linq.Expressions
{
    [Ignore]
    public sealed class LabelTarget
    {
        internal LabelTarget() 
        { 
        }

        public string Name 
        { 
            get 
            { 
                return null; 
            } 
        
        }
        
        public Type Type 
        { 
            get 
            { 
                return null; 
            } 
        }        
    } 

    [Ignore]
    public enum ExpressionType
    {
        Add,
        AddChecked,
        And,
        AndAlso,
        ArrayLength,
        ArrayIndex,
        Call,
        Coalesce,
        Conditional,
        Constant,
        Convert,
        ConvertChecked,
        Divide,
        Equal,
        ExclusiveOr,
        GreaterThan,
        GreaterThanOrEqual,
        Invoke,
        Lambda,
        LeftShift,
        LessThan,
        LessThanOrEqual,
        ListInit,
        MemberAccess,
        MemberInit,
        Modulo,
        Multiply,
        MultiplyChecked,
        Negate,
        UnaryPlus,
        NegateChecked,
        New,
        NewArrayInit,
        NewArrayBounds,
        Not,
        NotEqual,
        Or,
        OrElse,
        Parameter,
        Power,
        Quote,
        RightShift,
        Subtract,
        SubtractChecked,
        TypeAs,
        TypeIs,
        Assign,
        Block,
        DebugInfo,
        Decrement,
        Dynamic,
        Default,
        Extension,
        Goto,
        Increment,
        Index,
        Label,
        RuntimeVariables,
        Loop,
        Switch,
        Throw,
        Try,
        Unbox,
        AddAssign,
        AndAssign,
        DivideAssign,
        ExclusiveOrAssign,
        LeftShiftAssign,
        ModuloAssign,
        MultiplyAssign,
        OrAssign,
        PowerAssign,
        RightShiftAssign,
        SubtractAssign,
        AddAssignChecked,
        MultiplyAssignChecked,
        SubtractAssignChecked,
        PreIncrementAssign,
        PreDecrementAssign,
        PostIncrementAssign,
        PostDecrementAssign,
        TypeEqual,
        OnesComplement,
        IsTrue,
        IsFalse,
    }
}

namespace System.Diagnostics
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method)]
    public sealed class DebuggerStepThroughAttribute : Attribute
    {
    }
}