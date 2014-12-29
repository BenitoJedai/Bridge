using Bridge.CLR;

namespace System.Collections.Generic 
{
	[Ignore]
	[Namespace("Bridge")]
	public class KeyNotFoundException : Exception 
    {
		public KeyNotFoundException() 
        {
		}

		public KeyNotFoundException(string message) 
        {
		}

		public KeyNotFoundException(string message, Exception innerException) 
        {
		}
	}
}
