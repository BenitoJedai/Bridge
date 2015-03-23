using Bridge;

namespace System.Collections.Generic 
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IEnumerable<T> : IEnumerable, IBridgeClass
    {
        new IEnumerator<T> GetEnumerator();
    }
}
