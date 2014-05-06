namespace System.Collections 
{
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Bridge.IEnumerable")]
    public interface IEnumerable 
    {
        IEnumerator GetEnumerator();
    }
}
