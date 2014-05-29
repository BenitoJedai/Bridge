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

        public Class1(int value)
        {
            this.value = value;
        }

        public static void LoadInit()
        {
            Console.Log("Class1.LoadInit");
        }

        public static void Start()
        {
            Document.AddEventListener("DOMContentLoaded", LoadInit, false);

            var c = new Class1(11);
            Document.AddEventListener("DOMContentLoaded", c.LoadInitExt, false);
        }
    }

    public static class Class1Extension
    {
        public static void LoadInitExt(this Class1 instance)
        {
            Console.Log(instance.value);
        }
    }
}