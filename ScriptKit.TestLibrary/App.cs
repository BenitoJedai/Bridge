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
            Name = Name;
            Class1.Name = Class1.Name;

            MyProperty = MyProperty;
            this.MyProperty = this.MyProperty;

            MyField = 1;
            this.MyField = 1;

            MyStaticField = 1;
            Class1.MyStaticField = 1;

            MyStaticField = MyStaticField;
            Class1.MyStaticField = Class1.MyStaticField;

            var args = Window.Arguments;
        }

        public int MyProperty { get; set; }
        
        [Name("myf")]
        public int MyField;

        [Name("mysf")]
        public static int MyStaticField;

        public static int Name { get; set; }
    }

    public static class App
    {
        public static void Start()
        {           
        }
    }   
}
