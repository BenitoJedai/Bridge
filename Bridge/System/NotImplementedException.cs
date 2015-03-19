using Bridge;

namespace System 
{
	[Ignore]
	[Namespace("Bridge")]
	public class NotImplementedException : Exception 
    {
		public NotImplementedException() 
        {
		}

		public NotImplementedException(string message) 
        {
		}

		public NotImplementedException(string message, Exception innerException) 
        {
		}
	}
}
