namespace System.Collections.Generic 
{
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Bridge.IEnumerator")]
    public interface IEnumerator<T> 
    {
        T Current 
        {
            get;
        }

        bool MoveNext();
    }
}
