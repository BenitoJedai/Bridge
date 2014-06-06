using Bridge.CLR;

namespace System.Collections.Generic 
{
    [Ignore]
    [Name("Bridge.IEnumerable")]
    public interface IEnumerable<T> 
    {
        IEnumerator<T> GetEnumerator();
    }
}
