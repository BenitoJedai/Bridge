using Bridge.CLR;

namespace System 
{
	[Ignore]
	public interface IFormattable 
    {
		[Template("Bridge.format({this}, {format})")]
		string ToString(string format);
	}
}
