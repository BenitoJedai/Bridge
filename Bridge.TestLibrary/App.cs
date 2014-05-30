using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{    
    public partial class Class1
    {
        public int value;
        public string strValue;

        public Class1(int value)
        {
            this.value = value;
        }

        public void Method1()
        {
            Console.Log(this.value);
        }

        public static void Start()
        {
            var c1 = new Class1(1);
            var c2 = new Class2(2);
            var c3 = Window.Eval<Class1>("TestProject.Class1(3)");
            var c4 = Window.Eval<Class1>("TestProject.Class2(4)");
            
            c1.Method1();
            c2.Method1();
            c3.Method1();
            c4.Method1();
        }   
    }

    public partial class Class2
    {
        public int value;
        public string strValue;

        public Class2(int value)
        {
            this.value = value;
        }

        public void Method1()
        {
            Console.Log(this.value);
        }
    }
}