namespace System.Collections
{
    [ScriptKit.CLR.Ignore]
    public interface IEqualityComparer
    {
        bool Equals(object x, object y);
        int GetHashCode(object obj);
    }
}