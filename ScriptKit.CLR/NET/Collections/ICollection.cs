namespace System.Collections
{

    public interface ICollection<T> : IEnumerable  
    {
        void add(T item);
        void clear();
        bool contains(T item);
        bool isEmpty();
        //Iterator<T> iterator();
        void remove(T item);
        int count();
        T[] toArray();
    }
}