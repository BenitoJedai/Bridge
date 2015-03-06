using Bridge.Foundation;

namespace System 
{
	[Ignore]
	[Namespace("Bridge")]
	public class NullReferenceException : Exception 
    {
		public NullReferenceException() 
        {
		}

		public NullReferenceException(string message) 
        {
		}

		public NullReferenceException(string message, Exception innerException) 
        {
		}
	}
}
