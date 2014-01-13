using System;

namespace ScriptKit.NET
{
    public class Exception : System.Exception 
    {
        public Exception(string message) : base(message) 
        {
        }

        public static ScriptKit.NET.Exception Create(string format, params object[] args)
        {
            return new ScriptKit.NET.Exception(String.Format(format, args));
        }

        public static void Throw(string format, params object[] args) 
        {
            throw ScriptKit.NET.Exception.Create(format, args);
        }
    }
}
