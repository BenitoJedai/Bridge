using Bridge.CLR;
namespace System 
{
    [Ignore]
	public interface IEquatable<in T> 
    {
		[Template("Bridge.equalsT({this}, {other})")]
		bool Equals(T other);
	}
}
