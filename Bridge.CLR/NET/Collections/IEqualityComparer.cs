namespace System.Collections
{
    [Bridge.CLR.Ignore]
    public interface IEqualityComparer
    {
        bool Equals(object x, object y);
        int GetHashCode(object obj);
    }
}