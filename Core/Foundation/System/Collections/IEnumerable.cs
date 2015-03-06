using Bridge.Foundation;

namespace System.Collections 
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IEnumerable 
    {
        IEnumerator GetEnumerator();
    }
}
