using ScriptKit.CLR.Html;
using System;

namespace CompanyX
{
    public static class App
    {
        public static string name;

        public static void Init2()
        {
            var customer = new Customer
            {
                CompanyName = "Object.NET",
                Name = "Geoff"
            };

            App.name = customer.Name;

            Console.Log("Customer", customer);

            App.DoSomething();
        }

        public static void DoSomething()
        {
            Console.Log("field", App.name);

            var msg = "World";  //new Date().toTimeString();

            msg = "Hello World"; //  + msg; 

            Console.Log("Message", msg);
        }
    }
}