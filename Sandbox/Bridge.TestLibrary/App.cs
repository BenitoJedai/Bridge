using Bridge.CLR;
using Bridge.Html5;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{    
    public partial class Class1
    {
        public static void Start()
        {
            Document.GetElementById("id").As<InputElement>().Value = "test";
            Document.GetElementById("id").CallFn("fnName");
        }
    }
}