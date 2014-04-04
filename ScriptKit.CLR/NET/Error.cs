namespace System 
{

    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("Error")]
    [ScriptKit.CLR.Constructor("Error")]
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

    [ScriptKit.CLR.Ignore]
    public class NotImplementedException : Exception 
    {
    }
}
