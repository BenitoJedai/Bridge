namespace System 
{

    [ScriptKit.Core.Ignore]
    [ScriptKit.Core.TypeName("Error")]
    [ScriptKit.Core.Constructor("Error")]
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

    [ScriptKit.Core.Ignore]
    public class NotImplementedException : Exception 
    {
    }
}
