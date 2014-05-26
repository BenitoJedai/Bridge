namespace System 
{
    /// <summary>
    /// The URIError object represents an error when a global URI handling function was used in a wrong way.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("URIError")]
    [Bridge.CLR.Constructor("URIError")]
    public class URIException : Exception
    {        
        public URIException()
        {
        }

        public URIException(string message)
        {
        }
    }
}
