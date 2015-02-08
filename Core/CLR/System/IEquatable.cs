using Bridge.CLR;
namespace System 
{
    [Ignore]
    [Namespace("Bridge")]
	public interface IEquatable<in T> 
    {
		bool Equals(T other);
	}
}
