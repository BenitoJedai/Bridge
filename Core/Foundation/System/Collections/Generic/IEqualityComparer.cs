using Bridge.Foundation;

namespace System.Collections.Generic
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IEqualityComparer<in T> : IEqualityComparer
    {
        bool Equals(T x, T y);
        int GetHashCode(T obj);
    }

    [Ignore]
    [Namespace("Bridge")]
    public abstract class EqualityComparer<T> : IEqualityComparer<T>
    {
        public static EqualityComparer<T> Default { get { return null; } }

        public abstract bool Equals(T x, T y);
        public abstract int GetHashCode(T obj);

        bool IEqualityComparer.Equals(object x, object y) 
        { 
            return false; 
        }

        int IEqualityComparer.GetHashCode(object obj) 
        { 
            return 0; 
        }
    }
}