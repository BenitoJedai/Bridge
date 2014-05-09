using Bridge.CLR.Html;
using Bridge.CLR;
using System;

namespace CompanyX
{
    [ObjectLiteral]
    public class Company
    {
        public string Name { get; set; }
        
        public void DoSomething()
        {
            Console.Log("DoSomething called");
        }
    }

    public static class App
    {
        public static string Name = "Geoff";

        public static void Start()
        {
            var person = new Person { Name = Name };

            var company = new Company { Name = "Object.NET" };

            //company.DoSomething();

            Person.DoSomething(person);

            var date = DateTime.Today.DayOfYear;
            
            // Initalize int array fails
            var items = new int[6] { 1, 1, 2, 3, 5, 8 };

            Console.Log(items[5]);

            Console.Log(DateTime.Today.DayOfYear);

            var d1 = DateTime.Now;

            Console.Log("DateTime", d1.Millisecond);
            Console.Log("ClearTime", d1.ClearTime().Millisecond);
        }
    }
}