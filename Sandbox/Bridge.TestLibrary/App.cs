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
            Console.Log("message", 1, 2, 3);
            Document.GetElementById("el1").CallFn("name", 1, 2);
            Document.GetElementById("el1").CallMyFn("name", 1, 2);
            Document.GetElementById("el1").As<InputElement>().CallFn("name", 1, 2);
            Document.GetElementById("el1").Cast<InputElement>().CallFn("name", 1, 2);
        }
    }

    public static class ObjExt
    {
        [Template("Bridge.fn.call({obj}, {name}, {*args})")]
        public static void CallMyFn(this object obj, string name, params object[] args)
        {
        }
    }
}