using ScriptKit.CLR.Html;
using System;

namespace CompanyX
{
    public static class App
    {
        public static string Name = "Geoff";

        public static void Start()
        {
            int val = "sas".Length + 100;

            Console.Log("Length", val);

            Console.Log("Name", App.Name);

            var customer = new Customer {
                Name = "Vladimir",
                CompanyName = "Object.NET"
            };

            Console.Log("Company", customer.CompanyName);

            // Window
            // Document
        }
    }

    [ScriptKit.CLR.Name("App.direct")]
    public static class MyAppMethods
    {
        public static void Method1()
        {
        }

        public static void Method2()
        {
        }
    }
}