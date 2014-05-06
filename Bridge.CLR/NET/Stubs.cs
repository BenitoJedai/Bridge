namespace System 
{
    [Bridge.CLR.Ignore]
    public class Type 
    {
        public static Type GetTypeFromHandle(RuntimeTypeHandle typeHandle)
        {
            return null;
        } 
    }

    [Bridge.CLR.Ignore]
    public class ValueType 
    { 
    }

    [Bridge.CLR.Ignore]
    public class Enum 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct IntPtr 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct UIntPtr 
    { 
    }

    [Bridge.CLR.Ignore]
    public class ParamArrayAttribute 
    { 
    }

    [Bridge.CLR.Ignore]
    public interface IDisposable 
    {
        void Dispose();
    }

    [Bridge.CLR.Ignore]
    public struct Void
    { 
    }
    
    [Bridge.CLR.Ignore]
    public struct Byte 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct Int16 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct Int64 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct Single 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct Char 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct SByte 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct UInt16 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct UInt32 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct UInt64 
    { 
    }

    [Bridge.CLR.Ignore]
    public struct RuntimeTypeHandle
    {
    }

    [Bridge.CLR.Ignore]
    public struct RuntimeFieldHandle
    {
    }
}