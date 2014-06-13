using Bridge.CLR;

namespace System 
{
    /// <summary>
    /// The SyntaxError object represents an error when trying to interpret syntactically invalid code.
    /// </summary>
    [Ignore]
    [Name("SyntaxError")]
    [Constructor("SyntaxError")]
    public class SyntaxException : Exception
    {        
        public SyntaxException()
        {
        }

        public SyntaxException(string message)
        {
        }
    }
}
