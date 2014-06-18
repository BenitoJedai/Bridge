using Bridge.CLR;
using Bridge.Html5;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{    
    public partial class Class1<T>
    {
        [Template("mmm({i})")]
        public Class1(int i)
        {

        }

        [Template("mmm({s}, {T})")]
        public Class1(string s)
        {

        }

        [Template("mmm({t}, {T})")]
        public Class1(T t)
        {

        }

        public static void Start()
        {
            var c1 = new Class1<object>(1);
            var c2 = new Class1<object>("s");
            var c3 = new Class1<bool>(true);
        }
    }
}