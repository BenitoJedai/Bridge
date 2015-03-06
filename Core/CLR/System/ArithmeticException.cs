using Bridge.CLR;

namespace System 
{
	[Ignore]
	[Namespace("Bridge")]
	public class ArithmeticException : Exception 
    {
		public ArithmeticException() 
        {
		}

		public ArithmeticException(string message) 
        {
		}

        public ArithmeticException(string message, Exception innerException) 
        {
		}
	}
}
