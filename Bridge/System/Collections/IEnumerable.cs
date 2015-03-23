using Bridge;

namespace System.Collections 
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IEnumerable : IBridgeClass
    {
        IEnumerator GetEnumerator();
    }
}
