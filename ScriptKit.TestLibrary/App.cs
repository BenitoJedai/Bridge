using ScriptKit.CLR;
using ScriptKit.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public class Person
    {
        public string FirstName
        {
            get;
            set;
        }

        public string FullName
        {
            get
            {
                return this.FirstName;
            }
        }
    }

    public class App
    {
        public static string Name = "Geoff";
        public string Field = "Geoff";

        public static void Start()
        {
            var list = new Dictionary<string, Person>();

            list.Add("geoff", new Person { FirstName = "Geoff" });
            list.Add("daniil", new Person { FirstName = "Daniil" });
            list.Add("vladimir", new Person { FirstName = "Vladimir" });

            foreach (KeyValuePair<string, Person> pair in list)
            {
                Console.Log(pair.Key, pair.Value.FullName);
            }
        }
    }   
}
