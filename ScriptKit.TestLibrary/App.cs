using ScriptKit.CLR;
using ScriptKit.CLR.Html;
using ScriptKit.TestLibrary;
using System;

namespace TestProject
{
    public static class App
    {
        public static string Name = "Geoff";
        public static object Config = new { Item = "Value", Item1 = "Value1" };
        

        public static bool IsEmpty(this string instance)
        {
            return instance.Length > 0;
        }

        public static void Start()
        {
            bool isEmpty = !TestProject.App.IsEmpty("");
            bool isEmpty1 = "".IsEmpty();
            
            object obj = new object();
            string s = ScriptKit.CLR.Core.Apply<string>("", new { Item = "Value", Item1 = "Value1" });
            
            
            var cf = new { Item = "Value", Item1 = "Value1" };
            Console.Log(cf.Item1);
            
            Console.Log(App.Config["Item1"]);

            string name = "Item";
            Console.Log(App.Config[name]);
        }

    }   
}
