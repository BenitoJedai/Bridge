namespace System.Collections.Generic 
{
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("ScriptKit.ICollection")]
    public interface ICollection<T> : IEnumerable<T> 
    {
        int Count 
        {
            get;
        }

        T Get(int index);
        void Set(int index, T value);
    }
}
