using System;

namespace Bridge.NET
{
    public class Exception : System.Exception 
    {
        public Exception(string message) : base(message) 
        {
        }

        public static Bridge.NET.Exception Create(string format, params object[] args)
        {
            return new Bridge.NET.Exception(String.Format(format, args));
        }

        public static void Throw(string format, params object[] args) 
        {
            throw Bridge.NET.Exception.Create(format, args);
        }
    }
}
