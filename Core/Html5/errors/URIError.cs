using Bridge.Foundation;

namespace System 
{
    /// <summary>
    /// The URIError object represents an error when a global URI handling function was used in a wrong way.
    /// </summary>
    [Ignore]
    [Name("URIError")]
    [Constructor("URIError")]
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
