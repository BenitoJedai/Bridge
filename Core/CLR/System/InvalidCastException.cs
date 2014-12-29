using Bridge.CLR;

namespace System 
{
	[Ignore]
	[Namespace("Bridge")]
	public class InvalidCastException : Exception 
    {
		public InvalidCastException() 
        {
		}

		public InvalidCastException(string message) 
        {
		}

		public InvalidCastException(string message, Exception innerException) 
        {
		}
	}
}
