using Bridge.CLR;

namespace System.Collections.Generic 
{
    [Ignore]
    [Namespace("Bridge")]
    public interface ICollection<T> : IEnumerable<T> 
    {
        int Count 
        {
            get;
        }

        void Add(T item);
        void Clear();
        bool Contains(T item);
        bool Remove(T item);
    }
}
