using Bridge;

namespace System.Collections 
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IEnumerator : IBridgeClass
    {
        object Current 
        {
            get;
        }

        bool MoveNext();
        
        void Reset();
    }
}
