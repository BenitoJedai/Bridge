using Bridge.CLR;

namespace System.Collections
{
    [Ignore]
    public interface IEqualityComparer
    {
        bool Equals(object x, object y);
        int GetHashCode(object obj);
    }
}