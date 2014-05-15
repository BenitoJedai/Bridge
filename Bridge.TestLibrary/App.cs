using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

[assembly:Bridge.CLR.FilesHierrarchy(TypesSplit.ByFullName)]
namespace TestProject
{
    [Bridge.CLR.Module("mClass1")]
    //[Bridge.CLR.FileName("fn1")]
    public class Class1
    {
        public void Method1()
        {
            var c = (Class2<int>)(new Class2<int>());
        }
    }

    [Bridge.CLR.Module("mClass2")]
    //[Bridge.CLR.FileName("fn2")]
    public class Class2<T>
    {
        public void Method2()
        {
            var c = new Class1();
            var c1 = (Class2<int>)(new Class2<int>());
        }
    }
}