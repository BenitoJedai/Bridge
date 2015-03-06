using Bridge.CLR;
namespace System 
{
	[Namespace("Bridge")]
	[Ignore]
    public interface ICloneable 
    {
		object Clone();
	}
}
