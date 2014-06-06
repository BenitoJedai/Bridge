using Bridge.CLR.Html;
using System;

namespace CompanyX
{
    public class Person
    {
        public Person() { }

        public Person(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public void DoSomething(Person person)
        {
            Console.Log("DoSomething1", person.Name);
        }

        public void DoSomething(string message)
        {
            Console.Log("DoSomething2", message);
        }

        public void DoSomething(string title, Person person)
        {
            Console.Log(title, person.Name);
        }
    }
}