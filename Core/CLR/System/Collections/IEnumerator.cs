using Bridge.CLR;

namespace System.Collections 
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IEnumerator 
    {
        object Current 
        {
            get;
        }

        bool MoveNext();
        
        void Reset();
    }
}
