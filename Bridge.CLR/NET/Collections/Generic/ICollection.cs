namespace System.Collections.Generic 
{
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Bridge.ICollection")]
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
