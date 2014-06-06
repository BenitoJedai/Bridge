using Bridge.CLR;

namespace System 
{
    [Ignore]
    [Name("Error")]
    [Constructor("Error")]
    public class Exception 
    {
        /// <summary>
        /// Error name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Human-readable description of the error
        /// </summary>
        public readonly string Message;

        /// <summary>
        /// Path to file that raised this error.
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// Line number in file that raised this error.
        /// </summary>
        public readonly int LineNumber;

        /// <summary>
        /// Column number in line that raised this error.
        /// </summary>
        public readonly int ColumnNumber;

        /// <summary>
        /// Stack trace.
        /// </summary>
        public readonly string Stack;


        public Exception() 
        {
        }
        
        public Exception(string message) 
        {
        }
    }

    [Ignore]
    [Name("Error")]
    [Constructor("Error")]
    public class NotImplementedException : Exception 
    {
    }
}
