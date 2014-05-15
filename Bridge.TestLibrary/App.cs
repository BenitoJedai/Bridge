using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public interface I1
    {
    }

    public interface I2
    {
    }
    
    public partial class Class1: I1
    {
        [Name("f2")]
        public int Field2;

        public void Method1()
        {
            Field = 1;
            Method2();
            Method3();
        }

        public string Prop1
        {
            get;
            set;
        }

        partial void Method3();
    }
}