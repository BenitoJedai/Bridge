using Bridge.CLR;
namespace System 
{
    [Ignore]
	public interface IComparable<in T> 
    {
		[Template("Bridge.compare({this}, {other})")]
		int CompareTo(T other);
	}
}
