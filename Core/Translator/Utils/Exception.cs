using Bridge.Contract;
using System;

namespace Bridge.NET
{
    public class Exception : System.Exception, IVisitorException 
    {
        public Exception(string message) : base(message) 
        {
        }

        public static IVisitorException Create(string format, params object[] args)
        {
            return new Bridge.NET.Exception(String.Format(format, args));
        }

        public static void Throw(string format, params object[] args) 
        {
            throw (Exception)Bridge.NET.Exception.Create(format, args);
        }
    }
}