namespace System 
{
    /// <summary>
    /// The TypeError object represents an error when a value is not of the expected type.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("TypeError")]
    [Bridge.CLR.Constructor("TypeError")]
    public class TypeException : Exception
    {        
        public TypeException()
        {
        }

        public TypeException(string message)
        {
        }
    }
}
