using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public class Person
    {
        public string Name 
        { 
            get; 
            set; 
        }
    }

    public class App
    {
        public static void Start()
        {
            var person = new Person
            {
                Name = "Geoff"
            };

            var d1 = new Date();
            var d2 = new Date();
            var arr = new int[]{};

            if (d1 == d2)
            {
            }

            if (new Date(1) == d2)
            {
            }

            if (d1 == new Date(person.Name))
            {
            }

            person.Name = person.Name;
        }
    }   
}