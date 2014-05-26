namespace System 
{
    /// <summary>
    /// The SyntaxError object represents an error when trying to interpret syntactically invalid code.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("SyntaxError")]
    [Bridge.CLR.Constructor("SyntaxError")]
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
