using Bridge.CLR;
namespace System 
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IComparable
    {
        int CompareTo(Object obj);
    }

    [Ignore]
    [Namespace("Bridge")]
	public interface IComparable<in T> 
    {
		int CompareTo(T other);
	}
}
