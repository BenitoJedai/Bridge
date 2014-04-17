namespace System.Collections 
{
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("ScriptKit.IEnumerator")]
    public interface IEnumerator 
    {
        object Current 
        {
            get;
        }

        bool MoveNext();
    }
}
