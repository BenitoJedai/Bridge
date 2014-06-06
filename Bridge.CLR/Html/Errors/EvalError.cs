using Bridge.CLR;

namespace System 
{
    /// <summary>
    /// The EvalError object indicates an error regarding the global eval() function.
    /// </summary>
    [Ignore]
    [Name("EvalError")]
    [Constructor("EvalError")]
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
