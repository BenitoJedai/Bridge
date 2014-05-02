namespace System
{
    [ScriptKit.CLR.Name("Function")]
    public delegate void Action();

    [ScriptKit.CLR.Name("Function")]
    public delegate void Action<T>(T arg);

    [ScriptKit.CLR.Name("Function")]
    public delegate void Action<T1, T2>(T1 arg1, T2 arg2);

    [ScriptKit.CLR.Name("Function")]
    public delegate void Action<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

    [ScriptKit.CLR.Name("Function")]
    public delegate void Action<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    [ScriptKit.CLR.Name("Function")]
    public delegate void Action<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

    [ScriptKit.CLR.Name("Function")]
    public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    [ScriptKit.CLR.Name("Function")]
    public delegate void Action<T1, T2, T3, T4, T5, T6, T7>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

    [ScriptKit.CLR.Name("Function")]
    public delegate void Action<T1, T2, T3, T4, T5, T6, T7, T8>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8); 
}
