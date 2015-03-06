using Bridge.CLR;
namespace System 
{
	[Namespace("Bridge")]
	[Ignore]
	public interface IFormatProvider 
    {
		object GetFormat(Type formatType);
	}
}
