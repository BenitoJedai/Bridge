namespace System 
{
    /// <summary>
    /// The RangeError object indicates an error when a value is not in the set or range of allowed values.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("RangeError")]
    [Bridge.CLR.Constructor("RangeError")]
    public class RangeException : Exception
    {        
        public RangeException()
        {
        }

        public RangeException(string message)
        {
        }
    }
}
