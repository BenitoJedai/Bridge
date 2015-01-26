using System;
using Bridge.CLR;

namespace Bridge.Aspects
{
    [Ignore]
    public enum AspectFlow
    {
        Default = 0,
        Continue = 1,
        RethrowException = 2,
        Return = 3
    }
}
