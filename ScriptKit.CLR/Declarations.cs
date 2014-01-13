namespace System 
{
    [ScriptKit.Core.Ignore]
    public class Type 
    { 
    }

    [ScriptKit.Core.Ignore]
    public class ValueType 
    { 
    }

    [ScriptKit.Core.Ignore]
    public class Enum 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct IntPtr 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct UIntPtr 
    { 
    }

    [ScriptKit.Core.Ignore]
    public class ParamArrayAttribute 
    { 
    }

    [ScriptKit.Core.Ignore]
    public interface IDisposable 
    {
        void Dispose();
    }

    [ScriptKit.Core.Ignore]
    public struct Void
    { 
    }
    
    [ScriptKit.Core.Ignore]
    public struct Byte 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct Int16 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct Int64 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct Single 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct Char 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct SByte 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct UInt16 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct UInt32 
    { 
    }

    [ScriptKit.Core.Ignore]
    public struct UInt64 
    { 
    }
}

namespace System.Collections 
{
    [ScriptKit.Core.Ignore]
    public interface IEnumerable 
    {
        IEnumerator GetEnumerator();
    }

    [ScriptKit.Core.Ignore]
    public interface IEnumerator 
    {
        object Current 
        { 
            get; 
        }
        bool MoveNext();
    }
}
