using ScriptKit.CLR.Html;
using System;

namespace TestProject
{
    public static class App
    {
        public static void Run()
        {
            var message = "start";
            message += "end";

            var customer = new Customer("Name") { LastName = "LastName" };
            Console.Log(customer.LastName);
            customer.VirttualMethod();
        }
    }
    public class Person
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public virtual void VirttualMethod()
        {
            Console.Log("Person.VirtualMethod");
        }
    }

    public class Customer : Person
    {
        public Customer(string name)
        {
            this.Name = name;
        }

        public override void VirttualMethod()
        {
            base.VirttualMethod();
            Console.Log("Customer.VirtualMethod");
        }

        public int CustomerId { get; set; }
    }

   
}
