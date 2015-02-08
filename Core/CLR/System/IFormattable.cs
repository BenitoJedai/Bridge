using Bridge.CLR;

namespace System 
{
	[Ignore]
    [Namespace("Bridge")]
	public interface IFormattable 
    {
		string ToString(string format);
	}
}
