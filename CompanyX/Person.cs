using Bridge.CLR.Html;
using System;

namespace CompanyX
{
    public class Person
    {
        public string Name { get; set; }

        public static void DoSomething(Person person)
        {
            Console.Log(person.Name);
        }
    }
}