using Bridge.CLR;

namespace System.Collections 
{
    [Ignore]
    [Name("Bridge.IEnumerator")]
    public interface IEnumerator 
    {
        object Current 
        {
            get;
        }

        bool MoveNext();
    }
}
