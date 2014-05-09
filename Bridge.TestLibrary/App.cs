using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
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

    public class App
    {       
        public static void Start()
        {
            var obj = new { Test = "" };
            var c = new Company { Name = "Test" };
            c.DoSomething();
            Console.Log(c.Name);
        }
    }   
}