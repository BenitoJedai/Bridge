using Bridge.CLR;

namespace System 
{
    /// <summary>
    /// The ReferenceError object represents an error when a non-existent variable is referenced.
    /// </summary>
    [Ignore]
    [Name("ReferenceError")]
    [Constructor("ReferenceError")]
    public class ReferenceException : Exception
    {        
        public ReferenceException()
        {
        }

        public ReferenceException(string message)
        {
        }
    }
}
