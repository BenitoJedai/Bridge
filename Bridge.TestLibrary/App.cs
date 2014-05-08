using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    [Bridge.CLR.ObjectLiteral]
    public class Person
    {
        public string Name { get; set; }
        public string Field;
    }


    public class App
    {
        
        public static void Start()
        {
            Console.Log(Bridge.DateTime.Today.DayOfYear);            
        }
    }   
}