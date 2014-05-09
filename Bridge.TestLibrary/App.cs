using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    [Namespace(false)]
    public class Person
    {
    }

    [Namespace("Sales")]
    public class Customer
    {
    }

    public class App
    {       
        public static void Start()
        {
            var p = new Person();
            Console.Log((Person)p);

            var c = new Customer();
            Console.Log((Customer)c);
        }
    }   
}