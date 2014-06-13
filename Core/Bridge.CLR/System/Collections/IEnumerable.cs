using Bridge.CLR;

namespace System.Collections 
{
    [Ignore]
    [Name("Bridge.IEnumerable")]
    public interface IEnumerable 
    {
        IEnumerator GetEnumerator();
    }
}
