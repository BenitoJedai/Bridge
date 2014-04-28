using ScriptKit.CLR;
using ScriptKit.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public class StructureA
    {
        public int Prop { get; set; }
    }

    public class Class1
    {
        public Class1()
        {
            MyProperty = MyProperty;
            MyField = 1;
            MyMethod();
            Name();
        }

        public void MyMethod() { }
        public static void Name() { }
        public int MyProperty { get; set; }
        public int MyField;
    }

    public static class App
    {
        public static void Start()
        {           
        }
    }   
}
