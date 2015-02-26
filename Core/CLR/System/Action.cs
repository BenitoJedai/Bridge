using Bridge.CLR;

namespace System
{
    [Name("Function")]
    [IgnoreCast,IgnoreGeneric]
    public delegate void Action();

    [Name("Function")]
    [IgnoreCast,IgnoreGeneric]
    public delegate void Action<T>(T arg);

    [Name("Function")]
    [IgnoreCast,IgnoreGeneric]
    public delegate void Action<T1, T2>(T1 arg1, T2 arg2);

    [Name("Function")]
    [IgnoreCast,IgnoreGeneric]
    public delegate void Action<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

    [Name("Function")]
    [IgnoreCast,IgnoreGeneric]
    public delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    [Name("Function")]
    [IgnoreCast,IgnoreGeneric]
    public delegate void Action<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

    [Name("Function")]
    [IgnoreCast,IgnoreGeneric]
    public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    [Name("Function")]
    [IgnoreCast,IgnoreGeneric]
    public delegate void Action<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

    [Name("Function")]
    [IgnoreCast,IgnoreGeneric]
    public delegate void Action<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8); 
}
