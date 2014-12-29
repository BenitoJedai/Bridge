using Bridge.CLR;

namespace System 
{
	[Ignore]
	[Namespace("Bridge")]
	public class DivideByZeroException : Exception 
    {
		public DivideByZeroException() 
        {
		}

		public DivideByZeroException(string message) 
        {
		}

		public DivideByZeroException(string message, Exception innerException) 
        {
		}
	}
}
