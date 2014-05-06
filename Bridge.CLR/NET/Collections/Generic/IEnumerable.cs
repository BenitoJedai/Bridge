namespace System.Collections.Generic 
{
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Bridge.IEnumerable")]
    public interface IEnumerable<T> 
    {
        IEnumerator<T> GetEnumerator();
    }
}
