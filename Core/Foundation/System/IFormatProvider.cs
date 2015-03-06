using Bridge.Foundation;
namespace System 
{
	[Namespace("Bridge")]
	[Ignore]
	public interface IFormatProvider 
    {
		object GetFormat(Type formatType);
	}
}
