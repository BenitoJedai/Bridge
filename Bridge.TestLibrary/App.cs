using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public partial class Class1
    {
        public void Method1()
        {
            Console.Log("[no arguments]");
        }

        
        public void Method1(object value)
        {
            Console.Log("object");
        }

        public void Method1(string value)
        {
            Console.Log("string");
        }

        public void Method1(int value)
        {
            Console.Log("int");
        }
    }
}