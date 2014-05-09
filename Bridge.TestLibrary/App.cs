using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{ 
    public class App
    {
        public static void Start()
        {
            var d1 = DateTime.Now;

            var d2 = DateTime.Now.ClearTime();

            Console.Log("DateTime", d1.Millisecond);
            Console.Log("ClearTime", d2.Millisecond);
        }
    }   
}