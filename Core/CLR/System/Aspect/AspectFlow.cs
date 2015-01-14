using System;

namespace Bridge.CLR
{
    public enum AspectFlow
    {
        Default = 0,
        Continue = 1,
        RethrowException = 2,
        Return = 3
    }
}
