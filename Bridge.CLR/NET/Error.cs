namespace System 
{

    [Bridge.CLR.Ignore]
    [Bridge.CLR.Name("Error")]
    [Bridge.CLR.Constructor("Error")]
    public class Exception 
    {
        public string name;
        public string message;

        public Exception() 
        {
        }
        
        public Exception(string message) 
        {
        }
    }

    [Bridge.CLR.Ignore]
    public class NotImplementedException : Exception 
    {
    }
}
