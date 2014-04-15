using ScriptKit.CLR.Html;
using System;

namespace TestProject
{
    public static class App
    {
        private static string name;

        public static string ExtMethod1(this string str, int i)
        {
            return str;
        }

        public static string ExtMethod2(this string str, int i)
        {
            return str;
        }
        
        public static void Run()
        {
            DateTime dt = new DateTime();
            var s = ("m".ToLocaleLowerCase().ExtMethod1(1).ToLocaleString().ExtMethod2(2));
        }

    }   
}
