namespace System 
{
    /// <summary>
    /// The EvalError object indicates an error regarding the global eval() function.
    /// </summary>
    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("EvalError")]
    [Bridge.CLR.Constructor("EvalError")]
    public class EvalException : Exception
    {
        public EvalException()
        {
        }

        public EvalException(string message)
        {
        }
    }
}
