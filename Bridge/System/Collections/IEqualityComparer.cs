using Bridge;

namespace System.Collections
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IEqualityComparer : IBridgeClass
    {
        bool Equals(object x, object y);
        int GetHashCode(object obj);
    }
}