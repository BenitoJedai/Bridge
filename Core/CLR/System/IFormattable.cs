using Bridge.CLR;

namespace System 
{
	[Ignore]
    [Namespace("Bridge")]
	public interface IFormattable 
    {
        string Format(string format);
	}
}
