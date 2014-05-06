namespace System.Collections 
{
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Bridge.IEnumerator")]
    public interface IEnumerator 
    {
        object Current 
        {
            get;
        }

        bool MoveNext();
    }
}
