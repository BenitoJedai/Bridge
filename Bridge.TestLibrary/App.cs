using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{    
    public partial class Class1
    {
        public Class1()
        {
            Console.Log("Class1");
        }

        public Class1(string value)
        {
            Console.Log("Class1 string:" + value);
        }

        public static void Start(string s)
        {
            var c1 = new Class1(s);
            var c2 = new Class2("c2");
        }
    }

    public partial class Class2: Class1
    {
        public Class2() : base("c2")
        {            
            Console.Log("Class2");
        }

        public Class2(string value)
        {
            Console.Log("Class2 string: " + value);
        }
    }
}