namespace System.Collections 
{
    [ScriptKit.CLR.Ignore]
    public interface IEnumerator 
    {
        object Current 
        {
            get;
        }

        bool MoveNext();
    }
}
