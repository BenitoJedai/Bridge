using ScriptKit.CLR.Html;
using System;

namespace TestProject
{
    public static class App
    {
        public static string Name = "Geoff";

        public static bool IsEmpty(this string instance)
        {
            return instance.Length > 0;
        }

        public static void Start()
        {
            Console.Log("IsEmpty", App.Name.IsEmpty());
        }

    }   
}
