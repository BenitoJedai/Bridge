using Bridge;

namespace System.Collections.Generic 
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IEnumerator<T> : IBridgeClass
    {
        T Current 
        {
            get;
        }

        bool MoveNext();
    }
}
