using Bridge.Foundation;

namespace System.Collections
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IEqualityComparer
    {
        bool Equals(object x, object y);
        int GetHashCode(object obj);
    }
}