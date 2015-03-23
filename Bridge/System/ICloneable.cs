using Bridge;
namespace System 
{
	[Namespace("Bridge")]
	[Ignore]
    public interface ICloneable : IBridgeClass
    {
		object Clone();
	}
}
