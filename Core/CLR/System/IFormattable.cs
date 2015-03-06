using Bridge.CLR;

namespace System 
{
	[Ignore]
    [Namespace("Bridge")]
	public interface IFormattable 
    {
        [Template("Bridge.format({this}, {format}, {formatProvider})")]
        string Format(string format, IFormatProvider formatProvider);
	}
}
