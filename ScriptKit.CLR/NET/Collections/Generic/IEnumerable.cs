namespace System.Collections.Generic 
{
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("ScriptKit.IEnumerable")]
    public interface IEnumerable<T> 
    {
        IEnumerator<T> GetEnumerator();
    }
}
