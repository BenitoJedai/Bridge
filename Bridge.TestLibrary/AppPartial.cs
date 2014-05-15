using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{
    public partial class Class1: I2
    {
        [Name("f")]
        public int Field;
        
        public void Method2()
        {
        }

        public string Prop2
        {
            get;
            set;
        }
    }
}