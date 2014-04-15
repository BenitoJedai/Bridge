using ScriptKit.CLR.Html;
using System;

namespace TestProject
{
    public enum MyEnum
    {
        Item1,
        Item2,
        Name        
    }

    
    enum Coolness : byte
    {

        NotSoCool = 1,

        Cool = 2,

        VeryCool = 4,

        SuperCool = 8

    }
    
    public static class App
    {
        private static string name;
        public const string INDEX = "index";

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
            MyEnum e = MyEnum.Name;
            string c = App.INDEX;
            Coolness isCool = Coolness.Cool | Coolness.VeryCool | Coolness.SuperCool;
            var s = ("m".ToLocaleLowerCase().ExtMethod1(1).ToLocaleString().ExtMethod2(2));
        }

    }   
}
