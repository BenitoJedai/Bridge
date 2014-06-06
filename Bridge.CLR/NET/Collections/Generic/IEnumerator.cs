using Bridge.CLR;

namespace System.Collections.Generic 
{
    [Ignore]
    [Name("Bridge.IEnumerator")]
    public interface IEnumerator<T> 
    {
        T Current 
        {
            get;
        }

        bool MoveNext();
    }
}
