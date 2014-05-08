using Bridge.CLR;
using Bridge.CLR.Html;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public class Base1
    {
        public virtual void Method1() { }
        public virtual void Method2() { }
    }

    public class Base2 : Base1
    {
        public override void Method1() { }
        public override void Method2() 
        {
            base.Method1();
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public static void DoSomething(Person person, Date d1, Date d2) 
        {
            Console.Log(person.Name);

            if (d1 == d2)
            {

            }
        }
    }

    public class App
    {
        
        public static void Start()
        {            
            var items = new int[6] { 1, 1, 2, 3, 5, 8 };
            Console.Log(items);

            Console.Log(DateTime.Today.DayOfYear);
        }
    }   
}