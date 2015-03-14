using Bridge;

namespace System 
{
    [Ignore]
    public class Type 
    {
        public static Type GetTypeFromHandle(RuntimeTypeHandle typeHandle)
        {
            return null;
        } 
    }

    [Ignore]
    public class ValueType 
    { 
    }

    [Ignore]
    public class Enum 
    { 
    }

    [Ignore]
    public struct IntPtr 
    { 
    }

    [Ignore]
    public struct UIntPtr 
    { 
    }

    [Ignore]
    public class ParamArrayAttribute 
    { 
    }

    [Ignore]
    public interface IDisposable 
    {
        void Dispose();
    }

    [Ignore]
    public struct RuntimeTypeHandle
    {
    }

    [Ignore]
    public struct RuntimeFieldHandle
    {
    }
}