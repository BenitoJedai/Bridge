using Bridge.Foundation;
namespace System 
{
    [Ignore]
    [Namespace("Bridge")]
    public interface IComparable
    {
        [Template("Bridge.compare({this}, {obj})")]
        int CompareTo(Object obj);
    }

    [Ignore]
    [Namespace("Bridge")]
	public interface IComparable<in T> 
    {
        [Template("Bridge.compare({this}, {other})")]
		int CompareTo(T other);
	}
}
