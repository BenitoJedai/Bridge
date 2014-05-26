namespace System 
{
    /// <summary>
    /// The ReferenceError object represents an error when a non-existent variable is referenced.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("ReferenceError")]
    [Bridge.CLR.Constructor("ReferenceError")]
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
