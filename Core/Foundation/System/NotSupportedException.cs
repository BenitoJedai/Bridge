using Bridge.Foundation;

namespace System {
	[Ignore]
	[Namespace("Bridge")]
	public class NotSupportedException : Exception 
    {
		public NotSupportedException() 
        {
		}

		public NotSupportedException(string message) 
        {
		}

		public NotSupportedException(string message, Exception innerException) 
        {
		}
	}
}
