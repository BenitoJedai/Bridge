namespace System.Collections.Generic 
{
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("ScriptKit.ICollection")]
    public interface ICollection<T> : IEnumerable<T> 
    {
        int Length 
        {
            get;
        }

        T this[int index] 
        {
            get;
            set;
        }
    }
}
