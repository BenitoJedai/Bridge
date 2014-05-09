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

            company.DoSomething();

            Person.DoSomething(person);

            // Initalize int array fails
            var items = new int[6] { 1, 1, 2, 3, 5, 8 };

            Console.Log(items[5]);

            Console.Log(DateTime.Today.DayOfYear);

            var d1 = DateTime.Now;
            var d2 = d1.Clone().ClearTime();

            Console.Log("DateTime", d1.Hour);

            Console.Log("d2.ClearTime", d2.Hour);
            Console.Log("d2.ResetTime", d2.ResetTime().Hour);
            Console.Log(d2.Minute);

            var config = new DateTimeConfig { 
                Year = 1973,
                Month = 2,
                Day = 20,
                Hour = 5,
                Minute = 45,
                Second = 18,
                Millisecond = 333
            };

            Console.Log("Year", config.Year);
            Console.Log(d2.Set(config));
            Console.Log(d2.Minute);
            Console.Log(d2.DateData);

            var config2 = new DateTimeConfig {
                Year = 2,
                //Month = 2,
                //Day = 2,
                //Hour = 2,
                //Minute = 2,
                //Second = 2,
                //Millisecond = 2
            };

            Console.Log(d2.Add(config2).DateData);
        }
    }
}