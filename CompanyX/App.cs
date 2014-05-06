using Bridge.CLR.Html;
using Bridge.CLR;
using System;

namespace CompanyX
{
    public static class App
    {
        public static string Name = "Geoff";

        public static void Start()
        {
            var person = new Person { Name = Name };

            Person.DoSomething(person);
            var date = DateTime.Now;

            Console.Log(date);
        }
    }
}