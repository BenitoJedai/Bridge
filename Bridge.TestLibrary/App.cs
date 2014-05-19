using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    [ObjectLiteral]
    public class Obj
    {
        public Action MyFunc = delegate() {
            Console.Log("");
        };
    }
    
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

        public void Method()
        {
            this.Method1();
            this.Method1(new object());
            this.Method1(32);
            this.Method1("");


            Method1();
            Method1(new object());
            Method1(32);
            Method1("");
        }
    }
}